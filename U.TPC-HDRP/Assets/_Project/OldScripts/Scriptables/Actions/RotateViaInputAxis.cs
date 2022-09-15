/*
* RotateViaInputAxis - Uses CameraInputAxis to rotate the targetTransform
* Created by : Allan N. Murillo
* Last Edited : 5/7/2020
*/

using System;
using UnityEngine;
using ANM.Scriptables.Variables;

namespace ANM.Scriptables.Actions
{
    [CreateAssetMenu(menuName = "Scriptables/MonoActions/Rotate Via Input Axis")]
    public class RotateViaInputAxis : Action
    {
        [NonSerialized] private float _angle;
        public bool clamp;
        public bool negative;
        public float minClamp = -35f;
        public float maxClamp = 35f;
        public float speed;

        public CameraInputAxis axis;
        public RotateAxis targetAxis;
        public RotateAxis targetInputAxis;
        public TransformVariable targetTransform;


        public override void Execute()
        {
            //    TODO : make this frame dependant (create mono action to track delta and store in Var) video 106
            var t = speed * Time.deltaTime;

            switch (targetInputAxis)
            {
                case RotateAxis.X:
                    _angle = !negative ?
                        Mathf.Lerp(_angle, _angle + axis.value.x, t) :
                        Mathf.Lerp(_angle, _angle - axis.value.x, t);
                    break;
                case RotateAxis.Y:
                    _angle = !negative ?
                        Mathf.Lerp(_angle, _angle + axis.value.y, t) :
                        Mathf.Lerp(_angle, _angle - axis.value.y, t);
                    break;
            }

            if (clamp)
            {
                _angle = Mathf.Clamp(_angle, minClamp, maxClamp);
            }

            switch (targetAxis)
            {
                case RotateAxis.X:
                    targetTransform.value.localRotation = Quaternion.Euler(_angle, 0f, 0f);
                    break;
                case RotateAxis.Y:
                    targetTransform.value.localRotation = Quaternion.Euler(0f, _angle, 0f);
                    break;
                case RotateAxis.Z:
                    targetTransform.value.localRotation = Quaternion.Euler(0f, 0f, _angle);
                    break;
            }
        }

        public enum RotateAxis
        {
            X,Y,Z
        }
    }
}
