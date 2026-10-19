using System;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;
using static UnityEditorInternal.ReorderableList;

public class ToggleRemote : SingleLinkSwitch
{
    //THIS CLASS IS FOR SINGLE LINK TOGGLING

    public event Action deactivateListener;
    public event Action activateListener;

    public override void Deactivate()
    {
        deactivateListener?.Invoke();
    }

    public override void Activate(GameObject insigator)
    {
        activateListener?.Invoke();
    }
}
