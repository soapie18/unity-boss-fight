using UnityEngine;

public class MovingObjectSpawner : ObjectSpawner
{
    [SerializeField] private EAxisOfMovement axisOfMovement;
    [SerializeField] private float movementSpeedOverride;
    private Vector3 currentDirection;



}
