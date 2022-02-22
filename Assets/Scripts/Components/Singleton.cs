using Extentions;
using JetBrains.Annotations;
using UnityEngine;

namespace Assets.Scripts.Bases
{
    public abstract class Singleton<T> : MonoBehaviour where T : Component
    {
        [CanBeNull]
        private static T _instance;

        [NotNull]
        private static readonly object _lock = new object();

        [SerializeField]
        protected bool _persistent = true;

        [NotNull]
        public static T Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = FindObjectOfType<T>();
                        if (_instance == null)
                            _instance = new GameObject().GetComponentRequired<T>(true);
                    }
                    return _instance;
                }
            }
        }


        protected virtual void Awake()
        {
            _instance = this as T;
            if (_persistent)
                DontDestroyOnLoad(_instance);
        }
    }
}
