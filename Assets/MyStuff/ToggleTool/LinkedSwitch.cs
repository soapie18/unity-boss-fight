using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class LinkedSwitch : MonoBehaviour
{
    //This is called externally and is a temporary solution to be improved later in the module.
    public Action Triggered;

    public event Action ToggleOpposers;

    private bool activeSwitch = false;

    [Header("Switch Properties")]
    [SerializeField] protected bool interactable = false;
    [SerializeField] protected bool activeByDefault = false;
    [SerializeField] protected bool onTimer = false;
    [SerializeField] protected float timeActiveFor = 0;
    //[SerializeField] protected bool activeOnStart = false;

    [Header("Toggle Events")]
    public UnityEvent OnSwitchActivate;
    public UnityEvent OnSwitchDeactivate;

    protected float timeLeft;

    void Start()
    {
        if (activeByDefault)
        {
            Activate();
            activeSwitch = true;
        }
    }

    //In the event of interaction with this switch, this function is called. Toggle any opposers off and make this switch active.
    protected void TriggerToggleOpposer()
    {
        if (!activeSwitch)
        {
            ToggleOpposers?.Invoke();

            activeSwitch = true;
        }

    }

    // Tied to same delegate that triggers Activate() and ToggleOpposers()
    protected void ResetTimer()
    {
        timeLeft = timeActiveFor;
    }

    //This function is for when another object toggles, and how this object responds to that (turning itself off if active).
    protected void ToggleResponse()
    {

        if (this.activeSwitch)
        {
            Deactivate();

            activeSwitch = false;
        }

    }


    // Designer can call response behaviour in inspector using UnityEvents.
    protected void Activate()
    {
        OnSwitchActivate?.Invoke();

    }

    protected void Deactivate()
    {

        OnSwitchDeactivate?.Invoke();

    }

    // Cooldown for timed switches to be able to turn themselves back off.
    private void Update()
    {
        if (activeSwitch && onTimer)
        {
            timeLeft -= Time.deltaTime;

            if (timeLeft <= 0)
            {
                activeSwitch = false;
                Deactivate();
            }
        }
        
    }
}
