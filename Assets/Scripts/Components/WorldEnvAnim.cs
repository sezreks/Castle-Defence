using System;
using UnityEngine;

namespace Assets.Scripts
{
    internal class WorldEnvAnim : MonoBehaviour
    {
        public bool IsPlayOnStart;

        private bool IsPlaying;


        [Space(20)]
        public bool IsTranslate;
        [SerializeField] public Vector3Curve TranslateCurve;


        [Space(20)]
        public bool IsRotate;
        [SerializeField] public Vector3Curve RotateCurve;




        [Space(20)]
        public bool IsScale;
        [SerializeField] public Vector3Curve ScaleCurve;

        private float t = 0;




        private void Update()
        {
            if (IsPlaying)
            {
                t += Time.deltaTime;

                if (IsTranslate)
                    transform.localPosition = new Vector3(TranslateCurve.X.Evaluate(t), TranslateCurve.Y.Evaluate(t), TranslateCurve.Z.Evaluate(t));

                if (IsRotate)
                    transform.localEulerAngles = new Vector3(RotateCurve.X.Evaluate(t), RotateCurve.Y.Evaluate(t), RotateCurve.Z.Evaluate(t));

                if (IsScale)
                    transform.localScale = new Vector3(ScaleCurve.X.Evaluate(t), ScaleCurve.Y.Evaluate(t), ScaleCurve.Z.Evaluate(t));
            }
        }

        private void Start()
        {
            if (IsPlayOnStart)
                IsPlaying = true;
        }

        public void PlayAnim()
        {
            t = 0;
            IsPlaying = true;
        }

        public void ResetAnim()
        {
            t = 0;
            if (IsTranslate)
                transform.localPosition = new Vector3(TranslateCurve.X.Evaluate(t), TranslateCurve.Y.Evaluate(t), TranslateCurve.Z.Evaluate(t));

            if (IsRotate)
                transform.Rotate(RotateCurve.X.Evaluate(t), RotateCurve.Y.Evaluate(t), RotateCurve.Z.Evaluate(t));

            if (IsScale)
                transform.localScale = new Vector3(ScaleCurve.X.Evaluate(t), ScaleCurve.Y.Evaluate(t), ScaleCurve.Z.Evaluate(t));
        }
    }



    [Serializable]
    public struct Vector3Curve
    {
        public AnimationCurve X;
        public AnimationCurve Y;
        public AnimationCurve Z;
    }


}
