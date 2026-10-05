using System;
using UnityEngine;

public class EndlessLongInteraction : Interaction
{
    [SerializeField] Transform lightMesh;
    [SerializeField] Light spotlight;

    private void OnEnable()
    {
        OnLongInteraction += Spin;
    }

    private void Spin(GameObject instigator, InteractionPhase phase, float delta)
    {
        switch (phase)
        {
            case InteractionPhase.CONTINUED:
                spotlight.intensity = 10;
                lightMesh.Rotate(0, 60 * delta, 0, Space.World);
                break;
            case InteractionPhase.CANCELLED:
                spotlight.intensity = 0;
                break;
            default:
                break;
        }
    }
}
