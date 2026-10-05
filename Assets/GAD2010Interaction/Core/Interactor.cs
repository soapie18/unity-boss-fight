using System;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
	[field: Tooltip("The GameObject responsible for instigating interactions, usually the main player. Defaults to this GameObject if left blank")]
	[field: SerializeField] public GameObject Instigator { get; set; }

	private List<Interaction> _contacts = new List<Interaction>();

    /// <summary>
    /// Emitted when a new interaction is overlapped or hit
    /// </summary>
    public event Action<GameObject, Interaction> OnInteractionEntered;
    /// <summary>
    /// Emitted when an interaction is exited.
    /// </summary>
    public event Action<GameObject, Interaction> OnInteractionExited;

    private void OnTriggerEnter(Collider other)
    {
        Entered(other.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Entered(other.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Entered(collision.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Entered(collision.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        Exited(other.gameObject);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Exited(other.gameObject);
    }

    private void OnCollisionExit(Collision collision)
    {
        Exited(collision.gameObject);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Exited(collision.gameObject);
    }

    private void Entered(GameObject owner) {
        if (owner.TryGetComponent<Interaction>(out var interaction))
        {
            if (_contacts.Contains(interaction))
            {
                return;
            }
            _contacts.Insert(0, interaction);

            OnInteractionEntered?.Invoke(owner, interaction);
        }
    }

    private void Exited(GameObject owner)
    {
        if (owner.TryGetComponent<Interaction>(out var interaction))
        {
            var index = _contacts.IndexOf(interaction);
            if (index > -1)
            {
                _contacts.RemoveAt(index);
                OnInteractionExited?.Invoke(interaction.gameObject, interaction);
            }
        }
    }

    /// <summary>
    /// Attempt a short interaction with the first suitable overlapped GameObject.
    /// </summary>
    /// <returns>Returns an object containing 2 values: success (bool) and interaction (Interaction).</returns>
    public ShortInteractionInfo AttemptShortInteraction() {
        var info = new ShortInteractionInfo();

        foreach (var contact in _contacts)
        {
            if (contact.TriggerShortInteraction(gameObject)) {
                info.success = true;
                info.interaction = contact;
                return info;
            }
        }
        return info;
    }

    /// <summary>
    /// Attempt a long interaction with the first suitable overlapped GameObject. If possible, progress it by delta seconds.
    /// </summary>
    /// <param name="delta">The amount of time, in seconds, to progress the interaction.</param>
    /// <returns>Returns an object containing values for: success (bool), node (Node2D) and phase (Interaction.Phase)</returns>
    public LongInteractionInfo AttemptLongInteraction(float delta)
    {
        var info = new LongInteractionInfo();
        foreach (var contact in _contacts)
        {
            if (contact.TriggerLongInteraction(gameObject, delta))
            {
                info.success = true;
                info.interaction = contact;
                info.phase = contact.Phase;
                return info;
            }
        }
        return info;
    }

    /// <summary>
    /// Returns all Interactions that are in contact AND active
    /// </summary>
    /// <returns>A list of Interactions.</returns>
    public List<Interaction> GetEnabledInteractions()
    {
        var results = new List<Interaction>();
        foreach (var contact in _contacts)
        {
            if (contact.Enabled)
            {
                results.Add(contact);
            }
        }
        return results;
    }
}

public class ShortInteractionInfo {
    public bool success = false;
    public Interaction interaction;
}

public class LongInteractionInfo
{
    public bool success = false;
    public Interaction interaction;
    public InteractionPhase phase = InteractionPhase.CANCELLED;
}