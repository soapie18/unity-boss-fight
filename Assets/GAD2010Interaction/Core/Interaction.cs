using System;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    [Header("General")]
    [Tooltip("What type of interaction does this respond to?")]
    [SerializeField] Type InteractionType = Type.SHORT;
    public bool Enabled
    {
        get
        {
            return _enabled;
        }
        set
        {
            bool old = _enabled;
            _enabled = value;

            if (old != value)
            {
                OnEnabledChanged?.Invoke(value);
            }
        }
    }
    [Tooltip("Is interaction currently allowed?")]
    [SerializeField] bool _enabled = true;

    [field: Tooltip("Is this a one-and-done style interaction?")]
    [field: SerializeField] public bool OneShot { get; set; } = false;

    [field: Space(10)]
    [field: Header("Long Interactions")]

    [field: Tooltip("How long does this interaction need to be maintained before it's complete?")]
    [field: SerializeField] public float Duration { get; set; } = 2f;
    [field: Tooltip("How does this interaction handle interruptions?")]
    [field: SerializeField] public InteractionProgressMethod ProgressMethod { get; set; } = InteractionProgressMethod.RESET;
    [field: Tooltip("Interaction will reset if left alone for this long (seconds)")]
    [field: SerializeField] float timeout = 0.1f;

    [Tooltip("The current phase of the long interaction (read only)")]
    public InteractionPhase Phase { get; private set; } = InteractionPhase.CANCELLED;

    private float _timer;
    private float _timeout_start;

    [Tooltip("Emitted when a short interaction is successful")]
    public event Action<GameObject> OnShortInteraction;
    [Tooltip("Emitted when a long interaction has been maintained to completion")]
    public event Action<GameObject, InteractionPhase, float> OnLongInteraction;
    [Tooltip("Emitted when an interaction is attempted, but not allowed")]
    public event Action<GameObject, Type> OnFailedInteraction;
    [Tooltip("Emits whenever the progress towards a long interaction changes")]
    public event Action<float> OnPercentChanged;
    [Tooltip("Emitted when the interaction is enabled or disabled")]
    public event Action<bool> OnEnabledChanged;

    private void Update()
    {
        if (Phase == InteractionPhase.STARTED || Phase == InteractionPhase.CONTINUED)
        {
            if (_timeout_start + timeout <= Time.time)
            {
                Phase = InteractionPhase.CANCELLED;
                OnLongInteraction?.Invoke(null, InteractionPhase.CANCELLED, 0);
            }
        }
        else if (Phase == InteractionPhase.CANCELLED)
        {
            if (ProgressMethod == InteractionProgressMethod.DECAY)
            {
                if (_timer > 0)
                {
                    _timer = Mathf.Max(0, _timer - Time.deltaTime);
                    OnPercentChanged?.Invoke(GetPercent());
                }
            }
        }
    }

    /// <summary>
    /// Check that this supports a specific type of interaction
    /// </summary>
    /// <param name="type">The type of interaction to check against</param>
    /// <returns></returns>
    public bool SupportsInteractionType(Type type)
    {
        return (InteractionType == type || InteractionType == Type.ANY) && _enabled;
    }

    /// <summary>
    /// Attempt to trigger the short interaction on this node.
    /// </summary>
    /// <param name="instigator">The GameObject attempting this interaction.</param>
    /// <returns>Returns true if the attempt was successful.</returns>
    public bool TriggerShortInteraction(GameObject instigator)
    {
        if (SupportsInteractionType(Type.SHORT))
        {
            OnShortInteraction?.Invoke(instigator);
            Enabled = !OneShot;
            return true;
        }
        OnFailedInteraction?.Invoke(instigator, Type.SHORT);
        return false;
    }

    /// <summary>
    /// Does this long interaction progress forever?
    /// </summary>
    /// <returns>Returns true if the interaction is endless.</returns>
    public bool IsEndless()
    {
        return Duration < 0;
    }

    /// <summary>
    /// How far progressed is this long interaction?
    /// </summary>
    /// <returns>Returns the progress at as float between 0 and 1.</returns>
    public float GetPercent()
    {
        if (IsEndless())
        {
            return 1;
        }
        return Mathf.Clamp(_timer / Duration, 0, 1);
    }

    /// <summary>
    /// Attempt to progress the long interaction on this object.
    /// </summary>
    /// <param name="instigator">The GameObject attempting this interaction.</param>
    /// <param name="delta">The amount of time to progress.</param>
    /// <returns>Returns true if the interaction succeeded.</returns>
    public bool TriggerLongInteraction(GameObject instigator, float delta)
    {
        if (delta <= 0 || !SupportsInteractionType(Type.LONG))
        {
            OnFailedInteraction?.Invoke(instigator, InteractionType);
            return false;
        }
        switch (Phase)
        {
            case InteractionPhase.STARTED:
                Phase = InteractionPhase.CONTINUED;
                break;
            case InteractionPhase.CONTINUED:
                Phase = InteractionPhase.CONTINUED;
                break;
            case InteractionPhase.COMPLETED:
                Phase = InteractionPhase.STARTED;
                _timer = 0;
                break;
            case InteractionPhase.CANCELLED:
                if (ProgressMethod == InteractionProgressMethod.RESET)
                {
                    _timer = 0;
                }
                Phase = (_timer > 0) ? InteractionPhase.CONTINUED : InteractionPhase.STARTED;
                break;
            default:
                break;
        }
        if (Phase == InteractionPhase.STARTED)
        {
            OnLongInteraction?.Invoke(instigator, Phase, 0);
        }
        _timer = Mathf.Clamp(_timer + delta, 0, Duration);
        if (_timer < Duration || IsEndless())
        {
            Phase = InteractionPhase.CONTINUED;
        }
        else
        {
            Phase = InteractionPhase.COMPLETED;
            Enabled = !OneShot;
        }
        OnPercentChanged?.Invoke(GetPercent());
        OnLongInteraction?.Invoke(instigator, Phase, delta);
        _timeout_start = Time.time;
        return true;
    }
}

/// <summary>
/// What form of interaction is expected by an Interaction?
/// </summary>
public enum Type
{
    SHORT,
    LONG,
    ANY
}

/// <summary>
/// The current state of a long interaction
/// </summary>
public enum InteractionPhase
{
    STARTED,
    CONTINUED,
    COMPLETED,
    CANCELLED
}

/// <summary>
/// How a long interaction behaves when not engaged
/// </summary>
public enum InteractionProgressMethod
{
    RESET,
    MAINTAIN,
    DECAY
}