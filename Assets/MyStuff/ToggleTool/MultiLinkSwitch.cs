using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MultiLinkSwitch : Interaction
{
    [Header("Switch Attributes")]
    public List<MultiLinkSwitch> linkedSwitches;

    public event Action ToggleOpposers;

    [SerializeField] private bool interactable = false;
    [SerializeField] private bool timed = false;

    private bool activeSwitch = false;

    [Header("Toggle Events")]
    public UnityEvent OnSwitchActivate;
    public UnityEvent OnSwitchDeactivate;


    private void OnEnable()
    {
        foreach (MultiLinkSwitch s in linkedSwitches)
        {
            s.ToggleOpposers += ToggleResponse;
        }

        if (interactable)
        {
            OnShortInteraction += Activate;
            OnShortInteraction += TriggerToggleOpposer;
        }

    }

    private void OnDisable()
    {
        foreach (MultiLinkSwitch s in linkedSwitches)
        {
            s.ToggleOpposers -= ToggleResponse;
        }


        if (interactable)
        {
            OnShortInteraction -= Activate;
            OnShortInteraction -= TriggerToggleOpposer;
        }

    }

    private void TriggerToggleOpposer(GameObject @object)
    {
        if (!activeSwitch)
        {
            ToggleOpposers?.Invoke();

            activeSwitch = true;
        }
    }

    private IEnumerator ToggleCooldown()
    {
        throw new System.NotImplementedException();
    }

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


    private void Activate(GameObject instigator)
    {
        OnSwitchActivate?.Invoke();
    }


    private void Deactivate()
    {
        OnSwitchDeactivate?.Invoke();
    }

}

