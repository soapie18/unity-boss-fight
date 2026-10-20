using DG.Tweening;
using UnityEngine;

public class MoveTween : MonoBehaviour
{
    [Header("Tween Attributes")]
    [SerializeField] private Transform objectToMove;
    [SerializeField] private float animationDuration = 0.0f;
    [SerializeField] private float animationDistance;
    [SerializeField] private EAxisOfMovement axisOfMovement;

    private Vector3 defaultPos;
    private Vector3 newPos;
    private Tween movement;

    void Start()
    {
        DetermineAxis();
    }

    private void DetermineAxis()
    {
        switch (axisOfMovement)
        {
            case EAxisOfMovement.Y:
                newPos.y = defaultPos.y + animationDistance;
                break;
            case EAxisOfMovement.X:
                newPos.x = defaultPos.x + animationDistance;
                break;
            case EAxisOfMovement.Z:
                newPos.z = defaultPos.z + animationDistance;
                break;
        }
    }

    public void MoveObject()
    {
        movement = objectToMove.DOLocalMove(newPos, animationDuration);
    }

    public void ReturnObject()
    {
        movement = objectToMove.DOLocalMove(defaultPos, animationDuration);
    }
}
