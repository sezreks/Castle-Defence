using Bases;


public class EnemyStartState : BaseState
{
    protected EnemySM esm; 

    public EnemyStartState(EnemySM stateMachine) : base("Start", stateMachine)
    {
        esm = (EnemySM)this.stateMachine;
    }

    public override void Enter()
    { 
        base.Enter();
        esm.Name = "Start";
        esm._animator.SetBool("Start", true);
    }
    public override void UpdateLogic()
    {
        base.UpdateLogic();
    }

    public override void Exit()
    { 
        if (esm._animator != null) 
            esm._animator.SetBool("Start", false); 
        base.Exit();
    }
}
