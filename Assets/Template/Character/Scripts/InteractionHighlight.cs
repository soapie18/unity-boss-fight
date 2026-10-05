using System;
using UnityEngine;

public class InteractionHighlight : MonoBehaviour
{
    [SerializeField] Renderer renderer;
    Interactor interactor;
    MaterialPropertyBlock materialPropertyBlock;

    private void OnEnable()
    {
        if (renderer != null)
        {
            interactor = GetComponent<Interactor>();
            if (interactor != null)
            {
                materialPropertyBlock = new();
                renderer.SetPropertyBlock(materialPropertyBlock);
                interactor.OnInteractionEntered += ActivateHighlight;
                interactor.OnInteractionExited += DeactivateHighlight;
            }
        }
    }

    private void OnDisable()
    {
        if (interactor != null)
        {
            interactor.OnInteractionEntered -= ActivateHighlight;
            interactor.OnInteractionExited -= DeactivateHighlight;
        }
    }

    private void DeactivateHighlight(GameObject inst, Interaction interaction)
    {
        if (interactor != null && interactor.GetEnabledInteractions().Count < 1)
        {
            materialPropertyBlock.SetFloat("_ActivePower", 0);
            renderer.SetPropertyBlock(materialPropertyBlock);
        }
    }

    private void ActivateHighlight(GameObject inst, Interaction interaction)
    {
        if (interaction.Enabled)
        {
            materialPropertyBlock.SetFloat("_ActivePower", 1);
            renderer.SetPropertyBlock(materialPropertyBlock);
        }
    }
}
