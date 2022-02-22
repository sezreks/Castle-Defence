using Assets.Scripts.Canvas;
using UnityEngine;

namespace Assets.Scripts.WorldEnv
{
    public class CameraScript : MonoBehaviour
    {
        private GameObject _target;
        public float damping = .70f;
        public Vector3 offset;
        private GameObject mainCanvas;

        public void Awake()
        {
            _target = GameObject.FindGameObjectWithTag("Player");
            mainCanvas = GameObject.Find("MainCanvas");
        }


        void LateUpdate()
        {
            if (mainCanvas.GetComponent<MainCanvas>().started == true)
            {
                Vector3 desiredPosition = _target.transform.position + offset;
                Vector3 position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * damping);
                transform.position = position;

                //transform.LookAt(_target.transform.position);
            }

        }
    }

}