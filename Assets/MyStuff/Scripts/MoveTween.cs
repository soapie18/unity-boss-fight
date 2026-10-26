using DG.Tweening;
using UnityEngine;
using UnityEngine.UIElements;

public class MoveTween : MonoBehaviour
{
    [Header("Tween Attributes")]
    [SerializeField] private Transform objectToMove;
    [SerializeField] private float animationDuration = 0.0f;
    [SerializeField] private float animationDistance;
    [SerializeField] private EAxisOfMovement axisOfMovement;
    private Tween movement;

    //defaults
    private Vector3 defaultPos;
    private Vector3 newPos;
    

    void Awake()
    {
        defaultPos = objectToMove.localPosition;
        newPos = defaultPos;
    }

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

    public void DecideMovementNeeded()
    {
        if (objectToMove.position == newPos)
        {
            ReturnObject();
            return;
        }

        if (objectToMove.position == defaultPos)
        {
            MoveObject();
            return;
        }

        
        
        Debug.Log("object was still moving");
        return;
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
