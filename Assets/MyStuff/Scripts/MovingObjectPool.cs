using System.Collections.Generic;
using UnityEngine;

public class MovingObjectPool : MonoBehaviour
{
    //Use this or allow platforms to handle their lifetime.
    private BoxCollider killzone;

    [Header("Spawner Details")]
    [SerializeField] private bool flipState = true;
    [SerializeField] private bool active = false;
    [SerializeField] private MovingObjectPool oppositeSpawner;
    [Tooltip("Drag in any prefab with the 'EndlessMovement' script attached")][SerializeField] private EndlessMovement objectToPool;
    [Tooltip("Number of objects in the spawning pool")][SerializeField] private int initialPoolLimit;
    private List<EndlessMovement> pool = new();
    private int index = 0;

    //[Tooltip("Leave at 0 for consistent start rotation of spawned objects")][SerializeField] private float spawnRotationOffset;
    //[Tooltip("Leave at 0 for consistent start location of spawned objects")][SerializeField] private float spawnLocationOffset;

    private float spawnTimer;
    [Tooltip("Objects will spawn this many seconds apart, also acts as initial delay")][SerializeField] private float spawnDelay;

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

        //Ties movement direction of objects to activeState, therefore meaning the spawner this one is tied to
        //will not send its objects in the same direction by default as this one as long as it's bool value is opposite.
        if (!flipState)
        {
            foreach (var obj in pool)
            {
                obj.FlipDirection();
            }
        }
    }

    void Start()
    {
        
    }

    private EndlessMovement CreateObjectInPool()
    {
        //Instantiate() returns a reference to what it creates so we can pass this value back out.
        EndlessMovement objectCreated = Instantiate(objectToPool, transform);
        pool.Add(objectCreated);
        return objectCreated;
    }

    private void EnableObjectFromPool()
    {
        //Might not need to return an object but keeping it as an option.
        EndlessMovement newObject;

        //Cycle through list from whatever current point in it is ('index'), back to said starting point, looking
        //for a disabled object that we can enable. 
        for (int i = 0; i < pool.Count; i++)
        {
            if (pool[index].gameObject.activeSelf == false)
            {
                Debug.Log("Found inactive object");

                pool[index].transform.position = this.transform.position;
                pool[index].gameObject.SetActive(true);
            }

            index++;

            if (index >= pool.Count)
            {
                index = 0;
            }
        }

        newObject = CreateObjectInPool();
        newObject.transform.position = this.transform.position;
        newObject.gameObject.SetActive(true);


    }

    private void FlipState()
    {
        //If any are currently 'spawned' then switch their movement direction.
        foreach (var obj in pool)
        {
            if (obj.gameObject.activeInHierarchy == true)
            {
                obj.FlipDirection();
            }
        }

        active = !active; 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == objectToPool)
        {
            other.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (spawnTimer == 0 && active)
        {
            EnableObjectFromPool();
            spawnTimer = spawnDelay;
        }
    }
}
