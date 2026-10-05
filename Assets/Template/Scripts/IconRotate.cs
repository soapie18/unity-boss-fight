using UnityEngine;

public class IconRotate : MonoBehaviour
{
    public Vector3 rotation;
    public bool worldSpace = true;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotation * Time.deltaTime, worldSpace ? Space.World : Space.Self);
    }
}
