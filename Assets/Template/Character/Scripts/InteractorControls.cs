using UnityEngine;
using UnityEngine.InputSystem;

public class InteractorControls : MonoBehaviour
{
    public HoldState HoldingState {  get; private set; }
    [SerializeField] InteractBar interactBar;
    [SerializeField] Animator animator;

    private const float HoldThreshold = 0.2f;
    private Interactor _interactor;
    private bool _held = false;
    private float _heldTime = 0;

    private void Start()
    {
        _interactor = GetComponent<Interactor>();
        if (_interactor == null)
        {
            Debug.LogWarning("No Interactor component found.", gameObject);
            Destroy(this);
        }
    }

    private void Update()
    {
        UpdateHeldState(Time.deltaTime);
        switch (HoldingState) {
            case HoldState.SHORT_PRESS:
                var shortData = _interactor.AttemptShortInteraction();
                if (shortData.success)
                {
                    animator.SetTrigger("Press");
                }
                break;
            case HoldState.HOLDING:
                var longData = _interactor.AttemptLongInteraction(Time.deltaTime);
                if (longData.success)
                {
                    animator.SetBool("Interacting", true);
                    interactBar.CurrentInteraction = longData.interaction;
                    if (longData.phase == InteractionPhase.COMPLETED)
                    {
                        Interrupt();
                    }
                } else
                {
                    Interrupt();
                }
                break;
            case HoldState.HOLD_RELEASED:
                break;
            default:
                animator.SetBool("Interacting", false);
                break;
        }
    }

    private void Interrupt()
    {
        HoldingState = HoldState.NONE;
        animator.SetBool("Interacting", false);
        _heldTime = 0;
        _held = false;
    }

    private void UpdateHeldState(float delta)
    {
        if (_held)
        {
            if (_heldTime == 0)
            {
                HoldingState = HoldState.CHARGING;
            }
            _heldTime += delta;
            if (_heldTime >= HoldThreshold)
            {
                HoldingState = HoldState.HOLDING;
            }
            else
            {
                HoldingState = HoldState.CHARGING;
            }
        }
        else if (_heldTime > 0)
        {
            if (_heldTime < HoldThreshold)
            {
                HoldingState = HoldState.SHORT_PRESS;
            } else
            {
                HoldingState = HoldState.HOLD_RELEASED;
            }
            _heldTime = 0;
        }
        else
        {
            HoldingState = HoldState.NONE;
            interactBar.CurrentInteraction = null;
        }  
    }

    private void OnInteract(InputValue value)
    {
        _held = value.isPressed;
        if (_held)
        {
            _heldTime = 0;
        }
    }
}

public enum HoldState
{
    NONE,
    SHORT_PRESS,
    CHARGING,
    HOLDING,
    HOLD_RELEASED
}
