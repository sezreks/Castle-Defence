using Assets.Scripts.WorldEnv.Bricks.Base;
using Bases;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Enemy.EnemyStates
{
    public class EnemyWalkingState : BaseState
    {
        protected EnemySM _enemySM;
        private int selectedBrickIndex;
        private int collectedBrickCount;
        private Brick[] _worldBricks;
        public EnemyWalkingState(EnemySM stateMachine) : base("Walking", stateMachine)
        {
            _enemySM = (EnemySM)this.stateMachine;
        }

        public override void Enter()
        {
            base.Enter();
            _enemySM.Name = ("Walking");
            if (_enemySM._animator != null)
                _enemySM._animator.SetBool("Walking", true);

            _worldBricks = _enemySM.spawner.GetComponentsInChildren<Brick>();
            selectedBrickIndex = Random.Range(0, _worldBricks.Length);
        }

        public override void UpdateLogic()
        {

            var colbrick = _enemySM.GetComponent<CollectBricks>();
            if (colbrick._collectedItems.Count != collectedBrickCount)
            {
                _worldBricks = _enemySM.spawner.GetComponentsInChildren<Brick>().ToList().Where(x => x.IsGrounded).ToArray();
                selectedBrickIndex = Random.Range(0, _worldBricks.Length);
                collectedBrickCount = colbrick._collectedItems.Count;
            }



            if (collectedBrickCount >= _enemySM._collectLimit)
            {
                var targetPos = _enemySM.myTower.transform.position;
                var direction = targetPos - _enemySM.transform.position;
                _enemySM.GetComponent<Rigidbody>().MovePosition(_enemySM.Transform.position + direction.normalized * Time.deltaTime * _enemySM.speed);
                _enemySM.transform.rotation = Quaternion.Slerp(_enemySM.transform.rotation, Quaternion.LookRotation(direction), 5f * Time.fixedDeltaTime);
            }
            else
            {



                try
                {
                    var targetPos = _worldBricks[selectedBrickIndex].transform.position;
                    var direction = targetPos - _enemySM.gameObject.transform.position;
                    _enemySM.GetComponent<Rigidbody>().MovePosition(_enemySM.Transform.position + direction.normalized * Time.deltaTime * _enemySM.speed);
                    _enemySM.transform.rotation = Quaternion.Slerp(_enemySM.gameObject.transform.rotation, Quaternion.LookRotation(direction), 5f * Time.fixedDeltaTime);
                    if (!_worldBricks[selectedBrickIndex].IsGrounded) collectedBrickCount = -1;

                    if (_enemySM.transform.position + targetPos.normalized == targetPos)
                    {
                        selectedBrickIndex = Random.Range(0, _worldBricks.Length);
                    }
                }
                catch (System.Exception)
                {
                    _worldBricks = _enemySM.spawner.GetComponentsInChildren<Brick>().ToList().Where(x => x.IsGrounded).ToArray();
                    selectedBrickIndex = Random.Range(0, _worldBricks.Length);

                }




            }


            base.UpdateLogic();

        }



        public override void Exit()
        {
            if (_enemySM._animator != null)
                _enemySM._animator.SetBool("Walking", false);
            base.Exit();
        }
    }
}
