using Assets.Scripts.Canvas;
using Assets.Scripts.Player.PlayerStates;
using Bases;
using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerSM : StateMachine
{
    public string Name;
    public float speed = 15f;



    [HideInInspector] public Animator _animator;
    [HideInInspector] public Rigidbody _rigidbody;





    [HideInInspector] public PlayerIdleState idleState;
    [HideInInspector] public PlayerWalkingState walkingState;
    [HideInInspector] public PlayerAimState aimState;
    [HideInInspector] public PlayerShootState shootState;


    public Joystick joystick;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();



        idleState = new PlayerIdleState(this);
        walkingState = new PlayerWalkingState(this);
        aimState = new PlayerAimState(this);
        shootState = new PlayerShootState(this);


        if (joystick != null)
        {
            joystick.OnMove += Joystick_OnMove;
            joystick.OnMoveFinished += Joystick_OnMoveFinished;
        }

    }

    private void Joystick_OnMoveFinished(object sender, EventArgs e)
    {
        ChangeState(idleState);
    }

    private void Joystick_OnMove(object sender, Vector2 e)
    {
        if (e.magnitude > 0)
        {
            if (!MainCanvas.Instance._changeScene)
            {
                float walkStartAngle = Mathf.Atan2(e.x, e.y) * Mathf.Rad2Deg;
                transform.eulerAngles = new Vector3(0, walkStartAngle, 0);
                _rigidbody.MovePosition(transform.position + new Vector3(e.x, 0, e.y) * speed * Time.fixedDeltaTime);
                if (GetCurrentState() != walkingState)
                    ChangeState(walkingState);
            }
            else
            {
                float walkStartAngle = Mathf.Atan2(-e.x, -e.y) * Mathf.Rad2Deg;
                transform.eulerAngles = new Vector3(0, walkStartAngle, 0);
                _rigidbody.MovePosition(transform.position + new Vector3(-e.x, 0, -e.y) * speed * Time.fixedDeltaTime);
                if (GetCurrentState() != walkingState)
                    ChangeState(walkingState);
            }

        }
    }

    protected override BaseState GetInitialState()
    {
        return idleState;
    }

    protected override void Update()
    {
        base.Update();
    }
}