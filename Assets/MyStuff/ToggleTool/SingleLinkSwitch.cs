using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class SingleLinkSwitch : LinkedSwitch
{
    [Header("Switch Attributes")]
    public SingleLinkSwitch twinSwitch;

    public event Action ToggleOpposer;

    [SerializeField] private bool interactable = false;
    [SerializeField] private bool timed = false;

    private bool activeSwitch = false;

    [Header("Toggle Events")]
    public UnityEvent OnSwitchActivate;
    public UnityEvent OnSwitchDeactivate;

    private void OnEnable()
    {
        if (twinSwitch)
        {
            twinSwitch.ToggleOpposer += ToggleResponse;
        }

        if (interactable)
        {
            Triggered += Activate;
            Triggered += TriggerToggleOpposer;
        }

    }

    private void OnDisable()
    {
        if (twinSwitch)
        {
            twinSwitch.ToggleOpposer -= ToggleResponse;
        }

        if (interactable)
        {
            Triggered -= Activate;
            Triggered -= TriggerToggleOpposer;
        }
    }


    //In the event of interaction with this object, toggle single opposer off and make this switch active.
    private void TriggerToggleOpposer()
    {
        if (!activeSwitch)
        {
            ToggleOpposer?.Invoke();

            activeSwitch = true;
        }

    }

    //If this switch is timed, this cooldown will be used to control when it will be active until. 
    private IEnumerator ToggleCooldown()
    {
        yield return null;
    }

    //This function is for when another object toggles, and how this object responds to that (turning itself off if active).
    private void ToggleResponse()
    {

        if (this.activeSwitch)
        {
            Deactivate();

            activeSwitch = false;
        }


        if (timed && activeSwitch)
        {
            //TODO: start cooldown
        }

    }


    // Designer can call response behaviour in inspector using UnityEvents.
    private void Activate()
    {
        OnSwitchActivate?.Invoke();
        Debug.Log("A switch has been activated" + this);

    }

    private void Deactivate()
    {

        OnSwitchDeactivate?.Invoke();
        Debug.Log("A switch has been deactivated" + this);

    }


}
