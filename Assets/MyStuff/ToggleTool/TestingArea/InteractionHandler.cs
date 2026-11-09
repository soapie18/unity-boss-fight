using System;
using UnityEngine;

public class InteractionHandler : Interaction
{
    [SerializeField] LinkedSwitch toggleableSwitch;
    // public bool interactionAvailable;

    private void OnEnable()
    {
        OnShortInteraction += TriggerInteractionOccurred;
        OnLongInteraction += CheckInteractionComplete;
    }

    private void CheckInteractionComplete(GameObject instigator, InteractionPhase phase, float arg3)
    {
        if(phase == InteractionPhase.COMPLETED)
        {
            TriggerInteractionOccurred(instigator);
        }
    }

    private void OnDisable()
    {
        OnShortInteraction -= TriggerInteractionOccurred;
    }

    private void TriggerInteractionOccurred(GameObject instigator)
    {
        //if (interactionAvailable)
        //{
            toggleableSwitch.Triggered?.Invoke();
        //}
    }

    public void ResetInteractableState()
    {
        //interactionAvailable = true;
    }
}
