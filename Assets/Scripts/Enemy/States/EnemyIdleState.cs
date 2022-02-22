using Bases;

namespace Assets.Scripts.Enemy.EnemyStates
{
    public class EnemyIdleState : BaseState
    {
        protected EnemySM esm;

        public EnemyIdleState(EnemySM stateMachine) : base("Idle", stateMachine)
        {
            esm = (EnemySM)this.stateMachine;
        }

        public override void Enter()
        {
            if (esm._animator != null)
                esm._animator.SetBool("Idle", true);

            base.Enter();
            esm.Name = "Idle";
        }
        public override void UpdateLogic()
        {

            base.UpdateLogic();
        }

        public override void Exit()
        {

            if (esm._animator != null)
                esm._animator.SetBool("Idle", false);
            base.Exit();
        }
    }
}