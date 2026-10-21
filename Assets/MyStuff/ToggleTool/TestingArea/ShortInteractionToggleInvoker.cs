using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class ShortInteractionToggleInvoker : Interaction
{
    [Header("Interaction Event")]
    public GameEvent InteractionEvent;

    private void OnEnable()
    {
        OnShortInteraction += TriggerRaiseEvent;
    }

    private void OnDisable()
    {
        OnShortInteraction -= TriggerRaiseEvent;
    }

    private void TriggerRaiseEvent(GameObject instigator)
    {
        InteractionEvent.RaiseEvent();
    }
}
