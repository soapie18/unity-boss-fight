using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiLinkSwitch : Interaction, IToggleable
{
    [Header("Switch Attributes")]
    public List<MultiLinkSwitch> linkedSwitches;

    public event Action toggleOpposers;
    public event Action triggerDeactivate;

    [SerializeField] private bool interactable = false;
    [SerializeField] private bool timed = false;

    private bool activeSwitch = false;



    private void OnEnable()
    {
        foreach (MultiLinkSwitch s in linkedSwitches)
        {
            s.toggleOpposers += ToggleResponse;
        }

        if (interactable)
        {
            OnShortInteraction += Activate;
            OnShortInteraction += TriggerToggleOpposer;
        }

        triggerDeactivate += Deactivate;
    }

    private void OnDisable()
    {
        foreach (MultiLinkSwitch s in linkedSwitches)
        {
            s.toggleOpposers -= ToggleResponse;
        }


        if (interactable)
        {
            OnShortInteraction -= Activate;
            OnShortInteraction -= TriggerToggleOpposer;
        }

        triggerDeactivate -= Deactivate;
    }

    private void TriggerToggleOpposer(GameObject @object)
    {
        if (!activeSwitch)
        {
            toggleOpposers?.Invoke();

            activeSwitch = true;
        }
    }

    public IEnumerator ToggleCooldown()
    {
        throw new System.NotImplementedException();
    }

    public void ToggleResponse()
    {
        if (activeSwitch)
        {
            triggerDeactivate?.Invoke();

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
    public virtual void Activate(GameObject instigator)
    { }


    //If the switch object was a remote controller for another object, here you would call whatever event that
    //the other object would listen to. Otherwise here you would put functionality to make something happen,
    //e.g a door would re-lock itself.
    public virtual void Deactivate()
    { }
}
