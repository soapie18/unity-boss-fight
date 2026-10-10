using DG.Tweening;
using System;
using UnityEngine;

public class SensingCamera : PoweredObject
{
    [Header("Camera Details")]
    public CameraType CameraType;
    public Transform cameraMesh;
    private Vector3 defaultPosition;
    public Light cameraLight;
    public Interaction remote;
    [SerializeField] private float cycleLength;

    private void Awake()
    {
        defaultPosition = cameraMesh.localPosition;
    }

    void Start()
    {
        EvaluateBehaviour();
        
    }

    //OnEnable connect to sensor's detection event.

    public override void Deactivate()
    {
        CameraPowerDown();
    }

    public override void Reactivate()
    {
        CameraBootUp();
    }

    public void EvaluateBehaviour()
    {
        switch (CameraType)
        {
            case CameraType.AUTO_180:
                Rotate180();
                break;
            case CameraType.AUTO_360:
                Rotate360();
                break;
            case CameraType.MANUAL:
                AllowControl();
                break;
            default:
                break;
        }
    }

    private void CameraPowerDown()
    {
        throw new NotImplementedException();
    }

    private void CameraBootUp()
    {
        throw new NotImplementedException();
    }

    private void Rotate180()
    {
        cameraMesh.DORotate(new Vector3(0, defaultPosition.y + 180, 0), cycleLength).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
    }

    private void Rotate360()
    {
        cameraMesh.DORotate(new Vector3(0, 360, 0), cycleLength, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1, LoopType.Restart);
    }

    private void AllowControl()
    {
        if (remote)
        {
            remote.OnPercentChanged += RotateCamera;
        }
    }

    private void RotateCamera(float ratio)
    {
        //The multiplier should be equal to speedOfRotation on the remote if you want them to sync up position wise.
        cameraMesh.Rotate(0, ratio * (Time.deltaTime * 45), 0, Space.World);
    }


}

public enum CameraType
{
    AUTO_180,
    AUTO_360,
    MANUAL
}