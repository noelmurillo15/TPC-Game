using UnityEngine;


namespace SA
{
    public class CameraManager : MonoBehaviour
    {   //  Manages the Camera w/ Pivot
        public bool lockOn;
        public float followSpeed = 9f;
        public float mouseSpeed = 2f;
        public float controllerSpeed = 7f;

        public Transform target;
        public Transform lockOnTransform;
        public Transform pivot;
        public Transform cameraTransform;
        public Transform m_transform;

        float turnSmoothing = 0.1f;
        public float minAngle = -45f;
        public float maxAngle = 45f;

        float curZ;
        public float defZ;
        public float zSpeed;

        public float lookAngle;
        public float tiltAngle;

        bool usedRightAxis;

        bool changeTagetLeft;
        bool changeTargetRight;

        StateManager stateManager;


        public void Init(StateManager _stateManager)
        {
            m_transform = this.transform;
            stateManager = _stateManager;
            target = stateManager.transform;
        }

        public void Tick(float _delta)
        {
            float horiz = Input.GetAxis(StaticStrings.Mouse_X);
            float vert = Input.GetAxis(StaticStrings.Mouse_Y);

            float cH = Input.GetAxis(StaticStrings.RightAxis_X);
            float cV = Input.GetAxis(StaticStrings.RightAxis_Y);

            float targetSpeed = controllerSpeed;

            FollowTarget(_delta);
            HandleRotation(_delta, vert, horiz, targetSpeed);
            HandlePivotPosition();
        }

        void FollowTarget(float delta)
        {

        }

        void HandleRotation(float delta, float vert, float horiz, float targetSpeed)
        {

        }

        void HandlePivotPosition()
        {
            float targetZ = defZ;
            CameraCollision(defZ, ref targetZ);

            curZ = Mathf.Lerp(curZ, targetZ, stateManager.m_delta * zSpeed);
            Vector3 tp = Vector3.zero;
            tp.z = curZ;
            tp.y = 2.24f;
            cameraTransform.localPosition = tp;
        }

        void CameraCollision(float targetZ, ref float actualZ)
        {

        }


        public static CameraManager singleton;
        private void Awake()
        {
            singleton = this;
        }
    }
}