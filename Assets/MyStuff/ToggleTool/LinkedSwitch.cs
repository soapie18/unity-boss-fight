using System;
using UnityEngine;

public class LinkedSwitch : MonoBehaviour
{
    //This is called externally and is a temporary solution to be improved later in the module.
    public Action Triggered;

    public virtual void OnGameEventRaised()
    {

    }
}
