using System;
using UnityEngine;

public class Stunner : Interaction
{
    public Stunner twinStunner;

    public static event Action deactivateResponders;
    public static event Action reactivateResponders;


    private void OnEnable()
    {
        OnLongInteraction += Stun;
        OnLongInteraction += DrainPower;
    }

    private void Stun(GameObject insigator, InteractionPhase phase, float delta)
    {
        //stun player when interaction is in complete phase
        if (phase == InteractionPhase.COMPLETED)
        {

        }
    }

    private void DrainPower(GameObject instigator, InteractionPhase phase, float delta)
    {
        //Do different stuff based on current phase. start a cooldown before insigating the renew power event.
        deactivateResponders?.Invoke();
    }

    


}
