using Bases;
using Extentions;
using UnityEngine;
public class EnemyChooseState : BaseState
{
    protected EnemySM esm;

    public EnemyChooseState(EnemySM stateMachine) : base("Choose", stateMachine)
    {
        esm = (EnemySM)this.stateMachine;
    }

    public override void Enter()
    {
        if (esm._animator != null)
            esm._animator.SetBool("Choose", true);

        base.Enter();
        esm.Name = "Choose";
        _ = TasksExtentions.DoActionAfterSecondsAsync(() => { esm.ChangeStateAim(); }, 1f);
    }
    public override void UpdateLogic()
    {
        esm.chair.transform.rotation = Quaternion.Slerp(esm.chair.transform.rotation, Quaternion.LookRotation(Vector3.forward), .4f * Time.deltaTime);
        esm.weapon.transform.rotation = Quaternion.Slerp(esm.weapon.transform.rotation, Quaternion.LookRotation(Vector3.forward), .4f * Time.deltaTime);

        base.UpdateLogic();
    }


    public override void Exit()
    {
        if (esm._animator != null)
            esm._animator.SetBool("Choose", false);
        base.Exit();
    }
}
