using UnityEngine;
using UnityEngine.Events;

public class ActivateAllTracker : MonoBehaviour
{
    public UnityEvent OnAllActive;
    public int switchesToTrack = 0;
    private int tracker = 0;

    public void IncrementTracker()
    {
        tracker++;

        if (tracker == switchesToTrack)
        {
            OnAllActive?.Invoke();
        }
    }

    public void DecrementTracker()
    {
        tracker--;
    }
}
