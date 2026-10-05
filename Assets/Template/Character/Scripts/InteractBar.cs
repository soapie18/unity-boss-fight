using UnityEngine;
using UnityEngine.UI;

public class InteractBar : MonoBehaviour
{
    [SerializeField] Image progress;
    float _time;
    Transform _cam;
    const float MaxTime = 0.3f;

    private void Start()
    {
        _cam = Camera.main.transform;
    }

    public Interaction CurrentInteraction
    {
        get
        {
            return _currentInteraction;
        }
        set
        {
            if (value && value.SupportsInteractionType(Type.LONG) && !value.IsEndless())
            {
                _currentInteraction = value;
                gameObject.SetActive(true);
                _time = MaxTime;
            }
        }
    }
    Interaction _currentInteraction;

    void Update()
    {
        if (_time <= 0)
        {
            gameObject.SetActive(false);
            return;
        }
        if (_currentInteraction != null)
        {
            progress.transform.localScale = new(_currentInteraction.GetPercent(), 1, 1);
        }
        _time -= Time.deltaTime;
        transform.forward = _cam.forward;
    }
}
