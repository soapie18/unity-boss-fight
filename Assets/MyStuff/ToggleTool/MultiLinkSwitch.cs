using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MultiLinkSwitch : LinkedSwitch
{
    [Header("Switch Attributes")]
    public List<MultiLinkSwitch> linkedSwitches = new();

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
            Triggered += Activate;
            Triggered += TriggerToggleOpposer;
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
            Triggered -= Activate;
            Triggered -= TriggerToggleOpposer;
        }

    }

    private void TriggerToggleOpposer()
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
            //TODO: start cooldown
        }
    }


    private void Activate()
    {
        OnSwitchActivate?.Invoke();
    }


    private void Deactivate()
    {
        OnSwitchDeactivate?.Invoke();
    }

}

