/*
 * CameraManager - 
 * Created by : Allan N. Murillo
 * Last Edited : 2/24/2020
 */

using UnityEngine;
using ANM.Framework.Variables;

namespace ANM.Managers
{
    public class CameraManager : MonoBehaviour
    {   //  Manages the Camera w/ Pivot
        public bool lockOn;
        public float followSpeed = 9f;
        public float mouseSpeed = 2f;
        public float controllerSpeed = 7f;

        public Transform myTarget;
        public Transform myTransform;
        public TransformVariable lockOnTransformVar;
        
        public float minAngle = -45f;
        public float maxAngle = 45f;

        public float defZ;
        public float zSpeed;
        public float lookAngle;
        public float tiltAngle;
        
        [HideInInspector] public Transform myPivot;
        [HideInInspector] public Transform myCameraTransform;
        
        private readonly float _turnSmoothing = 0.1f;
        private float _smoothX, _smoothY;
        private float _smoothXVelocity, _smoothYVelocity;
        private float _curZ;
        private bool _usedRightAxis;
        private bool _changeTargetLeft;
        private bool _changeTargetRight;
        private StateManager _stateManager;


        public void Init(StateManager states)
        {
            myTransform = transform;
            _stateManager = states;
            myTarget = _stateManager.transform;

            if (myCameraTransform == null) myCameraTransform = Camera.main.transform;
            if (myPivot == null) myPivot = myCameraTransform.parent;

            lockOnTransformVar.value = null;
        }

        public void Fixed_Tick(float delta, float mouseX, float mouseY)
        {
            FollowTarget(delta);
            HandleRotation(delta, mouseY, mouseX, controllerSpeed);
            HandlePivotPosition();
        }

        
        private void FollowTarget(float d)
        {
            myTransform.position = Vector3.Lerp(myTransform.position, myTarget.position, d);
        }

        private void HandleRotation(float d, float vertical, float horizontal, float speed)
        {
            if (_turnSmoothing > 0)
            {   //    Apply Input Smoothing
                _smoothX = Mathf.SmoothDamp(
                    _smoothX, horizontal, ref _smoothXVelocity, _turnSmoothing);
                _smoothY = Mathf.SmoothDamp(
                    _smoothY, vertical, ref _smoothYVelocity, _turnSmoothing);
            }
            else
            {   //    Apply Raw Input
                _smoothX = horizontal;
                _smoothY = vertical;
            }

            if (lockOnTransformVar.value != null)
            {    //    Override Tilt & Look Input when LockedOn
                Vector3 targetDir = lockOnTransformVar.value.position - myTransform.position;
                targetDir.Normalize();
                targetDir.y = 0f;

                if (targetDir == Vector3.zero)
                    targetDir = myTransform.forward;

                var targetRot = Quaternion.LookRotation(targetDir);
                myTransform.rotation = Quaternion.Slerp(myTransform.rotation, targetRot, d * followSpeed);
                lookAngle = myTransform.eulerAngles.y;

                Vector3 tiltDir = lockOnTransformVar.value.position - myPivot.position;
                tiltDir.Normalize();
                if (tiltDir == Vector3.zero)
                    tiltDir = myPivot.forward;

                var tiltRot = Quaternion.LookRotation(tiltDir);
                myPivot.rotation = Quaternion.Slerp(myPivot.rotation, tiltRot, d * followSpeed);
                tiltAngle = myPivot.eulerAngles.x;
                return;
            }

            //  Look : Rotates on Y-Axis of CameraManager Game object which moves Screen Space Horizontally
            lookAngle += _smoothX * speed;
            myTransform.rotation = Quaternion.Euler(0f, lookAngle, 0f);

            //  Tilt : Rotates on X-Axis of Pivot Game object which moves Screen Space Vertically
            tiltAngle -= _smoothY * speed;
            tiltAngle = Mathf.Clamp(tiltAngle, minAngle, maxAngle);
            myPivot.localRotation = Quaternion.Euler(tiltAngle, 0f, 0f);
        }

        private void HandlePivotPosition()
        {
            var targetZ = defZ;
            CameraCollision(defZ, ref targetZ);

            _curZ = Mathf.Lerp(_curZ, targetZ, _stateManager.deltaTime * zSpeed);
            var pivotPosition = Vector3.zero;
            pivotPosition.z = _curZ;
            pivotPosition.y = 2.24f;
            myCameraTransform.localPosition = pivotPosition;
        }

        private static void CameraCollision(float targetZ, ref float actualZ)
        {   //  TODO : Implement

        }


        public static CameraManager instance;
        private void Awake()
        {
            if(instance != null && instance != this) { Destroy(gameObject); }
            instance = this;
        }
    }
}