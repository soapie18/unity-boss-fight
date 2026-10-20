using UnityEngine;

public class EndlessRotator : Interaction
{
    [Header("Rotation Details")]
    public Transform objectToRotate;
    public float speedOfRotation;

    private void OnEnable()
    {
        OnLongInteraction += RotateObject;
    }

    private void OnDisable()
    {
        OnLongInteraction -= RotateObject;
    }

    private void RotateObject(GameObject instigator, InteractionPhase phase, float delta)
    {
        switch (phase)
        {
            case InteractionPhase.CONTINUED:
                objectToRotate.Rotate(0, speedOfRotation * delta, 0, Space.World);
                RotationStarted();
                break;
            case InteractionPhase.CANCELLED:
                RotationStopped();
                break;
            default:
                break;
        }

    }

    public virtual void RotationStarted() { }
    public virtual void RotationStopped() { }
}
