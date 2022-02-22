using Bases;
using UnityEngine;
public class EnemyLostState : BaseState
{
    protected EnemySM esm;

    public EnemyLostState(EnemySM stateMachine) : base("Lost", stateMachine)
    {
        esm = (EnemySM)this.stateMachine;
    }

    public override void Enter()
    {
        if (esm._animator != null)
            esm._animator.SetBool("Lost", true);
        esm.flag.SetActive(true);
        base.Enter();
        esm.Name = "Lost";
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
            esm._animator.SetBool("Lost", false);
        base.Exit();
    }
}
