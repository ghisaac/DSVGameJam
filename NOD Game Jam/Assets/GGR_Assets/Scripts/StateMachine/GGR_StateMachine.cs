using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace GGR
{
    [CreateAssetMenu(menuName = "GGR/StateMachine")]
    public class GGR_StateMachine : ScriptableObject
    {
        // Attributes
        public List<GGR_State> States;

        private Dictionary<Type, GGR_State> stateMap = new Dictionary<Type, GGR_State>();
        public GGR_State queuedState;
        public GGR_State currentState;

        // Methods
        public void Awake()
        {
            foreach (GGR_State state in States)
            {
                GGR_State newState = Instantiate(state);
                newState.Initialize(this);
                stateMap.Add(state.GetType(), newState);

                if (queuedState == null)
                    queuedState = newState;
            }
        }

        public bool Run()
        {
            if (queuedState != null)
            {
                currentState?.Exit();
                currentState = queuedState;
                queuedState.Enter();
                queuedState = null;
            }

            return currentState.Run();
        }

        public void TransitionTo<GGR_State>()
        {
            queuedState = stateMap[typeof(GGR_State)];
        }
    }
}
