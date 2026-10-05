using System;
using UnityEngine;

/// <summary>
/// A simple, albeit slightly useless, material change after a Long Interaction.
/// </summary>
public class SimpleLongInteraction : Interaction
{
    [Space(10)]
    [Header("Target Renderer Details")]
    [SerializeField] Renderer renderer;
    [SerializeField] Material[] materials;
    int index;

    private void OnEnable()
    {
        OnLongInteraction += SwitchMaterial;
    }

    private void OnDisable()
    {
        OnLongInteraction -= SwitchMaterial;
    }

    private void SwitchMaterial(GameObject instigator, InteractionPhase phase, float delta)
    {
        if (phase == InteractionPhase.COMPLETED)
        {
            index = (index + 1) % materials.Length;
            renderer.material = materials[index];
        }
    }
}
