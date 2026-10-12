using StarterAssets;
using UnityEngine;

public class PlayerStun : MonoBehaviour, IEffectable
{
    private StatusEffectData effectData;
    private float effectRunningTime = 0.0f;

    [Header("References")]
    [SerializeField] private ThirdPersonController playerController;

    [HideInInspector] public bool isCharged = false;
    private float defaultMoveSpeed;

    void Start()
    {
        defaultMoveSpeed = playerController.MoveSpeed;
        playerController.SprintSpeed = defaultMoveSpeed;
    }

    public void ApplyEffect(StatusEffectData data)
    {
        effectData = data;

        playerController.MoveSpeed *= effectData.movementPenalty;

        isCharged = true;
    }

    public void HandleEffect()
    {
        effectRunningTime += Time.deltaTime;

        if (effectRunningTime >= effectData.duration)
        {
            RemoveEffect();
        }
    }

    public void RemoveEffect()
    {
        effectData = null;

        playerController.MoveSpeed = defaultMoveSpeed;

        isCharged = false;
    }

    void Update()
    {
        if (effectData != null)
        {
            HandleEffect();
        }
    }
}
