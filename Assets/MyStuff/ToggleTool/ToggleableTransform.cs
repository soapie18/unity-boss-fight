using DG.Tweening;
using UnityEngine;

public class ToggleableTransform : SingleLinkSwitch
{
    [Header("Object Attributes")]
    [SerializeField] private Transform objectToMove;
    [SerializeField] private float animationDuration = 0.0f;
    [SerializeField] private float animationDistance;

    [SerializeField] private AxisOfMovement axisOfMovement;

    private Vector3 defaultPos;
    private Vector3 newPos;
    private Tween objectMovement;



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
        objectMovement = objectToMove.DOLocalMove(defaultPos, animationDuration);
    }

    public override void Activate(GameObject insigator)
    {
        print("object activated");
        objectMovement = objectToMove.DOLocalMove(newPos, animationDuration);
    }
}
