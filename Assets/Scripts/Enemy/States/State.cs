using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State 
{
    public StateMachine machine {  get; protected set; }

    public State(StateMachine stateMachine)
    {
        machine = stateMachine;
    }

    public abstract void OnUpdate();
    public abstract void OnStart();
    public abstract void OnFinish();
}
