using Assets.Scripts.Components;
using Extentions;
using System.Collections;
using UnityEngine;
namespace Assets.Scripts.Tower
{
    public class WeaponShootS : MonoBehaviour
    {
        private PlayerSM _playerSM;
        private GameObject player;
        private Animator playerAnimator;
        public RaycastHit hit;


        private Touch touch;
        public float speed;


        public Camera cmvcam;
        public Camera shootCam;

        public Joystick joystick;
        public GameObject weapon, chair, muzzle, hiteffect;
        public float _firerate;
        private int placeholderInt = 1;

        private bool CanFire = true;

        void Start()
        {
            cmvcam = shootCam;
            player = GameObject.Find("Player");
            playerAnimator = player.GetComponent<Animator>();
            _playerSM = player.GetComponent<PlayerSM>();

            muzzle = GameObject.Find("MainTower/TowerTop/WeaponsComplete/WeaponChair/WeaponDeck/Weapons/MuzzleFlash");
            muzzle.SetActive(true);



            //if (joystick != null)
            //{
            //    joystick.OnMove += Joystick_OnMove;
            //    joystick.OnMoveFinished += Joystick_OnMoveFinished;
            //}



            weapon = GameObject.Find("MainTower/TowerTop/WeaponsComplete/WeaponChair/WeaponDeck");
            chair = GameObject.Find("MainTower/TowerTop/WeaponsComplete/WeaponChair");

        }

        //private void Joystick_OnMoveFinished(object sender, EventArgs e)
        //{
        //    //throw new NotImplementedException();
        //}

        //private void Joystick_OnMove(object sender, Vector2 e)
        //{
        //    if (e.magnitude > 0)
        //    {

        //        var normal = new Vector3(e.x, e.y, 0);
        //        if (transform.localPosition.x <= -10 && normal.y < 0)
        //            normal = normal.WithY(0);



        //        if (transform.localPosition.z <= -8 && normal.y > 0)
        //            normal = normal.WithY(0);

        //        transform.Translate(normal / 10, Space.World);

        //    }
        //}



        void Update()
        {


            if (Input.touchCount > 0)
            {

                touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Moved)
                {

                    transform.position = new Vector3(

                        transform.position.x + touch.deltaPosition.x * speed,
                        transform.position.y + touch.deltaPosition.y * speed,
                        transform.position.z);

                }
            }

            var limitx = Mathf.Clamp(transform.position.x, -3.5f, 3.5f);
            var limity = Mathf.Clamp(transform.position.y, 1.75f, 10f);
            transform.position = new Vector3(limitx, limity, -7.5f);




            gameObject.transform.LookAt(Camera.main.transform);

            Shoot();


            var target = hit.point;
            //Chair Aim
            var chairTarget = target - chair.transform.position;
            chairTarget.y = 0;
            chair.transform.rotation = Quaternion.Slerp(chair.transform.rotation, Quaternion.LookRotation(chairTarget), 3f * Time.deltaTime);


            //Weapon Aim
            var weaponTarget = target - weapon.transform.position;
            weapon.transform.rotation = Quaternion.Slerp(weapon.transform.rotation, Quaternion.LookRotation(weaponTarget), 3f * Time.deltaTime);

        }

        public void Shoot()
        {
            if (Physics.Raycast(shootCam.transform.position, (gameObject.transform.position - shootCam.transform.position), out hit))
            {

                if (CanFire)
                {
                    Debug.DrawLine(shootCam.transform.position, hit.point, Color.red);

                    if (hit.transform.TryGetComponent(out Cannonball ball))
                    {



                        ball.GetDamage();

                        var muzflash = muzzle.GetComponent<ParticleSystem>();
                        muzflash.Play();

                        StartCoroutine(BulletTravel());



                    }

                    CanFire = false;
                    TasksExtentions.DoActionAfterSecondsAsync(() => { CanFire = true; }, _firerate);
                }

            }

        }
        public IEnumerator BulletTravel()
        {

            var bullet = BulletPoolManager.Instance.GetObject("Bullet");
            var t = 0f;
            var speed = 8f;
            var target = hit.point;
            do
            {
                bullet.SetActive(true);

                t += Time.deltaTime * speed;
                var _start = muzzle.transform.position;
                var _target = target;


                var _controlPoint = ((_target + _start) / 2);
                playerAnimator.Play("Aim", -1, 0f);
                bullet.transform.position = CurveExtensions.GetQuadraticCurvePoint(t, _start, _controlPoint, _target);

                if (t >= 0.9f)
                {
                    hiteffect.transform.position = _target;
                    hiteffect.transform.LookAt(muzzle.transform);
                    hiteffect.GetComponent<ParticleSystem>().Play();

                    bullet.transform.position = _target;
                    bullet.transform.rotation = bullet.transform.rotation;
                    BulletPoolManager.Instance.SetObject("Bullet", bullet);
                    _target = Vector3.zero;
                }

                yield return new WaitForSeconds(3 / 100);

            } while (t < 1);


            yield return new WaitForSeconds(1);

        }

    }
}


