using Bases;

namespace Assets.Scripts.Player.PlayerStates
{
    public class PlayerShootState : BaseState
    {
        protected PlayerSM sm;

        public PlayerShootState(PlayerSM stateMachine) : base("Shoot", stateMachine)
        {
            sm = (PlayerSM)this.stateMachine;
        }

        public override void Enter()
        {
            if (sm._animator != null)
                sm._animator.SetBool("Shoot", true);

            base.Enter();
            sm.Name = "Shoot";
        }

        public override void UpdateLogic()
        {
        }

        public override void Exit()
        {
            if (sm._animator != null)
                sm._animator.SetBool("Shoot", false);
            base.Exit();
        }


    }
}