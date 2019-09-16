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

        public CharacterState m_characterState;       //  Current State 
        public States m_states;                       //  State Info
        public InputVariables m_input;                //  Input Info
        public ControllerStats m_stats;               //  Movement Info

        public WeaponManager m_weaponManager;         //  Weapon Info
        public InventoryManager m_inventoryManager;   // Inventory Info
        [HideInInspector] public Managers.ResourcesManager m_resourcesManager;

        public GameObject m_activeModel;
        public float m_delta;
        #endregion

        #region References
        public Transform m_transform;

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
        public void Initialize()
        {
            if (m_resourcesManager == null)
            {
                m_resourcesManager = Resources.Load("ResourceManager") as Managers.ResourcesManager;
            }

            SetupAnimator();
            SetupRigidBody();

            m_transform = transform;
            m_collider = GetComponent<Collider>();

            //  Layer masks
            gameObject.layer = 8;
            ignoreLayers = ~(1 << 9);
            ignoreForGroundCheck = ~(1 << 9 | 1 << 10);

            SetupInventoryManager();
            SetupWeaponManager();
        }

        void SetupAnimator()
        {   //  This will fail if no animator is attached
            if (m_activeModel == null)
            {   //  If designer forgets to attach the activeaModel ~ this will find the model via Animator
                m_animator = GetComponentInChildren<Animator>();
                m_activeModel = m_animator.gameObject;
            }

            if (m_animator == null) //  If animator has not been found, find via ActiveModel
                m_animator = m_activeModel.GetComponent<Animator>();
            else
            {   //  If animator was Ultimately found
                m_animator.applyRootMotion = false;
                m_animator.GetBoneTransform(HumanBodyBones.LeftHand).localScale = Vector3.one;
                m_animator.GetBoneTransform(HumanBodyBones.RightHand).localScale = Vector3.one;

                animatorHook = m_activeModel.AddComponent<AnimatorHook>();
                animatorHook.Init(this);
            }
        }

        void SetupRigidBody()
        {
            m_rigidbody = GetComponent<Rigidbody>();
            m_rigidbody.angularDrag = 999;
            m_rigidbody.drag = 4;
            m_rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        }

        void SetupInventoryManager()
        {
            if (m_inventoryManager.rightItem)
            {
                Debug.Log("RightItem is Equipped");
                WeaponToRuntime(m_inventoryManager.rightItem.obj, ref m_inventoryManager.rightSlot);
                EquipWeapon(m_inventoryManager.rightSlot, false);
            }
            if (m_inventoryManager.leftItem)
            {
                Debug.Log("LeftItem is Equipped");
                WeaponToRuntime(m_inventoryManager.leftItem.obj, ref m_inventoryManager.leftSlot);
                EquipWeapon(m_inventoryManager.leftSlot, true);
            }
        }

        void SetupWeaponManager()
        {
            if (m_inventoryManager.leftSlot == null && m_inventoryManager.rightSlot == null)
                return;

            if (m_inventoryManager.rightSlot != null)
            {   //  Right hand Take Prio ~?
                WeaponManager.ActionContainer rb = m_weaponManager.GetAction(InputType.RB);
                rb.action = m_inventoryManager.rightSlot.weaponData.GetAction(InputType.RB);

                WeaponManager.ActionContainer rt = m_weaponManager.GetAction(InputType.RT);
                rt.action = m_inventoryManager.rightSlot.weaponData.GetAction(InputType.RT);

                if (m_inventoryManager.leftSlot == null)
                {   //  If not Dual Wielding, Get Left Trigger & Bumper Actions from Right Hand
                    WeaponManager.ActionContainer lb = m_weaponManager.GetAction(InputType.LB);
                    lb.action = m_inventoryManager.rightSlot.weaponData.GetAction(InputType.LB);

                    WeaponManager.ActionContainer lt = m_weaponManager.GetAction(InputType.LT);
                    lt.action = m_inventoryManager.rightSlot.weaponData.GetAction(InputType.LT);
                }
                else
                {   //  If Dual Wielding, Get leftHand Actions
                    WeaponManager.ActionContainer lb = m_weaponManager.GetAction(InputType.LB);
                    lb.action = m_inventoryManager.leftSlot.weaponData.GetAction(InputType.LB);

                    WeaponManager.ActionContainer lt = m_weaponManager.GetAction(InputType.LT);
                    lt.action = m_inventoryManager.leftSlot.weaponData.GetAction(InputType.LT);
                }
                return;
            }

            if (m_inventoryManager.leftSlot != null)
            {   //  There is no right hand equipped so grab all actions from Left Hand
                WeaponManager.ActionContainer rb = m_weaponManager.GetAction(InputType.RB);
                rb.action = m_inventoryManager.leftSlot.weaponData.GetAction(InputType.RB);

                WeaponManager.ActionContainer rt = m_weaponManager.GetAction(InputType.RT);
                rt.action = m_inventoryManager.leftSlot.weaponData.GetAction(InputType.RT);

                WeaponManager.ActionContainer lb = m_weaponManager.GetAction(InputType.LB);
                lb.action = m_inventoryManager.leftSlot.weaponData.GetAction(InputType.LB);

                WeaponManager.ActionContainer lt = m_weaponManager.GetAction(InputType.LT);
                lt.action = m_inventoryManager.leftSlot.weaponData.GetAction(InputType.LT);
            }
        }

        void WeaponToRuntime(Object _obj, ref Inventory.RuntimeWeapon _slot)
        {
            Inventory.Weapon weaponData = (Inventory.Weapon)_obj;
            GameObject weaponInstance = Instantiate(weaponData.modelPrefab) as GameObject;

            weaponInstance.SetActive(false);

            Inventory.RuntimeWeapon rw = new Inventory.RuntimeWeapon();
            rw.weaponInstance = weaponInstance;
            rw.weaponData = weaponData;

            _slot = rw;
            m_resourcesManager.runtime.RegisterRuntimeWeapons(rw);
        }

        void EquipWeapon(Inventory.RuntimeWeapon _rw, bool isMirrored)
        {
            Vector3 position = Vector3.zero;
            Vector3 eulers = Vector3.zero;
            Vector3 scale = Vector3.one;
            Transform parent = null;

            if (!isMirrored)
            {
                Debug.Log("Equipping Weapon to right hand : " + _rw.weaponInstance.name);
                scale = _rw.weaponData.leftHandPosition.scale;
                parent = m_animator.GetBoneTransform(HumanBodyBones.RightHand);
            }
            else
            {
                Debug.Log("Equipping Weapon to left hand : " + _rw.weaponInstance.name);
                position = _rw.weaponData.leftHandPosition.position;
                eulers = _rw.weaponData.leftHandPosition.eulersAngles;
                parent = m_animator.GetBoneTransform(HumanBodyBones.LeftHand);
            }

            _rw.weaponInstance.transform.parent = parent;     //    local transform data depends on parent to be modified correctly             
            _rw.weaponInstance.transform.localPosition = position;
            _rw.weaponInstance.transform.localEulerAngles = eulers;
            _rw.weaponInstance.transform.localScale = scale;
            _rw.weaponInstance.SetActive(true); //  Activate Weapon
        }
        #endregion

        #region Updates
        public void Fixed_Tick(float _delta)
        {
            m_delta = _delta;
            m_states.onGround = OnGroundCheck();

            switch (m_characterState)
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
                    Vector3 tv = m_input.animationDelta;
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
            m_delta = _delta;
            m_states.onGround = OnGroundCheck();

            switch (m_characterState)
            {
                case CharacterState.MOVING:
                    bool interact = InteractionInputCheck();
                    if (!interact) HandleMovementAnimations();
                    break;
                case CharacterState.INTERACTING:
                    break;
                case CharacterState.ATTACKING:
                    m_states.animIsInteracting = m_animator.GetBool("isInteracting");
                    if (m_states.animIsInteracting == false)
                    {
                        if (m_states.isInteracting)
                        {
                            m_states.isInteracting = false;
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
            WeaponManager.ActionContainer a = null;
            if (m_input.rb)
            {
                a = GetAction(InputType.RB);
                if (a.action.actionObj != null)
                {
                    HandleAction(a);
                    return true;
                }
            }

            if (m_input.lb)
            {
                a = GetAction(InputType.LB);
                if (a.action.actionObj != null)
                {
                    HandleAction(a);
                    return true;
                }
            }

            if (m_input.rt)
            {
                a = GetAction(InputType.RT);
                if (a.action.actionObj != null)
                {
                    HandleAction(a);
                    return true;
                }
            }

            if (m_input.lt)
            {
                a = GetAction(InputType.LT);
                if (a.action.actionObj != null)
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

            if (m_states.isLockedOn)
            {   //  LockOn HandleMovement   TODO : this might be outside of its own scope
                velocity = m_input.moveDir;
            }

            if (m_input.moveAmount > 0)
            { m_rigidbody.drag = 0f; }
            else { m_rigidbody.drag = 4f; }

            //  Free Movement                
            if (!m_states.isRunning)
                velocity *= m_input.moveAmount * m_stats.moveSpeed;
            else
                velocity *= m_input.moveAmount * m_stats.sprintSpeed;

            m_rigidbody.velocity = velocity;
        }

        void HandleRotation()
        {
            Vector3 targetDir = (m_states.isLockedOn == false) ?
             m_input.moveDir : m_input.lockOnTransform.position - m_transform.position;

            targetDir.y = 0f;
            if (targetDir == Vector3.zero)
                targetDir = m_transform.forward;            

            Quaternion tr = Quaternion.LookRotation(targetDir);
            Quaternion targtRotation = Quaternion.Slerp(m_transform.rotation, tr, m_delta * m_stats.rotateSpeed * m_input.moveAmount);
            m_transform.rotation = targtRotation;
        }

        void HandleMovementAnimations()
        {
            m_animator.SetBool("lockon", m_states.isLockedOn);

            if (!m_states.isLockedOn)
            {
                m_animator.SetBool(StaticStrings.run, m_states.isRunning);
                m_animator.SetFloat(StaticStrings.vertical, m_input.moveAmount, 0.15f, m_delta);
            }
            else
            {
                m_animator.SetFloat(StaticStrings.vertical, m_input.vertical, 0.15f, m_delta);
                m_animator.SetFloat(StaticStrings.horizontal, m_input.horizontal, 0.15f, m_delta);
            }
        }

        void HandleAction(WeaponManager.ActionContainer _action)
        {
            switch (_action.action.actionType)
            {
                case ActionType.ATTACK:
                    AttackAction attackAction = (AttackAction)_action.action.actionObj;
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

        void PlayAttackAction(WeaponManager.ActionContainer _action, AttackAction _attackAction)
        {
            //  Is the action a right-handed action or left?
            m_animator.SetBool(StaticStrings.mirror, _action.isMirrored);

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
            m_characterState = _state;
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
                    m_states.isInteracting = true;
                    break;
                case CharacterState.ON_AIR:
                    m_animator.applyRootMotion = false;
                    break;
            }
        }

        WeaponManager.ActionContainer GetAction(InputType _inputType)
        {
            WeaponManager.ActionContainer ac = m_weaponManager.GetAction(_inputType);
            if (ac == null)
                return null;
            return ac;
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
            public bool isMirrored;
        }
    }

    [System.Serializable]
    public class InventoryManager
    {
        //  Player Slots
        public Inventory.RuntimeWeapon rightSlot;
        public Inventory.RuntimeWeapon leftSlot;

        //  Actual items on Player
        public Inventory.Item rightItem;
        public Inventory.Item leftItem;
        public Inventory.Item consumableHandSlot;
        public Inventory.Item spellHandSlot;
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