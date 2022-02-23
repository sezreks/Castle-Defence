using Assets.Scripts.Components;
using Extentions;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Tower
{
    public class EnemyWeaponShoot : MonoBehaviour
    {
        public Animator _animator;
        public RaycastHit hit;
        public GameObject muzzle, hiteffect;
        public float _firerate;
        private int placeholderInt = 1;
        private bool CanFire = true;

        // Update is called once per frame
        void Update()
        {
            Debug.DrawLine(muzzle.transform.position, muzzle.transform.position + muzzle.transform.forward * 1000, Color.red);
            if (Physics.Raycast(muzzle.transform.position, muzzle.transform.position + muzzle.transform.forward * 1000, out hit))
            {
                if (CanFire)
                {
                    CanFire = false;

                    if (hit.transform.TryGetComponent(out Cannonball ball))
                    {

                        ball.GetDamage();
                        FixedJobs();

                    }

                    Invoke("SetCanFire", _firerate);
                }


            }

        }


        private void FixedJobs()
        {

            _animator.SetTrigger("Shoot");
            muzzle.GetComponent<ParticleSystem>().Play();
            StartCoroutine(BulletTravel());
        }

        private void SetCanFire()
        {
            CanFire = true;
        }
        public IEnumerator BulletTravel()
        {

            var bullet = BulletPoolManager.Instance.GetObject("Bullet");
            var t = 0f;
            var speed = 4f;
            var _target = hit.point;

            do
            {


                if (bullet != null)
                {
                    bullet.SetActive(true);
                }
                else
                {
                    bullet = BulletPoolManager.Instance.GetObject("Bullet");
                }


                t += Time.deltaTime * speed;
                var _start = muzzle.transform.position;
                var target = _target;


                var _controlPoint = ((target + _start) / 2);

                bullet.transform.position = CurveExtensions.GetQuadraticCurvePoint(t, _start, _controlPoint, target);

                if (t >= 0.9f)
                {


                    hiteffect.transform.position = target;
                    hiteffect.transform.LookAt(muzzle.transform);
                    hiteffect.GetComponent<ParticleSystem>().Play();
                    BulletPoolManager.Instance.SetObject("Bullet", bullet);

                    bullet.transform.position = target;



                }

                yield return new WaitForSeconds(1 / 100);
            } while (t < 1);


            yield return new WaitForSeconds(1);

        }

    }
}