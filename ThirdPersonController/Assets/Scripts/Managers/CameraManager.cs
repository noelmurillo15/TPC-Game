using UnityEngine;
using SA.Scriptable;


namespace SA
{
    public class CameraManager : MonoBehaviour
    {   //  Manages the Camera w/ Pivot
        #region Class Members
        public bool lockOn;
        public float followSpeed = 9f;
        public float mouseSpeed = 2f;
        public float controllerSpeed = 7f;

        public Transform m_target;
        public Transform m_transform;
        public TransformVariable m_lockOnTransform;
        [HideInInspector] public Transform m_pivot;
        [HideInInspector] public Transform m_cameraTransform;

        float turnSmoothing = 0.1f;
        public float minAngle = -45f;
        public float maxAngle = 45f;

        float smoothX, smoothY;
        float smoothXVelocity, smoothYVelocity;

        float curZ;
        public float defZ;
        public float zSpeed;

        public float lookAngle;
        public float tiltAngle;

        bool usedRightAxis;
        bool changeTagetLeft;
        bool changeTargetRight;

        StateManager m_stateManager;
        #endregion


        public void Init(StateManager _stateManager)
        {
            m_transform = this.transform;
            m_stateManager = _stateManager;
            m_target = m_stateManager.transform;

            if (m_cameraTransform == null) m_cameraTransform = Camera.main.transform;
            if (m_pivot == null) m_pivot = m_cameraTransform.parent;

            m_lockOnTransform.value = null;
        }

        public void Tick(float _delta, float _mouseX, float _mouseY)
        {
            float targetSpeed = controllerSpeed;

            FollowTarget(_delta);
            HandleRotation(_delta, _mouseY, _mouseX, targetSpeed);
            HandlePivotPosition();
        }

        void FollowTarget(float _delta)
        {
            transform.position = Vector3.Lerp(transform.position, m_target.position, _delta);
        }

        void HandleRotation(float _delta, float _vertical, float _horizontal, float targetSpeed)
        {
            if (turnSmoothing > 0)
            {   //  TurnSmoothing - calculate smooth damp
                smoothX = Mathf.SmoothDamp(smoothX, _horizontal, ref smoothXVelocity, turnSmoothing);
                smoothY = Mathf.SmoothDamp(smoothY, _vertical, ref smoothYVelocity, turnSmoothing);
            }
            else
            {   //  Get Raw Input
                smoothX = _horizontal;
                smoothY = _vertical;
            }

            if (m_lockOnTransform.value != null)
            {   //  LockOn Target
                Vector3 targetDir = m_lockOnTransform.value.position - transform.position;
                targetDir.Normalize();
                targetDir.y = 0f;

                if (targetDir == Vector3.zero)
                    targetDir = transform.forward;

                Quaternion targetRot = Quaternion.LookRotation(targetDir);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, _delta * followSpeed);
                lookAngle = transform.eulerAngles.y;

                Vector3 tiltDir = m_lockOnTransform.value.position - m_pivot.position;
                tiltDir.Normalize();
                if (tiltDir == Vector3.zero)
                    tiltDir = m_pivot.forward;

                Quaternion tiltRot = Quaternion.LookRotation(tiltDir);
                Vector3 tiltEulers = Quaternion.Slerp(m_pivot.rotation, tiltRot, _delta * followSpeed).eulerAngles;
                tiltEulers.y = 0f;
                tiltAngle = tiltEulers.x;
                return;
            }

            //  Rotates on Y-Axis of CameraManager Gameobject which moves Screen Space Horizontally
            lookAngle += smoothX * targetSpeed;
            transform.rotation = Quaternion.Euler(0f, lookAngle, 0f);

            //  Rotates on X-Axis of Pivot Gameobject which moves Screen Space Vertically
            tiltAngle -= smoothY * targetSpeed;
            tiltAngle = Mathf.Clamp(tiltAngle, minAngle, maxAngle);
            m_pivot.localRotation = Quaternion.Euler(tiltAngle, 0f, 0f);
        }

        void HandlePivotPosition()
        {
            float targetZ = defZ;
            CameraCollision(defZ, ref targetZ);

            curZ = Mathf.Lerp(curZ, targetZ, m_stateManager.m_delta * zSpeed);
            Vector3 tp = Vector3.zero;
            tp.z = curZ;
            tp.y = 2.24f;
            m_cameraTransform.localPosition = tp;
        }

        void CameraCollision(float targetZ, ref float actualZ)
        {   //  TODO : Implement

        }


        public static CameraManager singleton;
        private void Awake()
        {
            singleton = this;
        }
    }
}