using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameEvent", menuName = "GameEvent")]
public class GameEvent : ScriptableObject
{
    private List<LinkedSwitch> ListeningSwitches = new();

    private void OnEnable()
    {
        ListeningSwitches.Clear();
    }

    public virtual void RaiseEvent()
    {
        for (int i =ListeningSwitches.Count - 1; i >= 0; --i)
        {
            ListeningSwitches[i].OnGameEventRaised();

            Debug.Log(ListeningSwitches[i]);
            Debug.Log(ListeningSwitches.Count);
        }
    }

    public void AddListener(LinkedSwitch linkedSwitch)
    {
        if (!ListeningSwitches.Contains(linkedSwitch))
        {
            ListeningSwitches.Add(linkedSwitch);
        }
    }

    public void RemoveListener(LinkedSwitch linkedSwitch)
    {
        if (ListeningSwitches.Contains(linkedSwitch))
        {
            ListeningSwitches.Remove(linkedSwitch);
        }
    }  
}

