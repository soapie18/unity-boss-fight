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

        //Lerp the location of the door mesh.

        //Play a door move sound effect
    }

    public void DoorPowerOn() 
    { 
    }

    public override void Deactivate()
    {
        DoorPowerDown();
    }

    public override void Reactivate()
    {
        DoorPowerOn();
    }
}
