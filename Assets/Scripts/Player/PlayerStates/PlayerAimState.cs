using Bases; 

namespace Assets.Scripts.Player.PlayerStates
{
    public class PlayerAimState : BaseState
    {
        protected PlayerSM sm;

        public PlayerAimState(PlayerSM stateMachine) : base("Aim", stateMachine)
        {
            sm = (PlayerSM)this.stateMachine;
        }

        public override void Enter()
        {
            if (sm._animator != null)
                sm._animator.SetBool("Aim", true);

            base.Enter();
            sm.Name = "Aim";
        }

        public override void UpdateLogic()
        { 
        }

        public override void Exit()
        {
            if (sm._animator != null)
                sm._animator.SetBool("Aim", false);
            base.Exit();
        }


    }
}