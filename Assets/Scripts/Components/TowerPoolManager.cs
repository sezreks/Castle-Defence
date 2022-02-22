using Assets.Scripts.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Components
{
    public class TowerPoolManager : Singleton<TowerPoolManager>
    {
        [SerializeField] private List<TowerPoolItem> ItemList;


        private Dictionary<string, Queue<GameObject>> _objectList = new Dictionary<string, Queue<GameObject>>();

        private void Awake()
        {
            foreach (var item in ItemList)
            {
                List<GameObject> _tempList = new List<GameObject>();
                for (int i = 0; i < item._count; i++)
                {
                    _tempList.Add(GameObject.Instantiate(item._object, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0), transform));

                    item._object.SetActive(false);

                }
                Instance._objectList.Add(item._name, new Queue<GameObject>(_tempList));
            }
        }



        public GameObject GetObject(string key)
        {
            if (!_objectList.ContainsKey(key)) return null;

            if (_objectList[key].Count > 0) return _objectList[key].Dequeue();

            var _temp = ItemList.FirstOrDefault<TowerPoolItem>(x => x._name == key);
            if (_temp != null)
                return GameObject.Instantiate(_temp._object, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0), transform);

            return null;
        }


        public void SetObject(string key, GameObject _object)
        {
            if (!_objectList.ContainsKey(key)) return;

            var _temp = ItemList.FirstOrDefault<TowerPoolItem>(x => x._name == key);

            if (_objectList[key].Count + 1 > _temp._count) Destroy(_object);

            _object.transform.parent = transform;
            _object.transform.position = new Vector3(0, 0, 0);
            _object.transform.rotation = new Quaternion(0, 0, 0, 0);
            _object.SetActive(false);
            _objectList[key].Enqueue(_object);
        }
    }


    [Serializable]
    public class TowerPoolItem
    {
        public string _name;
        public GameObject _object;
        public int _count;
    }
}

