using DG.Tweening;
using System;
using UnityEngine;

public class SensingCamera : PoweredObject
{
    [Header("Camera Details")]
    public CameraType CameraType;
    public Transform cameraMesh;
    public Light cameraLight;
    public Interaction remote;
    private Collider collider;
    [SerializeField] private float cycleLength;
    [SerializeField] private float fadeTime;

    private Vector3 defaultPosition;
  
    private float defaultIntensity;
   

    private Tween cameraMove;

    private void Awake()
    {
        defaultPosition = cameraMesh.localPosition;
        defaultIntensity = cameraLight.intensity;
        collider = GetComponent<Collider>();    

     
    }

    void Start()
    {
        EvaluateBehaviour();
        
    }

    //OnEnable connect to sensor's detection event.

    public override void Deactivate()
    {
        CameraPowerDown();

        if (CameraType == CameraType.MANUAL)
        {
            DisableControl();
        }
    }

    public override void Reactivate()
    {
        CameraBootUp();

        if (CameraType == CameraType.MANUAL)
        {
            AllowControl();
        }
        
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
        print("Camera powering down");
        cameraMove.Pause();

        cameraLight.DOIntensity(0.0f, fadeTime).SetEase(Ease.Linear);

        collider.enabled = false;
    }

    private void CameraBootUp()
    {
        print("Camera booting up");
        cameraMove.Play();

        cameraLight.DOIntensity(defaultIntensity, fadeTime).SetEase(Ease.Linear);

        collider.enabled = true;
    }

    private void Rotate180()
    {
        cameraMove = cameraMesh.DORotate(new Vector3(0, defaultPosition.y + 180, 0), cycleLength).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
    }

    private void Rotate360()
    {
        cameraMove = cameraMesh.DORotate(new Vector3(0, 360, 0), cycleLength, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1, LoopType.Restart);
    }

    private void AllowControl()
    {
        if (remote)
        {
            remote.OnPercentChanged += RotateCamera;
        }
    }

    private void DisableControl()
    {
        if (remote)
        {
            remote.OnPercentChanged -= RotateCamera;
        }
    }

    private void RotateCamera(float ratio)
    {
        //The multiplier should be equal to speedOfRotation on the remote if you want them to sync up position wise.
        cameraMesh.Rotate(0, ratio * (Time.deltaTime * 45), 0, Space.World);
    }


}

