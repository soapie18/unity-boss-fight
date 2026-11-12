using UnityEngine;
using UnityEngine.Events;

public class SimpleShortInteraction : Interaction
{
    public UnityEvent OnInteraction;

    private void OnEnable()
    {
        OnShortInteraction += OnInteract;
    }

    private void OnDisable()
    {
        OnShortInteraction -= OnInteract;
    } 

    private void OnInteract(GameObject insitgator)
    {
        OnInteraction?.Invoke();
    }  
}
