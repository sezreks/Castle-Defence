using Assets.Scripts.Bases;
using Assets.Scripts.Components;
using Extentions;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
//using static UnityEditor.Progress;


namespace Assets.Scripts.WorldEnv
{
    public class Spawner : Singleton<Spawner>
    {

        public List<string> keys;
        public Collider[] _colliders;
        private void Start()
        {
            _colliders = GetComponentsInChildren<BoxCollider>();
            foreach (BoxCollider item in _colliders)
            {

                Vector3 _rndPoint = Extentions.BoundsExtentions.RandomPointInBounds(item.bounds);
                int i = Random.Range(0, keys.Count());
                var _object = ObjectPoolManager.Instance.GetObject(keys[i]);
                _object.transform.parent = item.transform;
                _object.transform.position = _rndPoint.WithY(0);
                _object.GetComponent<BoxCollider>().enabled = true;
                _object.SetActive(true);
            }
        }



        public void GenerateItem(BoxCollider item)
        {
            _ = TasksExtentions.DoActionAfterSecondsAsync(() =>
            {
                Vector3 _rndPoint = Extentions.BoundsExtentions.RandomPointInBounds(item.bounds);
                int i = Random.Range(0, keys.Count());
                var _object = ObjectPoolManager.Instance.GetObject(keys[i]);

                _object.transform.parent = item.transform;
                _object.transform.position = _rndPoint.WithY(0);
                _object.GetComponent<BoxCollider>().enabled = true;
                _object.SetActive(true);


            }, 3);

        }
    }



}
