using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.tvOS;

public class RotateTween : MonoBehaviour
{
    //Creates rotation of object on Y axis
    [Header("Object Details")]

    [SerializeField] private Transform objectToRotate;
    [SerializeField] peivate EAxisOfMovement axisOfRotation;
    

    [Header("Animation Details")]

    [Tooltip("Speed of the rotation animation")][SerializeField] private float rotationTime;
    [SerializeField] private float rotationDistance;
    private Tween rotationTween;
    private Vector3 defaultPos;
    private Vector3 newPos;

    private void OnValidate()
    {
        if (objectToRotate != null)
        {
            if (rotationMode == ERotationMode.LOOP_360)
            {
                objectToRotate.rotation = Quaternion.identity;
            }
        }

        if (customTriggered)
        {
            rotationMode = ERotationMode.NONE;
        }

        defaultPos = objectToRotate.transform.rotation.eulerAngles;
    }

    private void DetermineAxis()
    {
        switch (axisOfMovement)
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
        //Improve this by getting the midpoint and calculating whether we are above or below it.
        
        if (objectToMove.position == newPos)
        {
            ReturnObject();
            return;
        }

        if (objectToMove.position == defaultPos)
        {
            MoveObject();
            return;
        }
        
        Debug.Log("object was still moving");
        return;
    }

    public void Rotate()
    {
        if (objectToRotate)
        {
            rotationTween = objectToRotate.DORotate(new Vector3(0, defaultLocation.y - 90, 0), cycleLength).SetEase(Ease.InOutSine);
        }
    }

    public void ReturnFromRotation()
    {
        if (objectToRotate)
        {
            rotationTween = objectToRotate.DORotate(new Vector3(0, defaultLocation.y, 0), cycleLength).SetEase(Ease.InOutSine);
        }
    }

    
}
