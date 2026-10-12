using System;
using UnityEngine;

public class Stunner : Interaction
{
    //TO DO upgrade this class to be aware of multiple stunners at a time via an array.

    [Header("References")]
    public Stunner twinStunner;
    [SerializeField] private StatusEffectData effectData;

    [Header("Stunner")]
    private float totalStunTime;
    private float currentStunTime = 0.0f;
    private bool activeStunner = false;

    private float currentInteractionCooldown = 0.0f;
    private bool stunned = false;

    public event Action deactivateResponders;
    public event Action reactivateResponders;

    public event Action flipTwinStunner;


    private void Awake()
    {
        totalStunTime = effectData.duration;
    }

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
        //Checking if intigator implements the script I made for the player. 
        var player = insigator.GetComponent<PlayerStun>();

        //Stun player when interaction is complete.
        if (player != null && !player.isCharged && phase == InteractionPhase.COMPLETED)
        {
            print("Applying Stun to player");
            //The script in question implements the IEffectable interface which is how it has this function
            //implemented.
            player.ApplyEffect(effectData);
        }

        //Resetting cooldown to stop repeated interaction.
        if (currentInteractionCooldown <= 0.0f)
        {
            currentInteractionCooldown = effectData.duration;
        }
    }

    private void DrainPower(GameObject instigator, InteractionPhase phase, float delta)
    {
        if (!activeStunner && phase == InteractionPhase.STARTED ||  phase == InteractionPhase.CONTINUED)
        {
            //TO DO: Do some feedback to player e.g a growing electricity particle effect or some sparks.
        }
        if (!activeStunner && phase == InteractionPhase.COMPLETED)
        {
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
            print("The twinned stunner was already active but it has been disabled");
            reactivateResponders?.Invoke();

            activeStunner = false;
        }

        print("The twin stunner was not active");
    }

    void Update()
    {
        //Cooldown stuff for powered objects reactivation.
        if (currentStunTime > 0.0f)
        {
            currentStunTime -= Time.deltaTime;
        }

        if (currentStunTime <= 0.0f && activeStunner)
        {
            activeStunner = false;
            reactivateResponders?.Invoke();
        }




        //Cooldown stuff for limiting more interaction while player is stunned exept its broken.
        if (currentInteractionCooldown > 0.0f)
        {
            //OnLongInteraction -= StunPlayer;

            currentInteractionCooldown -= Time.deltaTime;

            stunned = true;
        }

        if (stunned && currentInteractionCooldown <= 0.0f)
        {
            
            //OnLongInteraction += StunPlayer;

            stunned = false;
        }

    }


}
