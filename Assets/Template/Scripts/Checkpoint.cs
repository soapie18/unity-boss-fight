using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Check(other.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Check(collision.gameObject);
    }

    private void Check(GameObject toCheck)
    {
        if (toCheck.TryGetComponent<Banisher>(out var banisher))
        {
            banisher.SetCheckpoint(transform);
        }
    }
}
