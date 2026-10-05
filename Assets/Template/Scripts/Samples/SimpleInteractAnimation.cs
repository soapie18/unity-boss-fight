using UnityEngine;

/// <summary>
/// A simple, reusable script that will trigger an Animator when the player uses a Short Interaction.
/// </summary>
public class SimpleInteractAnimation : Interaction
{
    [Space(10)]
    [Header("Animator Details")]
    [SerializeField] Animator animator;
    [SerializeField] string trigger = "Interacted";

    private void OnEnable()
    {
        OnShortInteraction += Animate;
    }

    private void OnDisable()
    {
        OnShortInteraction -= Animate;
    }

    private void Animate(GameObject instigator)
    {
        if (animator != null)
        {
            animator.SetTrigger(trigger);
        }
    }
}
