/*
* RotateViaInputAxis - Uses CameraInputAxis to rotate the targetTransform
* Created by : Allan N. Murillo
* Last Edited : 3/12/2020
*/

using UnityEngine;
using ANM.Scriptables.Variables;

namespace ANM.Actions
{
    [CreateAssetMenu(menuName = "MonoActions/Rotate Via Input Axis")]
    public class RotateViaInputAxis : Action
    {
        public bool clamp;
        public bool negative;
        public float minClamp = -35f;
        public float maxClamp = 35f;
        public float speed;
        public float angle;
        public CameraInputAxis axis;
        public RotateAxis targetAxis;
        public RotateAxis targetInputAxis;
        public TransformVariable targetTransform;
        
        
        public override void Execute()
        {
            switch (targetInputAxis)
            {
                case RotateAxis.X:
                    if(!negative)
                        angle += axis.value.x * speed;
                    else
                        angle -= axis.value.x * speed;
                    break;
                case RotateAxis.Y:
                    if(!negative)
                        angle += axis.value.y * speed;
                    else
                        angle -= axis.value.y * speed;
                    break;
            }

            if (clamp)
            {
                angle = Mathf.Clamp(angle, minClamp, maxClamp);
            }

            switch (targetAxis)
            {
                case RotateAxis.X:
                    targetTransform.value.localRotation = Quaternion.Euler(angle, 0f, 0f);
                    break;
                case RotateAxis.Y:
                    targetTransform.value.localRotation = Quaternion.Euler(0f, angle, 0f);
                    break;
                case RotateAxis.Z:
                    targetTransform.value.localRotation = Quaternion.Euler(0f, 0f, angle);
                    break;
            }
        }

        public enum RotateAxis
        {
            X,Y,Z
        }
    }
}