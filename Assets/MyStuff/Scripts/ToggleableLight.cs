using DG.Tweening;
using UnityEngine;

public class ToggleableLight : MonoBehaviour
{
    private Light lightSource;
    [SerializeField] private float fadeTime;
    [SerializeField] private Color defaultColour = Color.red;
    [SerializeField] private Color secondaryColor = Color.green;
    private float defaultIntensity;

    private void Awake()
    {
        lightSource = GetComponent<Light>();

        lightSource.color = defaultColour;

        defaultIntensity = lightSource.intensity;
    }

    public void TurnOffLight()
    {
        lightSource.enabled = false;
    }

    public void TurnOnLight()
    {
        lightSource.enabled = true;
    }

    public void FadeOutLight()
    {
        lightSource.DOIntensity(0.0f, fadeTime).SetEase(Ease.Linear);
    }

    public void FadeInLight()
    {
        lightSource.DOIntensity(defaultIntensity, fadeTime).SetEase(Ease.Linear);
    }

    public void ChangeLightColour()
    {
        lightSource.DOColor(secondaryColor, fadeTime).SetEase(Ease.Linear);
    }

    public void ChangeLightColourToDefault()
    {
        lightSource.DOColor(defaultColour, fadeTime).SetEase(Ease.Linear);
    }
}
