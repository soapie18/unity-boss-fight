using UnityEngine;

[CreateAssetMenu(fileName = "StatusEffectData", menuName = "Scriptable Objects/StatusEffectData")]
public class StatusEffectData : ScriptableObject
{
    public string effectName;
    public float movementPenalty;
    public float duration;
    public GameObject sfx;
}
