using System.Collections.Generic;
using UnityEngine;

public class MovingObjectSpawner : ObjectSpawner
{
    [Header("Movement Details")]

    [Tooltip("What direction should the objects spawned here move in?")][SerializeField] private EAxisOfMovement axisOverride;
    [Tooltip("All objects will pull from this value for their speed and not their own value.")][SerializeField] private float movementSpeedOverride;
    [HideInInspector] public Vector3 direction;

    //private MovingObjectSpawner primarySpawner;
    //private MovingObjectSpawner secondarySpawner;
    //private MovingObjectSpawner currentSpawner;


    //In order to have different axis for different spawn points its probably easier to have a reference to a secondary spawner with its own
    //axisOverride value. 

    protected override void Awake()
    {
        base.Awake();

        foreach (var obj in pool)
        {
            obj.TryGetComponent<EndlessMovement>(out EndlessMovement mover);
            if (mover != null)
            {
                mover.spawner = this;
            }
        }
    }

    private void Start()
    {
        SetDirectionSpawnerUses();

        //primarySpawner = primarySpawnPoint as MovingObjectSpawner;
        //secondarySpawner = secondarySpawnPoint as MovingObjectSpawner;
        //currentSpawner = currentSpawnPoint as MovingObjectSpawner;
    }

    private void SetDirectionSpawnerUses()
    {
        switch (axisOverride)
        {
            case EAxisOfMovement.X:
                direction = new Vector3(movementSpeedOverride, 0, 0);
                break;
            case EAxisOfMovement.Y:
                direction = new Vector3(0, movementSpeedOverride, 0);
                break;
            case EAxisOfMovement.Z:
                direction = new Vector3(0, 0, movementSpeedOverride);
                break;
            default:
                break;
        }

    }

    public void FlipDirectionUsedInSpawner()
    {
        direction *= -1;
    }

    //override public void SwitchSpawner()
    //{
    //    if (primarySpawner is MovingObjectSpawner && secondarySpawner is MovingObjectSpawner && currentSpawner is MovingObjectSpawner)
    //    {
    //        switch (spawnerInUse)
    //        {
    //            case ESpawnerInUse.Primary:
    //                currentSpawner = secondarySpawner;
    //                spawnerInUse = ESpawnerInUse.Secondary;
    //                primarySpawner.active = false;
    //                secondarySpawner.active = true;
    //                break;
    //            case ESpawnerInUse.Secondary:
    //                currentSpawner = primarySpawner;
    //                spawnerInUse = ESpawnerInUse.Primary;
    //                secondarySpawner.active = false;
    //                primarySpawner.active = true;
    //                break;
    //            default:
    //                break;
    //        }
    //    }
    //}

    protected override void Update()
    {
        base.Update();
    }

}
