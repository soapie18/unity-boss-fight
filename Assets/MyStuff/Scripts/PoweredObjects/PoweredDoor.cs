using UnityEngine;

public class PoweredDoor : PoweredObject
{
    [Header("Door Details")]
    public Mesh doorMesh;
    public Light doorLight;
    private float defaultIntensity;
    public float fallSpeed;

    [Header("Other")]
    public TimerComponent timer;


    void Start()
    {
         defaultIntensity = doorLight.intensity;
    }

    public void DoorPowerDown()
    {
        print("Door Powering Down");

        //Lerp the light intensity.
        StartCoroutine(timer.LerpLightIntensity(doorLight, defaultIntensity, 0, 1));

        //Tween the location of the door mesh.

        //Play a door move sound effect
    }

    public void DoorBootUp() 
    {
        StartCoroutine(timer.LerpLightIntensity(doorLight, 0, defaultIntensity, 1));
    }

    public override void Deactivate()
    {
        DoorPowerDown();
    }

    public override void Reactivate()
    {
        DoorBootUp();
    }
}
