using UnityEngine;

public class ToggleableBanisher : MonoBehaviour
{
    private Collider banishingTrigger;

    private void Awake()
    {
        banishingTrigger = GetComponent<Collider>();
    }

    public void SetTriggerInactive()
    {
        banishingTrigger.enabled = false;
    }

    public void SetTriggerActive()
    {
        banishingTrigger.enabled = true;
    }
}
