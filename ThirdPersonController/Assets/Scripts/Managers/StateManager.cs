using UnityEngine;
using SA.Scriptable;


namespace SA
{
    public class StateManager : MonoBehaviour
    {
        #region Class Variables  
        public enum CharacterState
        {
            MOVING, ON_AIR, INTERACTING, ATTACKING
        }

        public CharacterState characterState;   //  Current State 
        public States states;                   //  State Info
        public InputVariables input;            //  Input Info
        public ControllerStats stats;           //  Movement Info
        public WeaponManager weaponManager;     //  Weapon Info
        public GameObject activeModel;
        public float delta;
        #endregion

        #region References
        Transform m_transform;

        [HideInInspector] public Animator m_animator;
        [HideInInspector] public Rigidbody m_rigidbody;
        [HideInInspector] public Collider m_collider;
        [HideInInspector] public AnimatorHook animatorHook;
        #endregion

        #region LayerRefs
        [HideInInspector] public LayerMask ignoreLayers;
        [HideInInspector] public LayerMask ignoreForGroundCheck;
        #endregion


        #region Initialization
        public void Init()
        {
            SetupAnimator();
            m_transform = transform;

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
        {   //  This will fail if no animator is attached
            if (activeModel == null)
            {   //  If designer forgets to attach the activeaModel ~ this will find the model via Animator
                m_animator = GetComponentInChildren<Animator>();
                activeModel = m_animator.gameObject;
            }

            if (m_animator == null) //  If animator has not been found, find via ActiveModel
                m_animator = activeModel.GetComponent<Animator>();
            else
            {   //  If animator was Ultimately found
                m_animator.applyRootMotion = false;
                m_animator.GetBoneTransform(HumanBodyBones.LeftHand).localScale = Vector3.one;
                m_animator.GetBoneTransform(HumanBodyBones.RightHand).localScale = Vector3.one;

                animatorHook = activeModel.AddComponent<AnimatorHook>();
                animatorHook.Init(this);
            }
        }
        #endregion

        #region Updates
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
                    m_rigidbody.drag = 0f;
                    Vector3 v = m_rigidbody.velocity;
                    Vector3 tv = input.animationDelta;
                    tv *= 50f;
                    tv.y = v.y;
                    m_rigidbody.velocity = tv;
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
                    bool interact = InteractionInputCheck();
                    if(!interact) HandleMovementAnimations();
                    break;
                case CharacterState.INTERACTING:
                    break;
                case CharacterState.ATTACKING:
                    states.animIsInteracting = m_animator.GetBool("isInteracting");
                    if (states.animIsInteracting == false)
                    {
                        if (states.isInteracting)
                        {
                            states.isInteracting = false;
                            ChangeState(CharacterState.MOVING);
                        }
                    }
                    break;
                case CharacterState.ON_AIR:
                    break;
            }
        }
        #endregion

        #region StateManager Functions
        bool OnGroundCheck()
        {
            bool returnVal = false;

            Vector3 origin = m_transform.position;
            origin.y += 0.4f;
            Vector3 dir = -Vector3.up;

            float distance = 0.7f;
            RaycastHit hit;

            if (Physics.Raycast(origin, dir, out hit, distance, ignoreForGroundCheck))
            {
                returnVal = true;
                Vector3 targetposition = hit.point;
                m_transform.position = targetposition;
            }

            return returnVal;
        }

        bool InteractionInputCheck()
        {
            Action a = null;
            if (input.rb)
            {
                a = GetAction(InputType.RB);
                if (a != null)
                {
                    HandleAction(a);
                    return true;
                }
            }

            if (input.lb)
            {
                a = GetAction(InputType.LB);
                if (a != null)
                {
                    HandleAction(a);
                    return true;
                }
            }

            if (input.rt)
            {
                a = GetAction(InputType.RT);
                if (a != null)
                {
                    HandleAction(a);
                    return true;
                }
            }

            if (input.lt)
            {
                a = GetAction(InputType.LT);
                if (a != null)
                {
                    HandleAction(a);
                    return true;
                }
            }

            return false;
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

            m_rigidbody.velocity = velocity;
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

        void HandleMovementAnimations()
        {
            if (!states.isLockedOn)
            {
                m_animator.SetBool(StaticStrings.run, states.isRunning);
                m_animator.SetFloat(StaticStrings.vertical, input.moveAmount, 0.15f, delta);
            }
        }

        void HandleAction(Action _action)
        {
            switch (_action.actionType)
            {
                case ActionType.ATTACK:
                    AttackAction attackAction = (AttackAction)_action.actionObj;
                    PlayAttackAction(_action, attackAction);
                    break;
                case ActionType.BLOCK:
                    break;
                case ActionType.PARRY:
                    break;
                case ActionType.SPELL:
                    break;

                default:
                    break;
            }
        }

        void PlayAttackAction(Action _action, AttackAction _attackAction)
        {
            //  Is the action a right-handed action or left?
            m_animator.SetBool(StaticStrings.mirror, _action.mirrorAnimation);

            //  Play the Attack Animation
            PlayActionAnimation(_attackAction.attackAnimation.value);

            //  Change the anim speed if necessary            
            if (_attackAction.changeSpeed)
            { m_animator.SetFloat("speed", _attackAction.animSpeed); }

            //  Switch State
            ChangeState(CharacterState.ATTACKING);
        }

        void PlayActionAnimation(string _animationName)
        {   //  The layer parameter is refering to Animator Controller Layer 
            // m_animator.PlayInFixedTime(_animationName, 5, 0.2f);    //  Pass in the Override Layer where attacks take place
            Debug.Log("Playing Animation : " + _animationName);
            m_animator.CrossFade(_animationName, 0.2f);
        }   //  PlayInFixedTime is kinda similar to CrossFade Animation but slightly better

        void ChangeState(CharacterState _state)
        {
            characterState = _state;
            switch (_state)
            {
                case CharacterState.MOVING:
                    m_animator.applyRootMotion = false;
                    break;
                case CharacterState.INTERACTING:
                    m_animator.applyRootMotion = false;
                    break;
                case CharacterState.ATTACKING:
                    m_animator.applyRootMotion = true;
                    m_animator.SetBool("isInteracting", true);
                    states.isInteracting = true;
                    break;
                case CharacterState.ON_AIR:
                    m_animator.applyRootMotion = false;
                    break;
            }
        }

        Action GetAction(InputType _inputType)
        {
            WeaponManager.ActionContainer ac = weaponManager.GetAction(_inputType);
            if (ac == null)
                return null;
            return ac.action;
        }
        #endregion        
    }

    #region Helper Classes
    [System.Serializable]
    public class WeaponManager
    {   //  Control what actions to do based on button input
        public ActionContainer[] actions;

        public ActionContainer GetAction(InputType _inputType)
        {
            for (int x = 0; x < actions.Length; x++)
            {
                if (actions[x].inputType == _inputType)
                    return actions[x];
            }
            return null;
        }

        public void Init()
        {
            actions = new ActionContainer[4];
            for (int x = 0; x < actions.Length; x++)
            {
                ActionContainer a = new ActionContainer();
                a.inputType = (InputType)x;
                actions[x] = a;
            }
        }

        [System.Serializable]
        public class ActionContainer
        {
            public InputType inputType;
            public Action action;
        }
    }

    [System.Serializable]
    public class InputVariables
    {
        public float horizontal;
        public float vertical;
        public float moveAmount;
        public Vector3 moveDir;
        public Vector3 animationDelta;
        public Transform lockOnTransform;

        public bool rt;
        public bool lt;
        public bool rb;
        public bool lb;
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
        public bool animIsInteracting;
        public bool isInteracting;
        public bool closeWeapons;
        public bool isInvisable;
    }

    [System.Serializable]
    public class NetworkStates
    {
        public bool isLocal;
        public bool isInRoom;
    }
    #endregion
}