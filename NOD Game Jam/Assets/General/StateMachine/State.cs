using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : ScriptableObject
{
    public StateMachine StateMachine { get; set; }

    public virtual void Initialize(object owner) { }
    public virtual void Enter() { }
    public virtual void StateUpdate() { }
    public virtual void StateFixedUpdate() { }
    public virtual void Exit() { }

    public virtual void TransitionToMe()
    {
        StateMachine.TransitionTo(this);
    }
    public virtual void TransitionTo(State state)
    {
        StateMachine.TransitionTo(state);
    }
    public virtual void TransitionToState<T>()
    {
        StateMachine.TransitionToState<T>();
    }
    public T GetState<T>()
    {
        return StateMachine.GetState<T>();
    }
}
