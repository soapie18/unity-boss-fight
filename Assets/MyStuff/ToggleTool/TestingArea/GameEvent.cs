using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameEvent", menuName = "GameEvent")]
public class GameEvent : ScriptableObject
{
    private List<LinkedSwitch> ListeningSwitches = new();

    public virtual void RaiseEvent()
    {
        for (int i =ListeningSwitches.Count - 1; i >= 0; --i)
        {
            ListeningSwitches[i].OnGameEventRaised();
        }
    }

    public void AddListener(LinkedSwitch linkedSwitch)
    {
        ListeningSwitches.Add(linkedSwitch);
    }

    public void RemoveListener(LinkedSwitch linkedSwitch)
    {
        if (ListeningSwitches.Contains(linkedSwitch))
        {
            ListeningSwitches.Remove(linkedSwitch);
        }
    }  
}

