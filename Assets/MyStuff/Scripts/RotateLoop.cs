using UnityEngine;

public class RotateLoop : MonoBehaviour
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
            if (rotationMode == ERotationMode.LOOP_360)
            {
                objectToRotate.rotation = Quaternion.identity;
            }
        }

        defaultLocation = objectToRotate.transform.rotation.eulerAngles;
    }

    private void Awake()
    {
        EvaluateBehaviour();
    }


    public void EvaluateBehaviour()
    {
        switch (rotationMode)
        {
            case ERotationMode.LOOP_90:
                Loop90();
                break;
            case ERotationMode.LOOP_180:
                Loop180();
                break;
            case ERotationMode.LOOP_360:
                Loop360();
                break;
            case ERotationMode.MANUAL:
                AllowControl();
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
            controller.OnPercentChanged += RotateObjectManually;
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
            controller.OnPercentChanged -= RotateObjectManually;
        }
    }

    private void Loop180()
    {
        if (objectToRotate)
        {
            rotationTween = objectToRotate.DORotate(new Vector3(0, defaultLocation.y - 180, 0), cycleLength).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
        }
    }

    private void Loop90()
    {
        if (objectToRotate)
        {
            rotationTween = objectToRotate.DORotate(new Vector3(0, defaultLocation.y - 90, 0), cycleLength).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
        }
    }

    private void Loop360()
    {
        if (objectToRotate)
        {
            rotationTween = objectToRotate.DORotate(new Vector3(0, 360, 0), cycleLength, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1, LoopType.Restart);
        }
    }
}
