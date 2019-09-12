using UnityEngine;


namespace SA
{
    public class StateManager : MonoBehaviour
    {
        public States states;
        public InputVariables input;
        public ControllerStats stats;
        public GameObject activeModel;

        #region References
        [HideInInspector]
        public Animator m_animator;
        [HideInInspector]
        public Rigidbody m_rigidbody;
        [HideInInspector]
        public Collider m_collider;
        #endregion

        #region LayerRefs
        [HideInInspector]
        public LayerMask ignoreLayers;
        [HideInInspector]
        public LayerMask ignoreForGroundCheck;
        #endregion

        public float delta;
        public Transform m_transform;

        public enum CharacterState
        {
            MOVING, ON_AIR, INTERACTING, ATTACKING
        }

        public CharacterState characterState;


        public void Init()
        {
            m_transform = transform;
            SetupAnimator();

            //  Layer masks
            gameObject.layer = 8;
            ignoreLayers = ~(1 << 9);
            ignoreForGroundCheck = ~(1 << 9 | 1 << 10);

            m_rigidbody = GetComponent<Rigidbody>();
            m_rigidbody.angularDrag = 999;
            m_rigidbody.drag = 4;
            m_rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;

            m_collider = GetComponent<Collider>();
        }

        void SetupAnimator()
        {
            if (activeModel == null)
            {
                m_animator = activeModel.GetComponent<Animator>();
                activeModel = m_animator.gameObject;
            }

            if (m_animator == null)
                m_animator = GetComponentInChildren<Animator>();

            if (m_animator != null)
            {
                m_animator.applyRootMotion = false;
                m_animator.GetBoneTransform(HumanBodyBones.LeftHand).localScale = Vector3.one;
                m_animator.GetBoneTransform(HumanBodyBones.RightHand).localScale = Vector3.one;
            }
        }

        public void Fixed_Tick(float _delta)
        {
            delta = _delta;
            states.onGround = OnGroundCheck();

            switch (characterState)
            {
                case CharacterState.MOVING:
                    HandleRotation();
                    HandleMovement();
                    break;
                case CharacterState.INTERACTING:
                    break;
                case CharacterState.ATTACKING:
                    break;
                case CharacterState.ON_AIR:
                    break;
            }
        }

        public void Tick(float _delta)
        {
            delta = _delta;
            states.onGround = OnGroundCheck();

            switch (characterState)
            {
                case CharacterState.MOVING:
                    HandleMovementAnimations();
                    break;
                case CharacterState.INTERACTING:
                    break;
                case CharacterState.ATTACKING:
                    break;
                case CharacterState.ON_AIR:
                    break;
            }
        }

        void HandleRotation()
        {
            Vector3 targetDir = (states.isLockedOn == false) ?
             input.moveDir : (input.lockOnTransform != null) ?  //  TODO : this might be T == null
             input.lockOnTransform.position - m_transform.position : input.moveDir;

            targetDir.y = 0f;
            if (targetDir == Vector3.zero)
            {
                targetDir = m_transform.forward;
            }

            Quaternion tr = Quaternion.LookRotation(targetDir);
            Quaternion targtRotation = Quaternion.Slerp(m_transform.rotation, tr, delta * stats.rotateSpeed * input.moveAmount);

            m_transform.rotation = targtRotation;
        }

        void HandleMovement()
        {
            Vector3 velocity = m_transform.forward;

            if (states.isLockedOn)
            {   //  LockOn HandleMovement   TODO : this might be outside of its own scope
                velocity = input.moveDir;
            }

            if (input.moveAmount > 0) 
            { m_rigidbody.drag = 0f; }
            else { m_rigidbody.drag = 4f; }

            //  Free Movement                
            if (!states.isRunning)
                velocity *= input.moveAmount * stats.moveSpeed;
            else
                velocity *= input.moveAmount * stats.sprintSpeed;

            // velocity.y = m_rigidbody.velocity.y;
            m_rigidbody.velocity = velocity;
        }

        void HandleMovementAnimations()
        {
            if (!states.isLockedOn)
            {
                m_animator.SetBool(StaticStrings.run, states.isRunning);
                m_animator.SetFloat(StaticStrings.vertical, input.moveAmount, 0.15f, delta);
            }
        }

        bool OnGroundCheck()
        {
            bool returnVal = false;

            Vector3 origin = m_transform.position;
            origin.y += 0.4f;
            Vector3 dir = -Vector3.up;

            float distance = 0.7f;
            RaycastHit hit;

            Debug.DrawRay(origin, dir * distance);

            if (Physics.Raycast(origin, dir, out hit, distance, ignoreForGroundCheck))
            {
                returnVal = true;
                Vector3 targetposition = hit.point;
                m_transform.position = targetposition;
            }

            return returnVal;
        }
    }


    [System.Serializable]
    public class InputVariables
    {
        public float horizontal;
        public float vertical;
        public float moveAmount;
        public Vector3 moveDir;
        public Transform lockOnTransform;
    }

    [System.Serializable]
    public class States
    {
        public bool onGround;
        public bool isRunning;
        public bool isLockedOn;
        public bool isInAction;
        public bool isMoveEnabled;
        public bool isDamageOn;
        public bool isRotateEnabled;
        public bool isAttackEnabled;
        public bool isSpellCasting;
        public bool isIKEnabled;
        public bool isUsingItem;
        public bool isAbleToBeParried;
        public bool isParryOn;
        public bool isLeftHand;
        public bool animIsOnEmpty;
        public bool closeWeapons;
        public bool isInvisable;
    }

    [System.Serializable]
    public class NetworkStates
    {
        public bool isLocal;
        public bool isInRoom;
    }
}