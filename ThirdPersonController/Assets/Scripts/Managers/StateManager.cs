/*
 * StateManager - 
 * Created by : Allan N. Murillo
 * Last Edited : 2/24/2020
 */

using System;
using UnityEngine;
using System.Linq;
using ANM.Input;
using SA.Utilities;
using SA.Scriptable;
using Action = SA.Scriptable.Action;
using Object = UnityEngine.Object;

namespace SA.Managers
{
    public class StateManager : MonoBehaviour
    {
        public enum CharacterState
        {  MOVING, ON_AIR, INTERACTING, OVERRIDE_INTERACTING, ROLL  }

        public CharacterState characterState;       //  Current State 
        public ControllerStats controlStats;        //  Movement Info
            
        public States states;                       //  State Info
        public InputVariables inputVar;             //  Input Info
        public WeaponManager weaponManager;         //  Weapon Info
        public InventoryManager inventoryManager;   // Inventory Info
        
        [HideInInspector] public ResourcesManager resourcesManager;

        public GameObject activeModel;
        public float deltaTime;
        private float _timeSinceLastHit;

        //  Spell Action
        private float _savedTime;
        private SpellAction _currentSpellAction;

        //  If the game object is not the player - force init
        public bool forceInit = false;

        [HideInInspector] public Transform myTransform;
        [HideInInspector] public Animator myAnimator;
        [HideInInspector] public Rigidbody myRigidbody;
        [HideInInspector] public Collider myCollider;
        [HideInInspector] public AnimatorHook animatorHook;

        [HideInInspector] public LayerMask ignoreLayers;
        [HideInInspector] public LayerMask ignoreForGroundCheck;
        
        private static readonly int _IsInteracting = Animator.StringToHash("isInteracting");
        private static readonly int _Lockon = Animator.StringToHash("lockon");
        private static readonly int _Speed = Animator.StringToHash("speed");
        
        
        private void Start()
        {   //  TODO : remove this for enemy
            if (forceInit)
            {
                Initialize();
            }
        }

        public void Initialize()
        {
            if (resourcesManager == null && !forceInit)
            {
                resourcesManager = Resources.Load("ResourceManager") as ResourcesManager;
            }

            SetupAnimator();
            SetupRigidBody();

            myTransform = transform;
            myCollider = GetComponent<Collider>();

            //  Layer masks
            gameObject.layer = 8;
            ignoreLayers = ~(1 << 9);
            ignoreForGroundCheck = ~(1 << 9 | 1 << 10);

            SetupInventoryManager();
            SetupWeaponManager();
        }

        private void SetupAnimator()
        {   //  This will fail if no animator is attached
            if (activeModel == null)
            {   //  If designer forgets to attach the active Model ~ this will find the model via Animator
                myAnimator = GetComponentInChildren<Animator>();
                activeModel = myAnimator.gameObject;
            }

            if (myAnimator == null) //  If animator has not been found, find via ActiveModel
                myAnimator = activeModel.GetComponent<Animator>();
            else
            {   //  If animator was Ultimately found
                myAnimator.applyRootMotion = false;
                myAnimator.GetBoneTransform(HumanBodyBones.LeftHand).localScale = Vector3.one;
                myAnimator.GetBoneTransform(HumanBodyBones.RightHand).localScale = Vector3.one;

                animatorHook = activeModel.AddComponent<AnimatorHook>();
                animatorHook.Init(this, forceInit);
            }
        }

        private void SetupRigidBody()
        {
            myRigidbody = GetComponent<Rigidbody>();
            myRigidbody.angularDrag = 999;
            myRigidbody.drag = 4;
            myRigidbody.constraints = RigidbodyConstraints.FreezeRotationX | 
                                      RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        }

        private void SetupInventoryManager()
        {
            if (inventoryManager.rightItem != null)
            {
                WeaponToRuntime(inventoryManager.rightItem.obj, ref inventoryManager.rightSlot);
                EquipWeapon(inventoryManager.rightSlot, false);
            }

            if (inventoryManager.leftItem == null) return;
            WeaponToRuntime(inventoryManager.leftItem.obj, ref inventoryManager.leftSlot);
            EquipWeapon(inventoryManager.leftSlot, true);
        }

        private void SetupWeaponManager()
        {
            if (inventoryManager.leftSlot == null && inventoryManager.rightSlot == null)
                return;

            if (inventoryManager.rightSlot != null)
            {   //  Right hand Take Priority ~?
                var rb = weaponManager.GetAction(InputType.RB);
                rb.action = inventoryManager.rightSlot.weaponData.GetAction(InputType.RB);

                var rt = weaponManager.GetAction(InputType.RT);
                rt.action = inventoryManager.rightSlot.weaponData.GetAction(InputType.RT);

                if (inventoryManager.leftSlot == null)
                {   //  If not Dual Wielding, Get Left Trigger & Bumper Actions from Right Hand
                    var lb = weaponManager.GetAction(InputType.LB);
                    lb.action = inventoryManager.rightSlot.weaponData.GetAction(InputType.LB);

                    var lt = weaponManager.GetAction(InputType.LT);
                    lt.action = inventoryManager.rightSlot.weaponData.GetAction(InputType.LT);
                }
                else
                {   //  If Dual Wielding, Get leftHand Actions
                    var lb = weaponManager.GetAction(InputType.LB);
                    lb.action = inventoryManager.leftSlot.weaponData.GetAction(InputType.LB);

                    var lt = weaponManager.GetAction(InputType.LT);
                    lt.action = inventoryManager.leftSlot.weaponData.GetAction(InputType.LT);
                }
                return;
            }

            if (inventoryManager.leftSlot == null) return;
            {
                //  There is no right hand equipped so grab all actions from Left Hand
                var rb = weaponManager.GetAction(InputType.RB);
                rb.action = inventoryManager.leftSlot.weaponData.GetAction(InputType.RB);

                var rt = weaponManager.GetAction(InputType.RT);
                rt.action = inventoryManager.leftSlot.weaponData.GetAction(InputType.RT);

                var lb = weaponManager.GetAction(InputType.LB);
                lb.action = inventoryManager.leftSlot.weaponData.GetAction(InputType.LB);

                var lt = weaponManager.GetAction(InputType.LT);
                lt.action = inventoryManager.leftSlot.weaponData.GetAction(InputType.LT);
            }
        }

        private void WeaponToRuntime(Object obj, ref Inventory.RuntimeWeapon slot)
        {
            var weaponData = (Inventory.Weapon)obj;
            var weaponInstance = Instantiate(weaponData.modelPrefab);
            var wHook = weaponInstance.AddComponent<WeaponHook>();
            wHook.Initialize(this);
            weaponInstance.SetActive(false);

            var rw = new Inventory.RuntimeWeapon
            {
                weaponInstance = weaponInstance, weaponData = weaponData, weaponHook = wHook
            };

            slot = rw;
            resourcesManager.runtime.RegisterRuntimeWeapons(rw);
        }

        private void EquipWeapon(Inventory.RuntimeWeapon rw, bool isMirrored)
        {
            var position = Vector3.zero;
            var eulers = Vector3.zero;
            var scale = Vector3.one;
            Transform parent = null;

            if (!isMirrored)
            {
                Debug.Log("Equipping Weapon to right hand : " + rw.weaponInstance.name + " of the " + gameObject.name);
                scale = rw.weaponData.leftHandPosition.scale;
                parent = myAnimator.GetBoneTransform(HumanBodyBones.RightHand);
            }
            else
            {
                Debug.Log("Equipping Weapon to left hand : " + rw.weaponInstance.name + " of the " + gameObject.name);
                position = rw.weaponData.leftHandPosition.position;
                eulers = rw.weaponData.leftHandPosition.eulersAngles;
                parent = myAnimator.GetBoneTransform(HumanBodyBones.LeftHand);
            }

            rw.weaponInstance.transform.parent = parent;     //    local transform data depends on parent to be modified correctly             
            rw.weaponInstance.transform.localPosition = position;
            rw.weaponInstance.transform.localEulerAngles = eulers;
            rw.weaponInstance.transform.localScale = scale;
            rw.weaponInstance.SetActive(true); //  Activate Weapon
        }
        
        private void Update()
        {
            if (forceInit)
            {   //  TODO : enemies wont properly animate without this here
                Tick(Time.deltaTime);
            }
        }

        public void Tick(float d)
        {
            deltaTime = d;
            states.onGround = OnGroundCheck();

            if (states.isGettingHit)
            {
                if (Time.realtimeSinceStartup - _timeSinceLastHit < 0.2f)
                {
                    states.isGettingHit = false;
                }
            }

            switch (characterState)
            {
                case CharacterState.MOVING:
                    var interact = InteractionInputCheck();
                    if (!interact) HandleMovementAnimations();
                    break;
                case CharacterState.INTERACTING:
                    if (states.isSpellCasting)
                    {
                        if (Time.realtimeSinceStartup - _savedTime > 1)
                        {
                            states.isSpellCasting = false;
                            PlaySavedSpellAction();
                        }
                    }
                    HandleMovementAnimations();
                    break;
                case CharacterState.OVERRIDE_INTERACTING:
                    states.animIsInteracting = myAnimator.GetBool(_IsInteracting);
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
                case CharacterState.ROLL:
                    states.animIsInteracting = myAnimator.GetBool(_IsInteracting);
                    if (states.animIsInteracting == false)
                    {
                        if (states.isInteracting)
                        {
                            states.isInteracting = false;
                            ChangeState(CharacterState.MOVING);
                        }
                    }
                    break;
            }
        }

        public void Fixed_Tick(float d)
        {
            deltaTime = d;
            states.onGround = OnGroundCheck();

            switch (characterState)
            {
                case CharacterState.MOVING:
                    HandleRotation();
                    HandleMovement();
                    break;
                case CharacterState.INTERACTING:
                    HandleRotation();
                    HandleMovement();
                    break;
                case CharacterState.OVERRIDE_INTERACTING:
                    myRigidbody.drag = 0f;
                    Vector3 v = myRigidbody.velocity;
                    Vector3 tv = inputVar.animationDelta;
                    tv *= 50f;
                    tv.y = v.y;
                    myRigidbody.velocity = tv;
                    break;
                case CharacterState.ON_AIR:
                    break;
            }
        }

        private bool OnGroundCheck()
        {    //    If character falls through the ground while lockon, check Animator->LocomotionLockedOn->BlendState
            Vector3 origin = myTransform.position;
            origin.y += 0.7f;
            Vector3 dir = -Vector3.up;

            float distance = 1.4f;
            if (!Physics.Raycast(origin, dir, out var hit, distance, ignoreForGroundCheck)) return false;

            var targetPosition = hit.point;
            myTransform.position = targetPosition;
            return true;
        }

        private bool InteractionInputCheck()
        {   //  Only happens during movement state
            WeaponManager.ActionContainer a = null;
            if (inputVar.rb) { a = GetAction(InputType.RB); }
            else if (inputVar.lb) { a = GetAction(InputType.LB); }
            else if (inputVar.rt) { a = GetAction(InputType.RT); }
            else if (inputVar.lt) { a = GetAction(InputType.LT); }

            if (a?.action == null) { return false; }
            if (a.action.animationAction == null) return false;
            
            HandleAction(a);
            return true;
        }

        private void HandleMovement()
        {
            var v = myTransform.forward;
            
            if (states.isLockedOn)  
                v = inputVar.moveDir; 

            myRigidbody.drag = inputVar.moveAmount > 0 ? 0f : 4f;

            if (states.animIsInteracting)
            {
                states.isRunning = false;
                v *= inputVar.moveAmount * controlStats.walkSpeed;
            }
            else
            {
                if (!states.isRunning)
                    v *= inputVar.moveAmount * controlStats.moveSpeed;
                else
                    v *= inputVar.moveAmount * controlStats.sprintSpeed;
            }
            myRigidbody.velocity = v;
        }

        private void HandleRotation()
        {
            var targetDir = (states.isLockedOn == false) ?
             inputVar.moveDir :  inputVar.lockOnTransform.position - myTransform.position;

            targetDir.y = 0f;
            if (targetDir == Vector3.zero)
                targetDir = myTransform.forward;

            var tr = Quaternion.LookRotation(targetDir);
            var targetRotation = Quaternion.Slerp(
                myTransform.rotation, tr, deltaTime * controlStats.rotateSpeed * inputVar.moveAmount);
            myTransform.rotation = targetRotation;
        }

        private void HandleMovementAnimations()
        {
            myAnimator.SetBool(_Lockon, states.isLockedOn);

            if (!states.isLockedOn)
            {
                myAnimator.SetBool(StaticStrings.run, states.isRunning);
                float move = inputVar.moveAmount;
                if (states.animIsInteracting) { move = Mathf.Clamp(move, 0, 0.5f); }
                myAnimator.SetFloat(StaticStrings.vertical, move, 0.15f, deltaTime);
            }
            else
            {
                myAnimator.SetFloat(StaticStrings.vertical, inputVar.vertical, 0.15f, deltaTime);
                myAnimator.SetFloat(StaticStrings.horizontal, inputVar.horizontal, 0.15f, deltaTime);
            }
        }

        private void HandleAction(WeaponManager.ActionContainer actionContainer)
        {
            switch (actionContainer.action.actionType)
            {
                case ActionType.ATTACK:
                    var attackAction = (AttackAction)actionContainer.action.animationAction;
                    PlayAttackAction(actionContainer, attackAction);
                    break;
                case ActionType.BLOCK:
                    break;
                case ActionType.PARRY:
                    break;
                case ActionType.SPELL:
                    var spellAction = (SpellAction)actionContainer.action.animationAction;
                    PlaySpellAction(actionContainer, spellAction);
                    break;
            }
        }

        private void PlayAttackAction(WeaponManager.ActionContainer actionContainer, AttackAction attackAction)
        {
            //  Is the action a right-handed action or left?
            myAnimator.SetBool(StaticStrings.mirror, actionContainer.isMirrored);

            //  Play the Attack Animation
            PlayActionAnimation(attackAction.attackAnimation.value);

            //  Change the anim speed if necessary            
            if (attackAction.changeSpeed)
            { myAnimator.SetFloat(_Speed, attackAction.animSpeed); }

            //  Switch State
            ChangeState(CharacterState.OVERRIDE_INTERACTING);
        }

        private void PlaySpellAction(WeaponManager.ActionContainer actionContainer, SpellAction spellAction)
        {
            string targetAnimation = spellAction.start_animation.value;
            targetAnimation += (actionContainer.isMirrored) ? "_l" : "_r";

            //  Is the action a right-handed action or left?
            myAnimator.SetBool(StaticStrings.mirror, actionContainer.isMirrored);

            //  Play the Attack Animation
            myAnimator.CrossFade(targetAnimation, 0.2f);

            //  Is it a spell?
            states.isSpellCasting = true;
            states.animIsInteracting = true;
            myAnimator.SetBool(StaticStrings.spellCasting, states.isSpellCasting);

            //  Change the anim speed if necessary            
            if (spellAction.changeSpeed)
            { myAnimator.SetFloat(_Speed, spellAction.animSpeed); }

            //  Switch State
            ChangeState(CharacterState.INTERACTING);
            _savedTime = Time.realtimeSinceStartup;
            _currentSpellAction = spellAction;
        }

        private void PlaySavedSpellAction()
        {
            myAnimator.SetBool(StaticStrings.spellCasting, states.isSpellCasting);
            PlayActionAnimation(_currentSpellAction.cast_animation.value);
            ChangeState(CharacterState.OVERRIDE_INTERACTING);
            states.animIsInteracting = false;
        }

        public void CastSpellActual()
        {
            if (!(_currentSpellAction is ProjectileSpell)) return;
            var projectile = (ProjectileSpell)_currentSpellAction;
            var go = Instantiate(projectile.projectile);

            Vector3 tp = myTransform.position;
            tp += myTransform.forward;
            tp.y += 1.5f;

            go.transform.position = tp;
            go.transform.rotation = transform.rotation;

            var rb = go.GetComponent<Rigidbody>();
            rb.AddForce(myTransform.forward * 10f, ForceMode.Impulse);
        }

        private void PlayActionAnimation(string animationName)
        {   //  The layer parameter is refering to Animator Controller Layer 
            // m_animator.PlayInFixedTime(_animationName, 5, 0.2f);    //  Pass in the Override Layer where attacks take place
            // Debug.Log("Playing Animation : " + _animationName);
            myAnimator.CrossFade(animationName, 0.2f);
        }   //  PlayInFixedTime is kinda similar to CrossFade Animation but slightly better

        private void ChangeState(CharacterState state)
        {
            if (characterState == state) return;
            characterState = state;
            switch (state)
            {
                case CharacterState.MOVING:
                    animatorHook.rm_mult = 1;
                    myAnimator.applyRootMotion = false;
                    break;
                case CharacterState.INTERACTING:
                    animatorHook.rm_mult = 1;
                    myAnimator.applyRootMotion = false;
                    break;
                case CharacterState.OVERRIDE_INTERACTING:
                    animatorHook.rm_mult = 1;
                    myAnimator.applyRootMotion = true;
                    myAnimator.SetBool(_IsInteracting, true);
                    states.isInteracting = true;
                    break;
                case CharacterState.ON_AIR:
                    animatorHook.rm_mult = 1;
                    myAnimator.applyRootMotion = false;
                    break;
                case CharacterState.ROLL:
                    myAnimator.applyRootMotion = true;
                    myAnimator.SetBool(_IsInteracting, true);
                    states.isInteracting = true;
                    break;
            }
        }

        private WeaponManager.ActionContainer GetAction(InputType inputType)
        {
            var ac = weaponManager.GetAction(inputType);

            if (ac == null)
                return null;

            inventoryManager.SetLastInput(inputType);
            return ac;
        }

        public void HandleRoll()
        {
            Vector3 relativeDir = myTransform.InverseTransformDirection(inputVar.moveDir);
            float v = relativeDir.z;
            float h = relativeDir.x;

            if (relativeDir == Vector3.zero)
            {   //  if no directional input, play step back animation
                inputVar.moveDir = -myTransform.forward;
                inputVar.targetRollSpeed = controlStats.backStepSpeed;
            }
            else
            {   //  else roll using directional input
                inputVar.targetRollSpeed = controlStats.rollSpeed;
            }

            //  Override root motion multiplier
            animatorHook.rm_mult = inputVar.targetRollSpeed;

            //  Set Animations floats using relative Direction
            myAnimator.SetFloat(StaticStrings.vertical, v);
            myAnimator.SetFloat(StaticStrings.horizontal, h);

            //  Play Animation and change state
            PlayActionAnimation(StaticStrings.rolls);
            ChangeState(CharacterState.ROLL);
        }

        public void SetDamageColliderStatus(bool status)
        {
            var wHook = inventoryManager.GetWeaponInUse().weaponHook;

            if (wHook != null)
            {
                if (status) { wHook.OpenDamageColliders(); }
                else { wHook.CloseDamageColliders(); }
            }
            else { Debug.LogError("Weapon hook came back null from : " + gameObject.name); }
        }

        public void HandleDamageCollision(StateManager targetStateManager)
        {
            if (targetStateManager == this) return;
            targetStateManager.GetHit();
        }

        private void GetHit()
        {
            if (states.isGettingHit) return;
            PlayActionAnimation("hit1");
            states.isGettingHit = true;
            _timeSinceLastHit = Time.realtimeSinceStartup;
            ChangeState(CharacterState.OVERRIDE_INTERACTING);
        }
    }

    [Serializable]
    public class WeaponManager
    {   //  Control what actions to do based on button input
        public ActionContainer[] actions;


        public ActionContainer GetAction(InputType inputType)
        {
            return actions.FirstOrDefault(t => t.inputType == inputType);
        }

        public void Init()
        {
            actions = new ActionContainer[4];
            for (var x = 0; x < actions.Length; x++)
            {
                var a = new ActionContainer {inputType = (InputType) x};
                actions[x] = a;
            }
        }

        [Serializable]
        public class ActionContainer
        {
            public InputType inputType;
            public Action action;
            public bool isMirrored;
        }
    }

    [Serializable]
    public class InventoryManager
    {
        private InputType _lastInput;

        //  Equipped Weapon Data
        public Inventory.RuntimeWeapon rightSlot;
        public Inventory.RuntimeWeapon leftSlot;

        //  Attached Item Data
        public Inventory.Item rightItem;
        public Inventory.Item leftItem;
        public Inventory.Item consumableHandSlot;
        public Inventory.Item spellHandSlot;


        public void SetLastInput(InputType type) { _lastInput = type; }

        public Inventory.RuntimeWeapon GetWeaponInUse()
        {
            switch (_lastInput)
            {
                case InputType.RB:
                case InputType.RT:
                    return rightSlot;
                case InputType.LB:
                case InputType.LT:
                    return leftSlot;
                default:
                    Debug.LogError("Last input was not recognized!");
                    return null;
            }
        }
    }

    [Serializable]
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

        public float targetRollSpeed;
    }

    [Serializable]
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
        public bool isIkEnabled;
        public bool isUsingItem;
        public bool isAbleToBeParried;
        public bool isParryOn;
        public bool isLeftHand;
        public bool animIsInteracting;
        public bool isInteracting;
        public bool closeWeapons;
        public bool isInvisible;
        public bool isGettingHit;
    }

    [Serializable]
    public class NetworkStates
    {
        public bool isLocal;
        public bool isInRoom;
    }
}