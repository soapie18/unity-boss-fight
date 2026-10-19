using DG.Tweening;
using UnityEngine;

public class ToggleableDoor : SingleLinkSwitch
{
    [Header("Door Attributes")]
    [SerializeField] private Transform door;
    [SerializeField] private float animationDuration = 0.0f;
    [SerializeField] private float animationDistance;

    private float defaultY = 0.0f;
    private Tween doorMovement;

    private void Awake()
    {
        defaultY = transform.position.y;
    }

    public override void Deactivate()
    {
        doorMovement = door.DOLocalMoveY(defaultY, animationDuration);
    }

    public override void Activate(GameObject insigator)
    {
        doorMovement = door.DOLocalMoveY(defaultY + animationDistance, animationDuration);
    }
}
