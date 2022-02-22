 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Bases
{
    public abstract class BaseState
    {
        protected string stateName;
        protected StateMachine stateMachine;

        protected BaseState(string name, StateMachine stateMachine)
        {
            this.stateName = name;
            this.stateMachine = stateMachine;
        }

        public virtual void Enter() { }
        public virtual void UpdateLogic() { }
        public virtual void UpdatePhysicsLate() { }
        public virtual void CollisionEnter(Collision collision) { }
        public virtual void ControllerColliderHit(Collision collision) { }
        public virtual void TriggerEnter(Collider otherObject) { }
        public virtual void TriggerExit(Collider otherObject) { }
        public virtual void Exit() { }
    }
}
