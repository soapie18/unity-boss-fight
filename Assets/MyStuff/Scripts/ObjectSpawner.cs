using System;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [Header("Spawner Details")]
    public bool activeSpawner = false;
    public GameObject objectToSpawn;
    [Tooltip ("Objects will spawn this many seconds apart, also acts as initial delay")][SerializeField] private float spawnDelay;
    [Tooltip("Limit on how many objects can be spawned")][SerializeField] private int spawnLimit;


    private float spawnTimer;
    public event Action<GameObject> ObjectSpawned;
    

    private BoxCollider killzone;

    void Awake()
    {
        killzone = GetComponent<BoxCollider>(); 
    }

   void OnEnable()
    {
        spawnTimer = spawnDelay;
    }



    private void SpawnObject()
    {
        
    }

    void Update()
    {
        if (spawnTimer == 0)
        {
            
        }
    }

}
