using UnityEngine;
using UnityEngine.SceneManagement;

public class Banisher : MonoBehaviour
{
    [SerializeField] string defaultLevel = "EncounterHub";
    Transform checkpoint;

    public void Banish()
    {
        if (checkpoint != null)
        {
            transform.position = checkpoint.position;
            Physics.SyncTransforms();
        }
        else
        {
            SceneManager.LoadScene(defaultLevel);
        }
    }

    public void SetCheckpoint(Transform newCheckpoint)
    {
        checkpoint = newCheckpoint;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Banisher"))
        {
            Banish();
        }
    }
}
