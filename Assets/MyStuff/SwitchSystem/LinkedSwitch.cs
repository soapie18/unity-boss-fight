using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class LinkedSwitch : MonoBehaviour
{
    //This is invoked externally.
    public Action Triggered;

    public event Action ToggleOpposers;

    private bool inStateA = false;

    [Header("Switch Properties")]
    [SerializeField] protected bool interactable = false;
    [SerializeField] protected bool startInA = false;
    [SerializeField] protected bool onTimer = false;
    [SerializeField] protected float timeActiveFor = 0;

    [Header("Toggle Events")]
    public UnityEvent OnStateA;
    public UnityEvent OnStateB;

    protected float timeLeft;

    //Switches always start in state A.
    void Start()
    {
        if(startInA)
        {
            ActivateStateA();
            inStateA = true;
        }

        else
        {
            ActivateStateB();
            inStateA = false;
        }
    }

    //In the event of interaction with this switch, this function is called. Toggle any opposers.
    protected void TriggerToggleOpposer()
    {
        ToggleOpposers?.Invoke();
    }

    // Tied to same delegate that triggers Activate() and ToggleOpposers()
    protected void ResetTimer()
    {
        timeLeft = timeActiveFor;
    }

    //This function is for when another object toggles, and how this object responds to that.
    protected void ToggleResponse()
    {
        DetermineState();
    }

    protected void DetermineState()
    {
        if (!inStateA)
        {
            ActivateStateA();
            inStateA = true;
            return;
        }

        if (inStateA)
        {
            ActivateStateB();
            inStateA = false;
            return;
        }
        
    }

    // Designer can call response behaviour in inspector using UnityEvents.
    protected void ActivateStateA()
    {
        OnStateA?.Invoke();

    }

    protected void ActivateStateB()
    {

        OnStateB?.Invoke();

    }

    // Cooldown for timed switches to be able to turn themselves back off.
    private void Update()
    {
        if (inStateA && onTimer)
        {
            timeLeft -= Time.deltaTime;

            if (timeLeft <= 0)
            {
                inStateA = false;
                ActivateStateB();
            }
        }

    }
}
