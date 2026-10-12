using System;
using UnityEngine;

public class Stunner : Interaction
{
    [Header("References")]
    public Stunner twinStunner;

    [Header("Stunner")]
    [SerializeField]private float totalStunTime;
    private float currentStunTime;

    private bool activeStunner = false;

    public event Action deactivateResponders;
    public event Action reactivateResponders;

    public event Action stunPlayer;

    public event Action flipTwinStunner;

    

    private void OnEnable()
    {
        OnLongInteraction += StunPlayer;
        OnLongInteraction += DrainPower;
        
        if (twinStunner)
        {
            twinStunner.flipTwinStunner += ToggleResponders;
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
            stunPlayer?.Invoke();
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

            flipTwinStunner?.Invoke();

            //TO DO: Play a sound effect for powering down.
        }

        //Resetting cooldown.
        if (currentStunTime <= 0.0f)
        {
            currentStunTime = totalStunTime;
        }
      
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




    void Update()
    {
        if (currentStunTime >= 0.0f)
        {
            currentStunTime -= Time.deltaTime;
        }

        if (currentStunTime <= 0.0f && activeStunner)
        {
            activeStunner = false;
            reactivateResponders?.Invoke();
        }
    }

}
