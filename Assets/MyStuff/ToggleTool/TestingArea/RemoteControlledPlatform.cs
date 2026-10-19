using DG.Tweening;
using UnityEngine;

public class RemoteControlledPlatform : MonoBehaviour
{
    [Header("Platform Attributes")]
    [SerializeField] private ToggleRemote remoteController;
    [SerializeField] private Transform platform;
    [SerializeField] private float animationDuration = 0.0f;
    [SerializeField] private float animationDistance;
    [SerializeField] private AxisOfMovement axisOfMovement;

    private Vector3 defaultPos;
    private Vector3 newPos;
    private Tween platformMovement;

    private void Awake()
    {
        defaultPos = platform.localPosition;
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
        platformMovement = platform.DOLocalMove(newPos, animationDuration);
    }

    private void returnPlatform()
    {
        platformMovement = platform.DOLocalMove(defaultPos, animationDuration);
    }
}

public enum AxisOfMovement
{ 
    Y,
    X,
    Z
}

