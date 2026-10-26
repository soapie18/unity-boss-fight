using DG.Tweening;
using UnityEditor.Experimental.GraphView;
using UnityEngine;


public class EndlessMovement : MonoBehaviour
{
    [SerializeField] private Transform objectToMove;
    [SerializeField] private float movementSpeed;
    [SerializeField] private EAxisOfMovement axisOfMovement;
    private Vector3 direction;

    public float objectLifetime;

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

    void Start()
    {
        ChooseAxis();
    }

    public void FlipDirection()
    {
        movementSpeed *= -1;
    }
   
    void Update()
    {
        objectToMove.Translate(direction * Time.deltaTime, Space.Self);
    }
}
