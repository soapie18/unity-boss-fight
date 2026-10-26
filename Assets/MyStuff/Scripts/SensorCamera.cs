using DG.Tweening;
using UnityEngine;

public class SensorCamera : MonoBehaviour
{
    //Note - rotate180 is currently broken

    [Header("Camera Details")]
    [SerializeField] private EndlessRotatingInteraction remote;
    [SerializeField] private CameraType cameraType;
    [SerializeField] private Transform cameraMesh;
    [SerializeField] private Light cameraLight;
    [SerializeField] private Collider banishingTrigger;

    [Header("Animation Details")]
    [Tooltip("AUTO only! The rate of rotation for auto camera types")][SerializeField] private float cycleLength;
    [SerializeField] private float fadeTime;
    [Tooltip("MANUAL only! Should be equal to Speed Of Rotation on camera this remote controls if you want them to sync up")][SerializeField] private float speedOfRotation;
    private Tween cameraTween;

    //defaults
    private Vector3 defaultPosition;
    private float defaultIntensity;

    private void Awake()
    {
        defaultPosition = cameraMesh.localPosition;
        defaultIntensity = cameraLight.intensity;
    }

    void Start()
    {
        EvaluateBehaviour();
    }

    public void EvaluateBehaviour()
    {
        switch (cameraType)
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
            case CameraType.STATIC:
                break;
            default:
                break;
        }
    }

    public void CameraPowerDown()
    {
        cameraTween.Pause();

        cameraLight.DOIntensity(0.0f, fadeTime).SetEase(Ease.Linear);

        banishingTrigger.enabled = false;

        DisableControl();
    }

    public void CameraBootUp()
    {
        print("Camera booting up");
        cameraTween.Play();

        cameraLight.DOIntensity(defaultIntensity, fadeTime).SetEase(Ease.Linear);

        banishingTrigger.enabled = true;

        AllowControl();
    }

    private void Rotate180()
    {
        //This is broken need to learn how to properly tween rotations
        if (cameraMesh)
        {
            cameraTween = cameraMesh.DORotate(new Vector3(0, defaultPosition.y - 180, 0), cycleLength).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
        }
    }

    private void Rotate360()
    {
        if (cameraMesh)
        {
            cameraTween = cameraMesh.DORotate(new Vector3(0, 360, 0), cycleLength, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1, LoopType.Restart);
        }
    }

    private void AllowControl()
    {
        if (cameraType != CameraType.MANUAL)
        {
            return;
        }

        if (remote)
        {
            remote.Enabled = true;
            remote.OnPercentChanged += RotateCamera;
        }
    }

    private void DisableControl()
    {
        if (cameraType != CameraType.MANUAL)
        {
            return;
        }

        if (remote)
        {
            remote.Enabled = false;
            remote.OnPercentChanged -= RotateCamera;
        }
    }

    private void RotateCamera(float ratio)
    {
        cameraMesh.Rotate(0, ratio * (Time.deltaTime * speedOfRotation), 0, Space.World);
    }
}
