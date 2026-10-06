using System;
using UnityEngine;

public class SensingCamera : PoweredObject
{
    [Header("Camera Details")]
    public CameraType CameraType;
    public Transform cameraMesh;
    public Light cameraLight;
    public Interaction remote;

    [Header("Other")]
    public TimerComponent timer;

    void Start()
    {
        EvaluateBehaviour();
        if (remote)
        {
            remote.OnPercentChanged += RotateCamera;
        }
        
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
                break;
            case CameraType.AUTO_360:
                break;
            case CameraType.MANUAL:
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

    public void RotateCamera(float ratio)
    {
        //The multiplier should be equal to speedOfRotation on the remote if you want them to sync up.
        cameraMesh.Rotate(0, ratio * (Time.deltaTime * 45), 0, Space.World);
    }


}

public enum CameraType
{
    AUTO_180,
    AUTO_360,
    MANUAL
}