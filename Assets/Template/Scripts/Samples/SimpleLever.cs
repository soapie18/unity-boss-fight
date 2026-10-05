using System;
using UnityEngine;

/// <summary>
/// A simple script to flip the rotation of a specified object after a Short Interaction.
/// </summary>
public class SimpleLever : Interaction
{
    [Space(10)]
    [Header("Lever Details")]
    [SerializeField] Transform lever;

    private void OnEnable()
    {
        OnShortInteraction += Toggle;
    }

    private void OnDisable()
    {
        OnShortInteraction -= Toggle;
    }

    private void Toggle(GameObject instigator)
    {
        if (lever != null)
        {
            lever.localEulerAngles *= -1;
        }
    }
}
