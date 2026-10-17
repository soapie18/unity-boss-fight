using System;
using System.Collections;
using UnityEngine;

public class SingleLinkSwitch: Interaction, IToggleable
{
    //Note - this class should probably inherit monobehaviour and have its own way of being interactable/
    //responding to interaction events without depending on GAD2010 when it comes releasing publicly. A job for later.

    public SingleLinkSwitch twinSwitch;

    public event Action toggleOpposer;

    [SerializeField] private bool interactable = false;
    [SerializeField] private bool timed = false;

    private bool activeSwitch = false;

    private void OnEnable()
    {
        if (twinSwitch)
        {
            twinSwitch.toggleOpposer += ToggleResponse;
        }

        if(interactable)
        {
            OnShortInteraction += TriggerToggleOpposer;
        }
    }

    private void OnDisable()
    {
        if (twinSwitch)
        {
            twinSwitch.toggleOpposer -= ToggleResponse;
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
            toggleOpposer?.Invoke();

            activeSwitch = true;
        }
       
    }

    //If this switch is timed, this cooldown will be used to control when it will be active until. 
    public IEnumerator ToggleCooldown()
    {
        yield return null;
    }

    //This function is for when another object toggles, and how this object responds to that.
    public void ToggleResponse()
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


    //If the switch object was a remote controller for another object, here you would call whatever event that
    //the other object would listen to. Otherwise here you would put functionality to make something happen,
    //e.g a door interacted with would unlock itself.
    public virtual void Activate()
    { }


    //If the switch object was a remote controller for another object, here you would call whatever event that
    //the other object would listen to. Otherwise here you would put functionality to make something happen,
    //e.g a door would re-lock itself.
    public virtual void Deactivate()
    { }
    

}
