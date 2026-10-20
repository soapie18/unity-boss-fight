using DG.Tweening;
using UnityEngine;

public class PoweredDoor : PoweredObject
{
    [Header("Door Details")]
    public Transform doorMesh;
    public Light doorLight;
    private float defaultIntensity;
    private Vector3 defaultPos;
    [SerializeField] private float fallSpeed;

    void Awake()
    {
        defaultIntensity = doorLight.intensity;
        defaultPos = doorMesh.localPosition;
    }

    public void DoorPowerDown()
    {
        //Tween the light intensity.
        doorLight.DOIntensity(0, 1.5f);

        //Tween the location of the door mesh.
        doorMesh.DOLocalMoveY(-3.0f, 2.0f);

        //Play a door move sound effect.
    }

    public void DoorBootUp() 
    {
        //Tween the light intensity.
        doorLight.DOIntensity(defaultIntensity, 1.5f);

        //Tween the location of the door mesh.
        doorMesh.DOLocalMoveY(defaultPos.y, fallSpeed);

        //Play a door move sound effect.
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
