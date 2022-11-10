/*
* PlayerStateManager -
* Created by : Allan N. Murillo
* Last Edited : 8/18/2020
*/

using Cinemachine;
using UnityEngine;
using ANM.TPC.Items;
using ANM.TPC.Behaviour;
using ANM.TPC.StateActions;
using System.Collections.Generic;

namespace ANM.TPC.StateManagers
{
    public class PlayerStateManager : CharacterStateManager
    {
        #region Class Members

        [Header("Inputs")] public float mouseX;
        public float mouseY;
        public float moveAmount;
        public Vector3 rotateDirection;

        [Header("References")] public new Transform camera;
        public CinemachineFreeLook normalCam;
        public CinemachineFreeLook lockOnCam;

        [Header("Movement")] public float adaptSpeed = 1f;
        public float movementSpeed = 1f;
        public float rotationSpeed = 10f;
        public float frontRayOffset = 0.5f;

        [HideInInspector] public LayerMask ignoreLayers;
        [HideInInspector] public LayerMask ignoreForGroundCheck;
        private const string LocomotionId = "locomotion";
        public const string AttackId = "attack";

        #endregion

        public override void Initialize()
        {
            base.Initialize();

            //    TODO : find cameras instead of depending on inspector

            var locomotion = new State(new List<StateAction> {new MovePlayerCharacter(this)},
                    new List<StateAction> {new InputHandler(this)})
                {onEnter = DisableRootMotion};

            var attackState = new State(new List<StateAction>(), new List<StateAction>
                    {new MonitorInteractingAnimation(this, "isInteracting", LocomotionId)})
                {onEnter = EnableRootMotion};

            RegisterState(LocomotionId, locomotion);
            RegisterState(AttackId, attackState);
            ChangeState(LocomotionId);

            ignoreLayers = ~(1 << 9);
            ignoreForGroundCheck = ~(1 << 9 | 1 << 10);

            myArmorManager.Initialize();
            myWeaponManager.Initialize();
            LoadListOfArmor(startingArmor);
            myWeaponManager.LoadWeaponOnHook(rightWeapon);
            myWeaponManager.LoadWeaponOnHook(leftWeapon, true);
        }

        #region Unity Funcs

        private void FixedUpdate()
        {
            delta = Time.fixedDeltaTime;
            FixedTick();
        }

        private void Update()
        {
            delta = Time.deltaTime;
            Tick();
        }

        private void LateUpdate() => LateTick();

        #endregion

        #region Inventory

        private void LoadListOfArmor(List<ArmorItem> targetArmorList)
        {
            myArmorManager.LoadListOfArmor(targetArmorList);
        }

        #endregion

        #region LockOn

        public override void OnAssignLookOverride(Transform target)
        {
            //Debug.Log("[PSM]: OnAssignLookOverride = " + target.name);
            base.OnAssignLookOverride(target);
            if (!isLockedOn) return;
            normalCam.gameObject.SetActive(false);
            lockOnCam.gameObject.SetActive(true);
            lockOnCam.m_LookAt = target;
        }

        public override void OnClearLookOverride()
        {
            //Debug.Log("[PSM]: OnClearLookOverride");
            base.OnClearLookOverride();
            normalCam.gameObject.SetActive(true);
            lockOnCam.gameObject.SetActive(false);
        }

        #endregion

        #region State Events

        private void DisableRootMotion() => useRootMotion = false;
        private void EnableRootMotion() => useRootMotion = true;

        #endregion
    }
}
