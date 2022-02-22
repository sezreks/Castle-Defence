using UnityEngine;

namespace Bases
{
    public abstract class StateMachine : MonoBehaviour
    {
        private BaseState currentState;

        protected virtual void Start()
        {
            currentState = GetInitialState();
            if (currentState != null)
                currentState.Enter();
        }
        protected virtual void Update()
        {
            if (currentState != null)
                currentState.UpdateLogic();
        }
        protected virtual void LateUpdate()
        {
            if (currentState != null)
                currentState.UpdatePhysicsLate();
        }
        protected virtual void OnCollisionEnter(Collision collision)
        {
            if (currentState != null)
                currentState.CollisionEnter(collision);
        }
        protected virtual void OnTriggerEnter(Collider otherObject)
        {
            if (currentState != null)
                currentState.TriggerEnter(otherObject);
        }
        protected virtual void OnTriggerExit(Collider otherObject)
        {
            if (currentState != null)
                currentState.TriggerExit(otherObject);
        }
        public virtual void ChangeState(BaseState newState)
        {
            if (newState != null)
            {
                if (currentState != null)
                    currentState.Exit();

                currentState = newState;
                newState.Enter();
            }
        }
        protected abstract BaseState GetInitialState();
        public virtual BaseState GetCurrentState()
        {
            return currentState;
        }

    }
}
