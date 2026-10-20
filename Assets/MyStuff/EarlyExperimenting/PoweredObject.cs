using UnityEngine;

public class PoweredObject : MonoBehaviour
{

    public Stunner stunner;

    private void OnEnable()
    {
        stunner.deactivateResponders += OnDeactivate;
        stunner.reactivateResponders += OnReactivate;
    }

    private void OnDisable()
    {
        stunner.deactivateResponders -= OnDeactivate;
        stunner.reactivateResponders -= OnReactivate;
    }

    private void OnDeactivate()
    {
        Deactivate();
    }

    private void OnReactivate()
    {
        Reactivate();
    }

    public virtual void Deactivate() { }
    public virtual void Reactivate() { }

}

