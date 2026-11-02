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

    void UpdateDirection()
    {
        
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

    private void Start()
    {
        ChooseAxis();
    }

    public void FlipDirection()
    {
        Debug.Log("Flipping direction of movement");
        direction *= -1;
    }
    
    public void FlipDirectionToDefault()
    {
        
    }

    void Update()
    {
        objectToMove.Translate(direction * Time.deltaTime, Space.Self);
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
