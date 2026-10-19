using System.Collections;
using UnityEngine;

public interface IToggleable
{
    public void ToggleResponse();

    public IEnumerator ToggleCooldown();


}
