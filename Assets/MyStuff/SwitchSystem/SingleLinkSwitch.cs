using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class SingleLinkSwitch : LinkedSwitch
{
    [Header("Switch Links")]
    public SingleLinkSwitch twinSwitch;

    
    private void OnEnable()
    {
        if (twinSwitch)
        {
            twinSwitch.ToggleOpposers += ToggleResponse;
        }

        if (interactable)
        {
            Triggered += DetermineState;
            Triggered += TriggerToggleOpposer;
        }

        if (onTimer)
        {
            Triggered += ResetTimer;
        }

    }

    private void OnDisable()
    {
        if (twinSwitch)
        {
            twinSwitch.ToggleOpposers -= ToggleResponse;
        }

        if (interactable)
        {
            Triggered -= DetermineState;
            Triggered -= TriggerToggleOpposer;
        }

        if (onTimer)
        {
            Triggered -= ResetTimer;
        }
    }




}
