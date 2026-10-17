using DG.Tweening;
using UnityEngine;

public class ToggleableDoor : SingleLinkSwitch
{
    [Header("Door Attributes")]
    [SerializeField] Transform door;
    [SerializeField] float animationDuration = 0.0f;
    [SerializeField] float animationDistance;

    private Vector3 defaultPos;
    private Vector3 doorAnim;
    private Tween doorMovement;

    private void Awake()
    {
        doorAnim = new Vector3(0, 0, 0);
        defaultPos = transform.position;
    }

    public override void Deactivate()
    {
        doorMovement = door.DOLocalMoveY(defaultPos.y, animationDuration);
    }

    public override void Activate()
    {
        doorMovement = door.DOLocalMoveY(animationDistance, animationDuration);
    }
}
