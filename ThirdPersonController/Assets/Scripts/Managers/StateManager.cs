﻿/*
 * StateManager - Handles all states of a Character
 * Created by : Allan N. Murillo
 * Last Edited : 3/13/2020
 */
 
using ANM.Saving;
using UnityEngine;
using ANM.Behaviour;
using ANM.Inventory;
using ANM.Utilities;
using ANM.Scriptables.Utils;
using ANM.Scriptables.Variables;

 namespace ANM.Managers
 {
     public class StateManager : MonoBehaviour
     {
         public State currentState;
         public StateAction initAction;

         [Header("References")] public GameObject activeModel;
         [HideInInspector] public Transform myTransform;
         [HideInInspector] public Animator myAnimator;
         [HideInInspector] public Rigidbody myRigidbody;
         [HideInInspector] public Collider myCollider;
         [HideInInspector] public AnimatorHook animatorHook;

         [HideInInspector] public LayerMask ignoreLayers;
         [HideInInspector] public LayerMask ignoreForGroundCheck;

         [Space] [Header("Inputs")] public float vertical;
         public float horizontal;
         public float moveAmount;
         public Vector3 rotateDirection;
         public Vector3 rollDirection;
         public float generalDelta;

         public bool rb;
         public bool rt;
         public bool lb;
         public bool lt;

         public bool isBackstep;

         [Space] [Header("Inventory")] public PlayerProfile playerProfile;
         public AbstractInventory inventory;
         public Vector3Variable leftHandPosition;
         public Vector3Variable leftHandRotation;

         [Space] [Header("Local Info")] public float deltaTime;

         private SerializableVector3 _lastKnownLocation;

         private static readonly int IsInteracting = Animator.StringToHash("isInteracting");
         private static readonly int Lockon = Animator.StringToHash("lockon");
         private static readonly int Speed = Animator.StringToHash("speed");
         private static readonly int Mirror = Animator.StringToHash("mirror");


         private void Start()
         {
             Initialize();
             initAction?.Execute(this);
         }

         private void Initialize()
         {
             myTransform = transform;
             myCollider = GetComponent<Collider>();
             myRigidbody = GetComponent<Rigidbody>();
             myAnimator = GetComponentInChildren<Animator>();
             activeModel = myAnimator.gameObject;

             gameObject.layer = 8;
             ignoreLayers = ~(1 << 9);
             ignoreForGroundCheck = ~(1 << 9 | 1 << 10);
         }

         private void FixedUpdate()
         {
             //    Fixed Update runs before Update
             deltaTime = Time.deltaTime;
             if (currentState != null)
             {
                 currentState.FixedTick(this);
             }
         }

         private void Update()
         {
             //    Update runs After Fixed Update
             deltaTime = Time.deltaTime;
             if (currentState != null)
             {
                 currentState.Tick(this);
             }
         }

         public void CastSpellActual()
         {
             // if (!(_currentSpellAction is ProjectileSpell)) return;
             // var projectile = (ProjectileSpell) _currentSpellAction;
             // var go = Instantiate(projectile.projectile);
             //
             // Vector3 tp = myTransform.position;
             // var forward = myTransform.forward;
             // tp += forward;
             // tp.y += 1.5f;
             //
             // go.transform.position = tp;
             // go.transform.rotation = transform.rotation;
             //
             // var rb = go.GetComponent<Rigidbody>();
             // rb.AddForce(forward * 10f, ForceMode.Impulse);
         }

         public void PlayAnimation(string targetAnim, bool isMirror = false)
         {
             if (string.IsNullOrEmpty(targetAnim)) return;
             myAnimator.SetBool(IsInteracting, true);
             myAnimator.SetBool(Mirror, isMirror);
             myAnimator.CrossFade(targetAnim, 0.2f);
         }

         public void SetDamageColliderStatus(bool status)
         {
             // var wHook = inventoryManager.GetWeaponInUse().weaponHook;
             //
             // if (wHook != null)
             // {
             //     if (status)
             //     {
             //         //wHook.OpenDamageColliders();
             //     }
             //     else
             //     {
             //         //wHook.CloseDamageColliders();
             //     }
             // }
             // else
             // {
             //     Debug.LogError("Weapon hook came back null from : " + gameObject.name);
             // }
         }

         public void HandleDamageCollision(StateManager targetStateManager)
         {
             if (targetStateManager == this) return;
             targetStateManager.GetHit();
         }

         private void GetHit()
         {

         }

         private void Save()
         {
             Debug.Log("Saving");
             _lastKnownLocation = new SerializableVector3(transform.position);
             SaveGameState.Save(_lastKnownLocation, name + "WorldPosition");
         }

         private void Load()
         {
             if (!SaveGameState.SaveExists(name + "WorldPosition")) return;
             Debug.Log("Loading");
             _lastKnownLocation = SaveGameState.Load<SerializableVector3>(name + "WorldPosition");
             transform.position = _lastKnownLocation.ToVector();
         }

         public enum InputButton
         {
             RB,
             LB,
             RT,
             LT
         }
     }
 }
