using DG.Tweening;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Rendering;


public class EndlessMovement : MonoBehaviour, ISpawnable
{
    [SerializeField] private Transform objectToMove;
    public float movementSpeed;
    public EAxisOfMovement axisOfMovement;
    private Vector3 direction;

    [HideInInspector] public MovingObjectSpawner spawner;


    private void Awake()
    {
        ChooseAxis(axisOfMovement);
    }

    public void ChooseAxis(EAxisOfMovement axis)
    {
        switch (axis)
        {
            case EAxisOfMovement.X:
                direction = new Vector3(movementSpeed, 0, 0);
                break;
            case EAxisOfMovement.Y:
                direction = new Vector3(0, movementSpeed, 0);
                break;
            case EAxisOfMovement.Z:
                direction = new Vector3(0, 0, movementSpeed);
                break;
        }
    }

    public void FlipDirection()
    {
        direction *= -1;
    }

    void Update()
    {
        if(spawner)
        {
            objectToMove.Translate(spawner.direction * Time.deltaTime, Space.Self);
        }

        else
        {
            objectToMove.Translate(direction * Time.deltaTime, Space.Self);
        }
            
    }

    public void OnToggleOfSpawner()
    {
        FlipDirection();
    }

    public void OnToggleOfSpawnerAlt()
    {
        return;
    }

    
}
