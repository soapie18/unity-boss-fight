using UnityEngine;

[CreateAssetMenu(fileName = "Responder", menuName = "Level Stuff/Responder")]

public class Responder : ScriptableObject
{
    public Stunner stunner;

    void Start()
    {
       //Subscribe to stunner's events here 'DeactivatreResponders' and 'ReactivateRespoinders'
    }

    void OnDeactivate()
    {
        Deactivate();
    }

    void OnReacivate()
    {
        Reactivate();
    }

    void Deactivate() { }
    void Reactivate() { }
    
}
