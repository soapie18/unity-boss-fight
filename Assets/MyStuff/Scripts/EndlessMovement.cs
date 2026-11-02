using DG.Tweening;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Rendering;


public class EndlessMovement : MonoBehaviour, ISpawnable
{
    [SerializeField] private Transform objectToMove;
    private float movementSpeed;
    [SerializeField]private float defaultMovementSpeed = 5;
    [SerializeField] private EAxisOfMovement axisOfMovement;
    private Vector3 direction;

    private void Awake()
    {
        movementSpeed = defaultMovementSpeed;
    }

    void ChooseAxis()
    {
        switch (axisOfMovement)
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

    void OnEnable()
    {
        ChooseAxis();
    }

    public void FlipDirection()
    {
        Debug.Log("Flipping direction of movement");
        movementSpeed *= -1;

        ChooseAxis();
    }
    
    public void FlipDirectionToDefault()
    {
        movementSpeed = defaultMovementSpeed;
    }

    void Update()
    {
        objectToMove.Translate(direction * Time.deltaTime, Space.Self);
    }

    private void OnDisable()
    {
        movementSpeed = defaultMovementSpeed;
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
