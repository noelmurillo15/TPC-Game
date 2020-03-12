/*
* RotateViaInputAxis - 
* Created by : Allan N. Murillo
* Last Edited : 3/10/2020
*/

using UnityEngine;
using ANM.Scriptables.Variables;

namespace ANM.Behaviour.Actions
{
    [CreateAssetMenu(menuName = "Behaviours/MonoActions/RotateViaInputAxis")]
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
                case RotateAxis.x:
                    if(!negative)
                        angle += axis.value.x * speed;
                    else
                        angle -= axis.value.x * speed;
                    break;
                case RotateAxis.y:
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
                case RotateAxis.x:
                    targetTransform.value.localRotation = Quaternion.Euler(angle, 0f, 0f);
                    break;
                case RotateAxis.y:
                    targetTransform.value.localRotation = Quaternion.Euler(0f, angle, 0f);
                    break;
                case RotateAxis.z:
                    targetTransform.value.localRotation = Quaternion.Euler(0f, 0f, angle);
                    break;
            }
        }

        public enum RotateAxis
        {
            x,y,z
        }
    }
}