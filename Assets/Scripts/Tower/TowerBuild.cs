using Assets.Scripts.Canvas;
using Assets.Scripts.WorldEnv.Blocks.Base;
using Assets.Scripts.WorldEnv.Bricks.Base;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Components
{
    public class TowerBuild : MonoBehaviour
    {
        public GameObject smokeCircle;
        [HideInInspector] public bool shootingScene = false;
        public GameObject myCannon;
        public List<GameObject> blocksList;
        public Collider Owner;
        public MainCanvas _mainCanvas;
        [SerializeField] private Transform toptower;


        private void Update()
        {

            if (shootingScene && blocksList.Count == 0)
            {
                if (Owner.name != "Player")
                {
                    myCannon.GetComponent<CannonShoot>().enabled = false;
                    Owner.GetComponent<EnemySM>().ChangeStateLost();

                }
                else
                {
                    GameObject.Find("MainCanvas").GetComponent<MainCanvas>().failCanvas.SetActive(true);
                    Time.timeScale = 1f;
                    myCannon.GetComponent<CannonShoot>().enabled = false;
                }
            }
        }


        public void OnTriggerEnter(Collider collider)
        {
            if (collider == Owner && Owner.name == "Player")
                StartCoroutine(PlayerMoveBricksToTower(0.08f, Owner));

            if (collider == Owner && Owner.name != "Player")
            {
                StartCoroutine(MoveBricksToTower(0.08f, Owner));
                if (collider.TryGetComponent<EnemySM>(out EnemySM _enemySM))
                    _enemySM.ChangeState(_enemySM.idleState);

            }

        }


        IEnumerator MoveBricksToTower(float time, Collider collider)
        {
            var _list = new List<Transform>(Owner.GetComponent<CollectBricks>()._collectedItems);
            foreach (Transform bricks in _list)
            {
                var _brick = bricks.GetComponent<Brick>();
                StartCoroutine(_brick.MoveToTower(toptower));
                GameObject _blockGO = TowerPoolManager.Instance.GetObject(_brick.BlockName);
                _blockGO.GetComponent<Block>().SetActivate(toptower.parent);
                _blockGO.transform.GetChild(0).gameObject.SetActive(true);
                _blockGO.transform.localRotation = new Quaternion(0, 0, 0, 0);
                blocksList.Add(_blockGO);
                Owner.GetComponent<CollectBricks>()._collectedItems.Remove(bricks);
                if (_mainCanvas.remainingTime > .5f)
                    yield return new WaitForSeconds(time);
            }

            if (collider.TryGetComponent<EnemySM>(out EnemySM _enemySM))
                _enemySM.ChangeState(_enemySM.walkingState);
        }


        IEnumerator PlayerMoveBricksToTower(float time, Collider collider)
        {
            var _list = new List<Transform>(Owner.GetComponent<CollectBricks>()._collectedItems);
            foreach (Transform bricks in _list)
            {
                var _brick = bricks.GetComponent<Brick>();
                StartCoroutine(_brick.MoveToTower(toptower));
                GameObject _blockGO = TowerPoolManager.Instance.GetObject(_brick.BlockName);
                _blockGO.GetComponent<Block>().SetActivate(toptower.parent);
                _blockGO.transform.GetChild(PlayerData.Instance._castleLevel).gameObject.SetActive(true);
                _blockGO.transform.localRotation = new Quaternion(0, 0, 0, 0);
                blocksList.Add(_blockGO);
                Owner.GetComponent<CollectBricks>()._collectedItems.Remove(bricks);
                if (_mainCanvas.remainingTime > .5f)
                    yield return new WaitForSeconds(time);
            }

            if (collider.TryGetComponent<EnemySM>(out EnemySM _enemySM))
                _enemySM.ChangeState(_enemySM.walkingState);
        }

    }
}
