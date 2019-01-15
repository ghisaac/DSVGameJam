using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

[Serializable]
public class StateMachine
{
    [SerializeField] private State[] states;
    [SerializeField] private bool firstStateIsDefault = true;

    public State CurrentState;
    protected Dictionary<Type, State> stateDictionary;
    public object owner { get; set; }
    private List<State> instancedStates;

    public StateMachine(StateMachine stateMachine)
    {
        states = stateMachine.states.ToArray();
        firstStateIsDefault = stateMachine.firstStateIsDefault;
    }

    public virtual void Initialize(object owner)
    {
        this.owner = owner;
        stateDictionary = new Dictionary<Type, State>();
        instancedStates = new List<State>();
        //Create copies of all states
        foreach (State state in states)
            stateDictionary[state.GetType()] = Object.Instantiate(state);
        //Run init and set the StateMachine variable of all created states
        foreach (State state in stateDictionary.Values)
        {
            state.StateMachine = this;
            state.Initialize(owner);
            instancedStates.Add(state);
        }
        //Transition to the first state in the list
        if (firstStateIsDefault)
        {
            TransitionTo(stateDictionary.First().Value);
            return;
        }

        //If the current state variable was set in the inspector, use that state type as the default state
        if (CurrentState != null)
            TransitionTo(GetState(CurrentState.GetType()));
    }
    public virtual void Update()
    {
        if (CurrentState != null)
            CurrentState.StateUpdate();
    }

    public T GetState<T>()
    {
        return (T)Convert.ChangeType(stateDictionary[typeof(T)], typeof(T));
    }
    public State GetState(Type type)
    {
        return stateDictionary[type];
    }
    public virtual void TransitionToState<T>()
    {
        TransitionTo(GetState<T>() as State);
    }
    public virtual void TransitionTo(State state)
    {
        if (state == null) { Debug.LogWarning("Cannot transition to state null"); return; }
        if (CurrentState != null) CurrentState.Exit();
        CurrentState = state;
        CurrentState.Enter();
    }
    //Editor
    public void ReinitializeState(State state)
    {
        if (owner == null) { Debug.LogWarning("Statemachine has not been initialized with valid owner"); return; }

        Type type = state.GetType();
        State instance = Object.Instantiate(state);
        instance.StateMachine = this;
        instance.Initialize(owner);

        stateDictionary[type] = instance;
        if (CurrentState.GetType() == type) TransitionTo(instance);
    }
}

