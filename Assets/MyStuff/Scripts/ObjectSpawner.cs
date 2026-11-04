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

    [SerializeField] protected bool active = true;

    [Tooltip("Object to spawn MUST implement ISpawnable interface for toggleable functionality to work.")][SerializeField] protected GameObject objectToPool;
    [Tooltip("Number of objects in the spawning pool.")][SerializeField] protected int initialPoolLimit;
    protected List<GameObject> pool = new();
    protected int index = 0;

    [Tooltip("Objects will spawn this many seconds apart, also acts as initial delay.")][SerializeField] protected float spawnDelay = 5;
    protected float spawnCountdown;

    [Header("Paired Spawner Details")]

    [SerializeField] protected ESpawnerInUse spawnerInUse = ESpawnerInUse.Primary;
    protected ObjectSpawner currentSpawnPoint;
    [Tooltip("Primary should == the same GameObject accross both spawners involveds' fields. Don't assume this == Primary.")][SerializeField] protected ObjectSpawner primarySpawnPoint;
    [Tooltip("Secondary should == the same GameObject accross both spawners involveds' fields.")][SerializeField] protected ObjectSpawner secondarySpawnPoint;

    //Use this but also allow objects that the pool spawns to handle their lifetime if necessary.
    protected BoxCollider killzone;


    virtual protected void Awake()
    {
        killzone = GetComponent<BoxCollider>();

        for (int i = 0; i < initialPoolLimit; i++)
        {
            CreateObjectInPool();
        }

        currentSpawnPoint = primarySpawnPoint;

        spawnCountdown = spawnDelay;
    }

    public void TurnOffSpawner()
    {
        active = false;
    }

    public void TurnOnSpawner()
    {
        active = true;
    }

    virtual public void SwitchSpawner()
    {
        switch (spawnerInUse)
        {
            case ESpawnerInUse.Primary:
                currentSpawnPoint = secondarySpawnPoint;
                spawnerInUse = ESpawnerInUse.Secondary;
                primarySpawnPoint.active = false;
                secondarySpawnPoint.active = true;
                break;
            case ESpawnerInUse.Secondary:
                currentSpawnPoint = primarySpawnPoint;
                spawnerInUse = ESpawnerInUse.Primary;
                secondarySpawnPoint.active = false;
                primarySpawnPoint.active = true;
                break;
            default:
                break;
        }
    }


    public void ISpawnableToggleResponse()
    {
        foreach (var obj in pool)
        {
            if (obj.TryGetComponent<ISpawnable>(out var spawnable))
            {
                spawnable.OnToggleOfSpawner();
            }
        }
    }

    public void ISpawnableToggleResponseAlt()
    {
        foreach (var obj in pool)
        {
            if (obj.TryGetComponent<ISpawnable>(out var spawnable))
            {
                spawnable.OnToggleOfSpawnerAlt();
            }
        }
    }


    protected GameObject CreateObjectInPool()
    {
        //Instantiate() returns a reference to what it creates so we can pass this value back out.
        GameObject objectCreated = Instantiate(objectToPool, transform);
        objectCreated.SetActive(false);
        pool.Add(objectCreated);
        return objectCreated;
    }

    protected GameObject EnableObjectFromPool()
    {
        //Cycle through list from whatever current point in it is ('index'), back to said starting point, looking
        //for a disabled object that we can enable. 
        for (int i = 0; i < pool.Count; i++)
        {
            if (pool[index].activeInHierarchy == false)
            {
                //Enabling object found.
                pool[index].transform.position = this.transform.position;
                pool[index].SetActive(true);

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

        return newObject;
        
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (pool.Contains(other.gameObject))
        {
            other.gameObject.SetActive(false);
        }
    }

    virtual protected void Update()
    {
        spawnCountdown -= Time.deltaTime;

        if (spawnCountdown <= 0 && active)
        {
            EnableObjectFromPool();
            spawnCountdown = spawnDelay;
        }
    }

}
