using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GGR
{
    public abstract class GGR_State : ScriptableObject
    {
        // Property
        public GGR_StateMachine Owner { get; private set; }

        // Methods
        public virtual void Initialize(GGR_StateMachine owner) { Owner = owner; }
        public virtual void Enter() { }
        public virtual bool Run() { return true; }
        public virtual void Exit() { }
    }
}
