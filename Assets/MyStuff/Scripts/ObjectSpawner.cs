using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;


public class ObjectSpawner : MonoBehaviour
{
    [Header("Spawner Details")]
    [SerializeField] private bool activeState = true;
    [SerializeField] private ObjectSpawner twinSpawner;
    [Tooltip ("Object to spawn MUST implement ISpawnable interface")][SerializeField] private GameObject objectToPool;
    [Tooltip("Number of objects in the spawning pool")][SerializeField] private int initialPoolLimit;
    private List<GameObject> pool = new();
    private int index = 0;

    [Tooltip ("Objects will spawn this many seconds apart, also acts as initial delay")][SerializeField] private float spawnDelay;
    [Tooltip("Leave at 0 for consistent start rotation of spawned objects")][SerializeField] private float spawnRotationOffset;
    [Tooltip("Leave at 0 for consistent start location of spawned objects")][SerializeField] private float spawnLocationOffset;
 
    private float spawnTimer;

    GameObject latestObjectEnabled;
    
    //Use this or allow objects that the pool spawns to handle their lifetime.
    private BoxCollider killzone;

    void Awake()
    {
        killzone = GetComponent<BoxCollider>(); 

        for (int i = 0; i < initialPoolLimit; i++)
        {
            CreateObjectInPool();
        }
    }

   void OnEnable()
    {
        spawnTimer = spawnDelay;
    }

    private void FlipState()
    {
        activeState = !activeState;

        for (int i = 0;i < pool.Count;i++)
        {
            //Check if obj implements ISpawnable and call FlipState on it
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

    private GameObject EnableObjectFromPool(float rotationOffset, float locationOffset)
    {
        //Might not need to return an object but keeping it as an option.
        GameObject newObject;

        //Cycle through list from whatever current point in it is ('index'), back to said starting point, looking
        //for a disabled object that we can enable. 
        for (int i = 0; i < pool.Count; i++)
        {
            if (pool[index].activeInHierarchy == false)
            {
                newObject = pool[index];
                newObject.transform.position = this.transform.position;
                newObject.SetActive(true);

                //If aren't 0 use rotation offset and location offset to modify the transform of this object, based on
                //'latestObjectEnabled' transform + the offset value. Its so that you can have say bullet hell spiral type
                //patterns for drone spawning later down the line.

                //latestObjectEnabled = newObject;

                return newObject;
            }

            index++;

            if (index >= pool.Count)
            {
                index = 0;
            }
        }

        newObject = CreateObjectInPool();
        newObject.transform.position = this.transform.position;
        newObject.SetActive(true);

        //If aren't 0 use rotation offset and location offset to modify the transform of this object, based on
        //'latestObjectEnabled' transform + the offset value. Its so that you can have say bullet hell patterns
        //for drone spawning later down the line.

        //latestObjectEnabled = newObject;

        return newObject;
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == objectToPool)
        {
            other.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (spawnTimer == 0)
        {
            EnableObjectFromPool(spawnRotationOffset, spawnLocationOffset);
            spawnTimer = spawnDelay;
        }
    }

}
