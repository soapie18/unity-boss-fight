using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class SingleLinkSwitch: Interaction
{
    //Note - this class should probably inherit monobehaviour and have its own way of being interactable/
    //responding to interaction events without depending on GAD2010 when it comes releasing publicly. A job for later.

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

        if(interactable)
        {
            OnShortInteraction += Activate;
            OnShortInteraction += TriggerToggleOpposer;
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
            OnShortInteraction -= TriggerToggleOpposer;
        }
    }

    //In the event of interaction with this object, toggle single opposer off and make this switch active.
    private void TriggerToggleOpposer(GameObject insigator)
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

        if (activeSwitch)
        {
            Deactivate();

            activeSwitch = false;
        }


        if (timed && activeSwitch)
        {
            StartCoroutine(ToggleCooldown());
        }

    }


    // Designer can call response behaviour in inspector.
    private void Activate(GameObject instigator)
    {
        OnSwitchActivate?.Invoke();
    }

    private void Deactivate()
    {
        OnSwitchDeactivate?.Invoke();
    }


}
