using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MultiLinkSwitch : LinkedSwitch
{
    [Header("Switch Links")]
    public List<MultiLinkSwitch> linkedSwitches = new();


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

        if (onTimer)
        {
            Triggered += ResetTimer;
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

        if (onTimer)
        {
            Triggered -= ResetTimer;
        }

    }


}

