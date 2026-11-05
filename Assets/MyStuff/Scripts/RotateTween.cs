using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.tvOS;

public class RotateTween : MonoBehaviour
{
    //Creates rotation of object on Y axis
    [Header("Object Details")]

    [SerializeField] private Transform objectToRotate;
    [Tooltip("Required only for MANUAL")][SerializeField] private EndlessRotatingInteraction controller;
    [SerializeField] private ERotationMode rotationMode;

    [Header("Animation Details")]

    [Tooltip("AUTO only! The rate of rotation for auto modes")][SerializeField] private float cycleLength;
    [Tooltip("MANUAL only! Should be equal to Speed Of Rotation on any other manually rotating object this one may be linked with," +
        "if sync is desired.")][SerializeField] private float speedOfRotation;
    private Tween rotationTween;
    private Vector3 defaultLocation;

    private void OnValidate()
    {
        if (objectToRotate != null)
        {
            if (rotationMode == ERotationMode.AUTO_360)
            {
                objectToRotate.rotation = Quaternion.identity;
            }
        }
    }

    private void Awake()
    {
        defaultLocation = objectToRotate.transform.rotation.eulerAngles;
    }

    void Start()
    {
        EvaluateBehaviour();
    }

    public void EvaluateBehaviour()
    {
        switch (rotationMode)
        {
            case ERotationMode.AUTO_180:
                Rotate180();
                break;
            case ERotationMode.AUTO_360:
                Rotate360();
                break;
            case ERotationMode.MANUAL:
                AllowControl();
                break;
            case ERotationMode.NONE:
                break;
            default:
                break;
        }
    }

    public void PauseRotation()
    {
        rotationTween.Pause();
        DisableControl();
    }

    public void ResumeRotation()
    {
        rotationTween.Play();
        AllowControl();
    }

    private void AllowControl()
    {
        if (rotationMode != ERotationMode.MANUAL)
        {
            return;
        }

        if (controller)
        {
            controller.Enabled = true;
            controller.OnPercentChanged += RotateObject;
        }
    }

    private void DisableControl()
    {
        if (rotationMode != ERotationMode.MANUAL)
        {
            return;
        }

        if (controller)
        {
            controller.Enabled = false;
            controller.OnPercentChanged -= RotateObject;
        }
    }

    private void RotateObject(float ratio)
    {
        objectToRotate.Rotate(0, ratio * (Time.deltaTime * speedOfRotation), 0, Space.World);
    }

    private void Rotate180()
    {
        if (objectToRotate)
        {
            rotationTween = objectToRotate.DORotate(new Vector3(0, defaultLocation.y - 180, 0), cycleLength).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
        }
    }

    private void Rotate360()
    {
        if (objectToRotate)
        {
            rotationTween = objectToRotate.DORotate(new Vector3(0, 360, 0), cycleLength, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1, LoopType.Restart);
        }
    }
}
