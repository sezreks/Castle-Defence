using Assets.Scripts.Components;
using Extentions;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.WorldEnv.Bricks.Base
{
    public abstract class Brick : MonoBehaviour
    {
        public string Name { get; protected set; }
        public string BlockName { get; protected set; }
        public bool IsGrounded { get => transform.parent.name == "Point"; }


        public Brick(string key, string blockkey)
        {
            Name = key;
            BlockName = blockkey;
        }
        private void Update()
        {
            if (gameObject.transform.localScale.x < 0.0020f)
            {

                gameObject.transform.localScale += new Vector3(0.0005f, 0.0005f, 0.0005f);

            }
        }
        public IEnumerator MoveToTarget(/*int count,*/ Transform collectibles)
        {
            var t = 0f;
            var speed = 6f;
            do
            {
                gameObject.transform.localScale = Vector3.one;
                t += Time.deltaTime * speed;

                var _start = transform.localPosition;
                var _target = collectibles.localPosition /* + (new Vector3(0, 25f, 0)  count)*/;


                var _controlPoint = ((_target + _start) / 2);
                _controlPoint = _controlPoint.WithX(_controlPoint.x + 1).WithY(_controlPoint.y + 1);
                transform.localPosition = CurveExtensions.GetQuadraticCurvePoint(t, _start, _controlPoint, _target);
                transform.rotation = Quaternion.Slerp(transform.rotation, collectibles.rotation, t);


                if (t >= 0.9f)
                {
                    transform.GetChild(0).gameObject.SetActive(false);
                    transform.GetChild(1).gameObject.SetActive(true);
                    transform.localPosition = Vector3.zero;
                    transform.localScale = Vector3.one;
                    transform.rotation = collectibles.rotation;
                    _target = Vector3.zero;
                }

                yield return new WaitForSeconds(1 / 100);
            } while (t < 1);
            yield return new WaitForSeconds(1);
        }

        public IEnumerator MoveToTower(Transform TowerTop)
        {
            var t = 0f;
            var speed = 9f;
            do
            {
                t += Time.deltaTime * speed;
                var _start = gameObject.transform.position;
                var _target = TowerTop.position;


                var _controlPoint = ((_target + _start) / 2);
                _controlPoint = _controlPoint.WithX(_controlPoint.x + 1).WithY(_controlPoint.y + 1);
                transform.position = CurveExtensions.GetQuadraticCurvePoint(t, _start, _controlPoint, _target);





                if (t >= 0.9f)
                {
                    transform.position = _target;
                    _target = Vector3.zero;
                    TasksExtentions.DoActionAfterSecondsAsync(() => { ObjectPoolManager.Instance.SetObject(Name, gameObject); }, 0.05f);


                }

                yield return new WaitForSeconds(1 / 100);



            } while (t < 1);

            yield return new WaitForSeconds(1);
        }

    }
}
