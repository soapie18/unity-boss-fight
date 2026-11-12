using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.tvOS;

public class RotateTween : MonoBehaviour
{
    [Header("Object Details")]

    [SerializeField] private Transform objectToRotate;
    [SerializeField] private EAxisOfMovement axisOfRotation;

    private bool returning;
    

    [Header("Animation Details")]

    [Tooltip("Speed of the rotation animation")][SerializeField] private float rotationTime;
    [SerializeField] private float rotationDistance;
    private Tween rotationTween;
    private Vector3 defaultPos;
    private Vector3 newPos;

    private void Awake()
    {
        if(objectToRotate)
        {
            defaultPos = objectToRotate.transform.rotation.eulerAngles;
        }
    }

    private void DetermineAxis()
    {
        switch (axisOfRotation)
        {
            case EAxisOfMovement.Y:
                newPos.y = defaultPos.y + rotationDistance;
                break;
            case EAxisOfMovement.X:
                newPos.x = defaultPos.x + rotationDistance;
                break;
            case EAxisOfMovement.Z:
                newPos.z = defaultPos.z + rotationDistance;
                break;
        }
    }

    private void Start()
    {
        DetermineAxis();
    }

    public void DecideRotationNeeded()
    {
        returning = !returning;
        
        if (returning)
        {
            Rotate();
            return;
        }

        else
        {
            ReturnFromRotation();
        }

            Debug.Log("failed?");
        return;
    }

    public void Rotate()
    {
        if (objectToRotate)
        {
            rotationTween = objectToRotate.DORotate(newPos, rotationTime).SetEase(Ease.InOutSine);
        }
    }

    public void ReturnFromRotation()
    {
        if (objectToRotate)
        {
            rotationTween = objectToRotate.DORotate(newPos, rotationTime).SetEase(Ease.InOutSine);
        }
    }

    
}
