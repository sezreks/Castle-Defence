using Assets.Scripts.Enemy.EnemyStates;
using Bases;
using UnityEngine;

public class EnemySM : StateMachine
{
    public string Name;
    public float speed = 15f;
    public int _collectLimit = 7;

    [HideInInspector] public bool goBuild;
    [HideInInspector] public bool goCollect;
    [HideInInspector] public GameObject spawner;
    [HideInInspector] public GameObject myTower;
    public GameObject flag;


    [HideInInspector] public Animator _animator;
    [HideInInspector] public Rigidbody _rigidbody;

    [HideInInspector] public EnemyIdleState idleState;
    [HideInInspector] public EnemyChooseState chooseState;
    [HideInInspector] public EnemyLostState lostState;
    [HideInInspector] public EnemyAimState aimState;
    [HideInInspector] public EnemyWalkingState walkingState;
    [HideInInspector] public EnemyStartState startState;

    public GameObject enemy, player;

    public GameObject chair, weapon;


    private Transform _transform;


    private void Awake()
    {
        spawner = GameObject.Find("Spawner");


        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
        _transform = GetComponent<Transform>();


        idleState = new EnemyIdleState(this);
        walkingState = new EnemyWalkingState(this);
        aimState = new EnemyAimState(this);
        startState = new EnemyStartState(this);
        chooseState = new EnemyChooseState(this);
        lostState = new EnemyLostState(this);


    }


    protected override BaseState GetInitialState()
    {
        return startState;
    }

    public void ChangeStateIdle()
    {
        ChangeState(idleState);
    }
    public void ChangeStateChoose()
    {
        ChangeState(chooseState);
    }
    public void ChangeStateLost()
    {
        ChangeState(lostState);
    }
    public void ChangeStateAim()
    {
        ChangeState(aimState);
    }
    public void ChangeStateWalking()
    {
        ChangeState(walkingState);
    }
    public void ChangeStateStart()
    {
        ChangeState(startState);
    }


    protected override void Update()
    {

        base.Update();
    }

    public Transform Transform => _transform;

}


