using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;


public enum ESpawnerInUse
{ 
    Primary,
    Secondary
}


public class ObjectSpawner : MonoBehaviour
{
    [Header("Spawner Details")]
    [SerializeField] private bool active = true;
    [SerializeField] private ESpawnerInUse spawnerInUse = ESpawnerInUse.Primary;
    private Transform currentSpawner;
    [SerializeField] private Transform primarySpawnPoint;
    [SerializeField] private Transform secondarySpawnpoint;
    [Tooltip ("Object to spawn MUST implement ISpawnable interface")][SerializeField] private GameObject objectToPool;
    [Tooltip("Number of objects in the spawning pool")][SerializeField] private int initialPoolLimit;
    private List<GameObject> pool = new();
    private int index = 0;

    [Tooltip ("Objects will spawn this many seconds apart, also acts as initial delay")][SerializeField] private float spawnDelay = 5;
    private float spawnCountdown;
    
    //Use this or allow objects that the pool spawns to handle their lifetime.
    private BoxCollider killzone;


    void Awake()
    {
        killzone = GetComponent<BoxCollider>();

        for (int i = 0; i < initialPoolLimit; i++)
        {
            CreateObjectInPool();
        }

        currentSpawner = primarySpawnPoint.transform;

        spawnCountdown = spawnDelay;
    }


    private void ChangeCurrentSpawnPoint()
    {
        switch (spawnerInUse)
        {
            case ESpawnerInUse.Primary:
                currentSpawner = secondarySpawnpoint.transform;
                break;
            case ESpawnerInUse.Secondary:
                currentSpawner = primarySpawnPoint.transform;
                break;
            default:
                break;
        }

    }

    public void TurnOffSpawner()
    {
        active = false;
    }

    public void TurnOnSpawner()
    {
        active = true;
    }

    public void SwitchSpawnPoint()
    {
        ChangeCurrentSpawnPoint();

        foreach(var obj in pool)
        {
            if(obj.TryGetComponent<ISpawnable>(out var spawnable))
            {
                Debug.Log("Fired interface func");
                spawnable.OnToggleOfSpawner();
            }
        }
        
    }

    private GameObject CreateObjectInPool()
    {
        //Instantiate() returns a reference to what it creates so we can pass this value back out.
        GameObject objectCreated = Instantiate(objectToPool, transform);
        objectCreated.SetActive(false);
        pool.Add(objectCreated);
        return objectCreated;
    }

    private GameObject EnableObjectFromPool()
    {
        Debug.Log("Enabling...");

        //Cycle through list from whatever current point in it is ('index'), back to said starting point, looking
        //for a disabled object that we can enable. 
        for (int i = 0; i < pool.Count; i++)
        {
            if (pool[index].activeInHierarchy == false)
            {
                //Enabling object found
                pool[index].transform.position = this.transform.position;
                pool[index].SetActive(true);

                Debug.Log("Object enabled");

                return pool[index];
            }

            index++;

            //If full array is cycled through, reset index.
            if (index >= pool.Count)
            {
                index = 0;
            }
        }

        GameObject newObject;

        //Creating an object in pool because no objects were available.
        newObject = CreateObjectInPool();
        newObject.transform.position = this.transform.position;
        newObject.SetActive(true);
        Debug.Log("Object enabled");

        return newObject;
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("collision event fired");

        if (pool.Contains(other.gameObject))
        {
            Debug.Log("Correct object collided");
            other.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        spawnCountdown -= Time.deltaTime;

        if (spawnCountdown <= 0 && active)
        {
            EnableObjectFromPool();
            spawnCountdown = spawnDelay;
        }
    }

}
