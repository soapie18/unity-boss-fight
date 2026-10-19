using DG.Tweening;
using UnityEngine;

public class MultiLinkToggleTransform : MultiLinkSwitch
{
    [Header("Object Attributes")]
    [SerializeField] private Transform objectToMove;
    [SerializeField] private float animationDuration = 0.0f;
    [SerializeField] private float animationDistance;

    [SerializeField] private AxisOfMovement axisOfMovement;

    private Vector3 defaultPos;
    private Vector3 newPos;
    private Tween movementAnimation;



    private void Awake()
    {
        defaultPos = objectToMove.localPosition;
        newPos = defaultPos;
    }

    public void Start()
    {
        chooseAxis();
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
        movementAnimation = objectToMove.DOLocalMove(defaultPos, animationDuration);
    }

    public override void Activate(GameObject insigator)
    {
        print("object activated");
        movementAnimation = objectToMove.DOLocalMove(newPos, animationDuration);
    }
}
