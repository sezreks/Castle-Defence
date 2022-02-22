using Bases;

namespace Assets.Scripts.Player.PlayerStates
{
    public class PlayerIdleState : BaseState
    {
        protected PlayerSM sm;

        public PlayerIdleState(PlayerSM stateMachine) : base("Idle", stateMachine)
        {
            sm = (PlayerSM)this.stateMachine;
        }

        public override void Enter()
        {
            if (sm._animator != null)
                sm._animator.SetBool("Idle", true);

            base.Enter();
            sm.Name = "Idle";
        }



        public override void Exit()
        {
            if (sm._animator != null)
                sm._animator.SetBool("Idle", false);
            base.Exit();
        }


    }
}