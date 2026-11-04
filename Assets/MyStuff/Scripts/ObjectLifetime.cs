using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class ObjectLifetime : MonoBehaviour
{
    //Especially useful for objects used in a pooling spawner.
    public float lifetime;
    private float lifetimeTimer;
    private bool needed = false;

    private void OnEnable()
    {
        lifetimeTimer = lifetime;
        needed = true;
    }

    private void Update()
    {
        if (needed)
        {
            lifetimeTimer -= Time.deltaTime;
        }
        
        if ( lifetimeTimer <= 0 )
        {
            this.gameObject.SetActive( false );
            lifetimeTimer = 0;
        }
    }

    private void OnDisable()
    {
        needed = false;
    }
}
