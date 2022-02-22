
using Bases;
using UnityEngine;

namespace Assets.Scripts.Player.PlayerStates
{
    public class PlayerWalkingState : BaseState
    {
        protected PlayerSM sm;

        public PlayerWalkingState(PlayerSM stateMachine) : base("Walking", stateMachine)
        {
           
            sm = (PlayerSM)this.stateMachine;  
        }

        public override void Enter()
        {
            if (sm._animator != null)
                sm._animator.SetBool("Walking", true);

            base.Enter();
            sm.Name = "Walking";
        }

        public override void TriggerEnter(Collider otherObject)
        { 
            base.TriggerEnter(otherObject);
        }


        public override void TriggerExit(Collider otherObject)
        { 
            base.TriggerExit(otherObject);
        }

        public override void Exit()
        {
            if (sm._animator != null)
                sm._animator.SetBool("Walking", false);
             
            base.Exit();
        }
    }
}