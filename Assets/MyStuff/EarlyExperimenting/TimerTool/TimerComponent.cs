using System.Collections;
using UnityEngine;

public class TimerComponent : MonoBehaviour
{
    

    public IEnumerator LerpFloatValue(float valueToLerp, float startValue, float endValue, float duration, float ratio)
    {

        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            print("Lerping a value: " + valueToLerp);

            valueToLerp = Mathf.Lerp(startValue, endValue, ratio);

            yield return null;
        }

        

        yield return new WaitForSeconds(0.1f);

    }


    public IEnumerator LerpLightIntensity(Light light, float startVal, float endVal, float duration)
    {
        //When t, time elapsed, surpasses the desired duration of the lerp, the loop ends.
        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            light.intensity = Mathf.Lerp(startVal, endVal, t / duration);

            yield return null;
        }

        //Ensuring its set to the intended end value.
        light.intensity = endVal;

        yield return new WaitForSeconds(0.1f);
    }
    
}
