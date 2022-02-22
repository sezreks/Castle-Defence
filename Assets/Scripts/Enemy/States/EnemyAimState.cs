using Bases;
using Extentions;
using UnityEngine;

public class EnemyAimState : BaseState
{
    protected EnemySM _enemySM;

    //public List<GameObject> _targets = new List<GameObject>();
    //public List<GameObject> _avalibleTargets = new List<GameObject>();
    // public GameObject _targetBall;
    public Transform target;

    public EnemyAimState(EnemySM stateMachine) : base("Aim", stateMachine)
    {
        _enemySM = (EnemySM)this.stateMachine;
        _enemySM.Name = "Aim";

    }
    // Start is called before the first frame update

    public override void Enter()
    {
        _enemySM.Name = ("Aim");
        if (_enemySM._animator != null)
            _enemySM._animator.SetBool("Aim", true);



        //_avalibleTargets.Clear();
        //_avalibleTargets = _targets;
        //if (_targets != null)
        //{
        //    target = _targets[0].transform;
        //    Debug.Log(target.name);
        //}

        //_avalibleTargets.AddRange(_targets.Where(x => x.GetComponentsInChildren<Block>().Length > 0));



        //_ = TasksExtentions.DoActionAfterSecondsAsync(() =>
        //{
        //    if (_enemySM.GetCurrentState() == _enemySM.aimState) { _enemySM.ChangeState(_enemySM.chooseState); }
        //}, Random.Range(8, 10));

        base.Enter();
    }
    public override void UpdateLogic()
    {


        if (target == null)
        {

            _enemySM.ChangeState(_enemySM.chooseState);

        }
        else
        {

            Debug.Log(target.name);
            var chair = _enemySM.chair.transform.position;
            var weapon = _enemySM.weapon.transform.position;

            //Chair Aim
            var chairTarget = target.GetChild(0).position - chair;
            chairTarget.y = 0;
            _enemySM.chair.transform.rotation = Quaternion.Slerp(_enemySM.chair.transform.rotation, Quaternion.LookRotation(chairTarget), .9f * Time.deltaTime);


            //Weapon Aim
            var weaponTarget = (target.GetChild(0).position - weapon) + Vector3.zero.WithY(-0.85f);

            _enemySM.weapon.transform.rotation = Quaternion.Slerp(_enemySM.weapon.transform.rotation, Quaternion.LookRotation(weaponTarget), .9f * Time.deltaTime);

        }

        base.UpdateLogic();

    }
    public override void Exit()
    {

        if (_enemySM._animator != null)
            _enemySM._animator.SetBool("Aim", false);
        base.Exit();
    }

}
