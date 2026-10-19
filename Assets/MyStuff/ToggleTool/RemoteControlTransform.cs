using DG.Tweening;
using UnityEngine;

public class RemoteControlTransform : MonoBehaviour
{
    [Header("Object Attributes")]
    [SerializeField] private ToggleRemote remoteController;
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

    private void Start()
    {
        chooseAxis();
    }

    private void OnEnable()
    {
        remoteController.activateListener += movePlatform;
        remoteController.deactivateListener += returnPlatform;
    }

    private void OnDisable()
    {
        remoteController.activateListener -= movePlatform;
        remoteController.deactivateListener -= returnPlatform;
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

    private void movePlatform()
    {
        movement = objectToMove.DOLocalMove(newPos, animationDuration);
    }

    private void returnPlatform()
    {
        movement = objectToMove.DOLocalMove(defaultPos, animationDuration);
    }
}
