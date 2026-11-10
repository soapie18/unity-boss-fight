using DG.Tweening;
using UnityEngine;

public class ToggleableLight : MonoBehaviour
{
    public Light lightSource;
    [SerializeField] private float fadeTime;
    [SerializeField] private Color defaultColour = Color.red;
    [SerializeField] private Color secondaryColor = Color.green;
    [SerializeField]private float defaultIntensity = 0;

    private void OnValidate()
    {
        if(lightSource)
        {
            lightSource.intensity = defaultIntensity;

            lightSource.color = defaultColour;
        }
    }

    public void TurnOffLight()
    {
        lightSource.intensity = 0;
    }

    public void TurnOnLight()
    {
        lightSource.intensity = defaultIntensity;
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
