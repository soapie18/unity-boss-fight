using DG.Tweening;
using UnityEngine;
using static UnityEditorInternal.ReorderableList;

public class ToggleableDoor : SingleLinkSwitch
{
    [Header("Door Attributes")]
    [SerializeField] private Transform objectToMove;
    [SerializeField] private float animationDuration = 0.0f;
    [SerializeField] private float animationDistance;

    [SerializeField] private AxisOfMovement axisOfMovement;

    private Vector3 defaultPos;
    private Vector3 newPos;
    private Tween movement;



    private void Awake()
    {
        defaultPos = objectToMove.localPosition;
        newPos = defaultPos;
    }

    private void chooseAxis()
    {
        switch (axisOfMovement)
        {
            case AxisOfMovement.Y:
                newPos.y = defaultPos.y + animationDistance;
                break;
            case AxisOfMovement.X:
                newPos.x = defaultPos.x + animationDistance;
                break;
            case AxisOfMovement.Z:
                newPos.z = defaultPos.z + animationDistance;
                break;
        }
    }

    public override void Deactivate()
    {
        movement = objectToMove.DOLocalMove(defaultPos, animationDuration);
    }

    public override void Activate(GameObject insigator)
    {
        movement = objectToMove.DOLocalMove(newPos, animationDuration);
    }
}

