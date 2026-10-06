using System;
using UnityEngine;

public class Stunner : Interaction
{
    [Header("References")]
    public Stunner twinStunner;

    private bool activeStunner = false;

    public event Action deactivateResponders;
    public event Action reactivateResponders;

    public event Action flipActiveStunner;

    

    private void OnEnable()
    {
        OnLongInteraction += StunPlayer;
        OnLongInteraction += DrainPower;
        
        if (twinStunner)
        {
            twinStunner.flipActiveStunner += ToggleResponders;
        }
       
    }

    private void OnDisable()
    {
        OnLongInteraction -= StunPlayer;
        OnLongInteraction -= DrainPower;
    }

    private void StunPlayer(GameObject insigator, InteractionPhase phase, float delta)
    {
        //Stun player when interaction is in complete phase
        if (phase == InteractionPhase.COMPLETED)
        {
            
        }
    }

    private void DrainPower(GameObject instigator, InteractionPhase phase, float delta)
    {
        if (phase == InteractionPhase.STARTED ||  phase == InteractionPhase.CONTINUED)
        {
            //TO DO: Do some feedback to player e.g a growing electricity particle effect or some sparks.
        }
        if (phase == InteractionPhase.COMPLETED)
        {
            print("Stunner interaction complete");
            deactivateResponders?.Invoke();

            activeStunner = true;

            flipActiveStunner?.Invoke();

            //TO DO: Play a sound effect for powering down.
        }

        //TO DO: start cooldown with variable time amount then reactivate responders and set activeStunner to false.
    }

    private void ToggleResponders()
    {
        if (activeStunner)
        {
            print("There was a stunner already active but it has been disabled");
            reactivateResponders?.Invoke();

            activeStunner = false;
        }

        print("There were no other stunners currently active");
    }


}
