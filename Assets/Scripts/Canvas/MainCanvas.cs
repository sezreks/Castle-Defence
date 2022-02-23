using Assets.Scripts.Components;
using Assets.Scripts.Tower;
using Assets.Scripts.WorldEnv;
using Assets.Scripts.WorldEnv.Blocks.Base;
using System;
using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Canvas
{
    public class MainCanvas : MonoBehaviour
    {
        public static MainCanvas Instance;
        [HideInInspector] public Animator Transition;
        public float remainingTime;
        [HideInInspector] public bool started = false;

        [HideInInspector] public GameObject enemy1, enemy2;
        [HideInInspector] public GameObject tower1, tower2;

        [HideInInspector] public GameObject arrow, spawner;
        [HideInInspector] public GameObject failCanvas;
        [HideInInspector] public GameObject remainingTimeText;
        [HideInInspector] public GameObject openingCanvas;
        [HideInInspector] public GameObject customizationCanvas;
        [HideInInspector] public GameObject inGameCanvas;
        [HideInInspector] public GameObject winCanvas;
        [HideInInspector] public GameObject joystickCanvas;
        [HideInInspector] public GameObject noBlockCanvas;

        [HideInInspector] public GameObject[] cannons;
        [HideInInspector] public GameObject crosshair;

        public GameObject shootText;


        [HideInInspector] public TextMeshProUGUI remainingText;
        [HideInInspector] public TextMeshProUGUI diamondText;
        public GameObject countdown;

        private bool TimerBool = false;

        public ParticleSystem confetti;
        [HideInInspector] public bool _changeScene = true;


        public Coroutine BlowUpCoroutine = null;
        void Start()
        {


            Instance = this;


            remainingText = remainingTimeText.GetComponent<TextMeshProUGUI>();
            remainingText.text = "00:20";
            remainingTime = 20;

            Transition = GameObject.Find("Image").GetComponent<Animator>();
            enemy1 = GameObject.Find("Enemy 1");
            enemy2 = GameObject.Find("Enemy 2");
            BlowUpCoroutine = null;

        }

        void FixedUpdate()
        {
            //diamondText.text = PlayerData.Instance.Diamond.ToString();

            if (remainingTime > 0 && started && !TimerBool)
            {

                StartCoroutine(Timer());

                if (remainingTime <= 5.0f)
                {
                    countdown.SetActive(true);
                    countdown.GetComponent<TextMeshProUGUI>().text = Math.Truncate(remainingTime).ToString();
                }

            }
            else if (remainingTime <= 0)
            {

                StopCoroutine(Timer());
                countdown.GetComponent<TextMeshProUGUI>().fontSize = 220f;
                countdown.GetComponent<TextMeshProUGUI>().text = "Time is up!";



                var towerBuild = GameObject.Find("MainTower").GetComponent<TowerBuild>();
                if (towerBuild.blocksList.Count != 0 && towerBuild.shootingScene == false)
                {
                    StartCoroutine(ChangeScene());
                }
                else if (towerBuild.blocksList.Count == 0 && towerBuild.shootingScene == false)
                {

                    enemy1.GetComponent<EnemySM>().ChangeStateIdle();
                    enemy2.GetComponent<EnemySM>().ChangeStateIdle();
                    joystickCanvas.SetActive(false);
                    noBlockCanvas.SetActive(true);
                    inGameCanvas.SetActive(false);
                }

            }

            //Win condition

            if (enemy1.GetComponent<EnemySM>().Name == "Lost" && enemy2.GetComponent<EnemySM>().Name == "Lost")
            {


                var _playerSM = GameObject.Find("Player").GetComponent<PlayerSM>();
                _playerSM.ChangeState(_playerSM.aimState);
                GameObject.Find("MainTower").GetComponent<TowerBuild>().myCannon.GetComponent<CannonShoot>().enabled = false;

                GameObject.Find("Player").GetComponent<Animator>().SetBool("Shoot", false);

                winCanvas.SetActive(true);
                GameObject.Find("Crosshair").GetComponent<WeaponShootS>().enabled = false;





                if (BlowUpCoroutine == null)
                {
                    confetti.Play();
                    BlowUpCoroutine = StartCoroutine(PlayerTowerBlowUp());
                }

            }



        }

        IEnumerator Timer()
        {
            TimerBool = true;
            yield return new WaitForSeconds(1);
            remainingTime -= 1;
            if (remainingTime < 10)
            {
                remainingText.text = "00:0" + remainingTime.ToString("f0");
            }
            else
            {
                remainingText.text = "00:" + remainingTime.ToString("f0");
            }

            TimerBool = false;


        }
        public void OpenCustomizationMenu()
        {
            openingCanvas.SetActive(false);
            customizationCanvas.SetActive(true);


            diamondText.text = PlayerData.Instance.Diamond.ToString();


        }
        public void CloseCustomizationMenu()
        {
            customizationCanvas.SetActive(false);
            openingCanvas.SetActive(true);
        }


        public void TapToStart()
        {

            started = true;
            inGameCanvas.SetActive(true);
            GameObject.Find("Enemy 2").GetComponent<EnemySM>().ChangeStateWalking();
            GameObject.Find("Enemy 1").GetComponent<EnemySM>().ChangeStateWalking();

            GameObject.Find("Opening Canvas").SetActive(false);




        }
        IEnumerator ChangeScene()
        {

            if (_changeScene)
            {
                Transition.SetTrigger("Start");

                yield return new WaitForSeconds(1f);
                countdown.GetComponent<TextMeshProUGUI>().text = "";
                Transition.ResetTrigger("Start");

                inGameCanvas.transform.GetChild(0).gameObject.SetActive(false);
                inGameCanvas.transform.GetChild(1).gameObject.SetActive(false);

                arrow.SetActive(false);
                spawner.SetActive(false);
                joystickCanvas.SetActive(false);

                var player = GameObject.Find("Player");
                player.GetComponent<PlayerSM>().enabled = false;

                Camera.main.GetComponent<CameraScript>().offset = new Vector3(0, 12f, -15f);
                Camera.main.GetComponent<CameraScript>().damping = 10f;




                Camera.main.transform.localRotation = Quaternion.Euler(29, 0, 0);
                player.GetComponent<Animator>().SetBool("Aim", true);
                enemy1.GetComponent<EnemySM>().ChangeStateChoose();
                enemy2.GetComponent<EnemySM>().ChangeStateChoose();

                player.transform.GetChild(2).gameObject.SetActive(false);
                enemy1.transform.GetChild(2).gameObject.SetActive(false);
                enemy2.transform.GetChild(2).gameObject.SetActive(false);

                enemy1.GetComponent<Animator>().SetTrigger("Shoot");
                enemy2.GetComponent<Animator>().SetTrigger("Shoot");

                var maintower = GameObject.Find("MainTower");

                player.transform.parent = maintower.transform.GetChild(0).GetChild(0).GetChild(0);
                player.transform.localPosition = new Vector3(-2, -20, -45);
                player.transform.localRotation = Quaternion.Euler(0, 0, 0);
                player.GetComponent<Animator>().SetBool("Aim", true);
                player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;


                enemy1.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
                enemy2.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;

                enemy1.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                enemy2.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

                tower1.GetComponent<TowerBuild>().shootingScene = true;
                tower2.GetComponent<TowerBuild>().shootingScene = true;
                maintower.GetComponent<TowerBuild>().shootingScene = true;



                enemy1.transform.parent = GameObject.Find("Tower 1").transform.GetChild(0).GetChild(0);
                enemy2.transform.parent = GameObject.Find("Tower 2").transform.GetChild(0).GetChild(0);

                enemy1.transform.localPosition = new Vector3(-1, 58, 16);
                enemy2.transform.localPosition = new Vector3(-1, 58, 16);

                enemy1.transform.localRotation = Quaternion.Euler(0, 0, 0);
                enemy2.transform.localRotation = Quaternion.Euler(0, 0, 0);



                foreach (Block go in FindObjectsOfType(typeof(Block)) as Block[])
                {
                    var rb = go.GetComponent<Rigidbody>();
                    rb.constraints = ~RigidbodyConstraints.FreezePositionY;
                    rb.useGravity = true;
                }



                crosshair.SetActive(true);
                Transition.SetTrigger("End");
                shootText.SetActive(true);

                yield return new WaitForSeconds(1f);
                shootText.SetActive(true);

                foreach (var item in cannons)
                {
                    item.GetComponent<CannonShoot>().enabled = true;

                }


                _changeScene = false;
                StopCoroutine(ChangeScene());

            }


        }
        public void WinCondition()
        {

            Transition.SetTrigger("Start");
            SceneManager.LoadScene("CollectScene");

            PlayerData.Instance.curLevel++;

            if (PlayerData.Instance.curLevel != 0 && PlayerData.Instance.curArena < 2 && PlayerData.Instance.curLevel % 4 == 0)
            {
                PlayerData.Instance.curArena++;
            }


        }

        IEnumerator PlayerTowerBlowUp()
        {

            var towerBlocks = GameObject.Find("MainTower").GetComponent<TowerBuild>().blocksList.ToList();

            if (towerBlocks.Count != 0)
            {
                for (int i = towerBlocks.Count - 1; i > 0; i--)
                {
                    GameObject _block = towerBlocks[i];
                    yield return new WaitForSeconds(.25f);
                    PlayerData.Instance.AddDiamond(10);
                    towerBlocks.Remove(_block);
                    Destroy(_block);
                }

            }
            else
            {
                StopCoroutine(PlayerTowerBlowUp());
                yield return null;
            }


        }
        public void FailCondition()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        }

    }
}


