/*
* CharacterStateManager -
* Created by : Allan N. Murillo
* Last Edited : 8/19/2020
*/

using UnityEngine;
using ANM.TPC.Items;
using ANM.TPC.Utilities;
using ANM.TPC.Items.Weapons;
using System.Collections.Generic;

namespace ANM.TPC.StateManagers
{
    public abstract class CharacterStateManager : StateManager
    {
        [Header("References")]
        public Animator myAnimator;
        public Rigidbody myRigidbody;
        public AnimatorHook myAnimHook;
        public ArmorManager myArmorManager;
        public WeaponHolderManager myWeaponManager;
        public Transform myTarget;

        [Header("Items")]
        public WeaponItem leftWeapon;
        public WeaponItem rightWeapon;
        public List<ArmorItem> startingArmor;

        [Header("Controller Values")]
        public float vertical;
        public float horizontal;

        [Header("Animation Values")]
        public bool useRootMotion;
        public Vector3 rootMovement;

        [Header("States")]
        public bool isGrounded;
        public bool isLockedOn;

        [Header("Misc")]
        public float delta;

        private static readonly int IsInteracting = Animator.StringToHash("isInteracting");


        public override void Initialize()
        {
            myTransform = transform;
            myRigidbody = GetComponent<Rigidbody>();
            myAnimator = GetComponentInChildren<Animator>();
            myAnimHook = GetComponentInChildren<AnimatorHook>();
            myArmorManager = GetComponentInChildren<ArmorManager>();
            myWeaponManager = GetComponentInChildren<WeaponHolderManager>();
            myAnimator.applyRootMotion = false;
            myAnimHook.Initialize(this);
        }

        public void PlayTargetAnimation(string targetAnim, bool isInteracting)
        {
            myAnimator.SetBool(IsInteracting, isInteracting);
            myAnimator.CrossFade(targetAnim, 0.2f);
        }

        public virtual void OnAssignLookOverride(Transform target)
        {
            myTarget = target;
            isLockedOn = target != null;
        }

        public virtual void OnClearLookOverride()
        {
            isLockedOn = false;
        }
    }
}
