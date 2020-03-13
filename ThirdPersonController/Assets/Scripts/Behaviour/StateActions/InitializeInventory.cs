/*
* InitializeInventory - 
* Created by : Allan N. Murillo
* Last Edited : 3/13/2020
*/

using UnityEngine;
using ANM.Managers;
using ANM.Inventory;

namespace ANM.Behaviour.StateActions
{
    [CreateAssetMenu(menuName = "Behaviours/StateAction/Initialize Inventory")]
    public class InitializeInventory : StateAction
    {

        public override void Execute(StateManager state)
        {
            if (state.playerProfile == null) return;

            var rm = LevelManager.GetResourcesManager();

            var item = rm.GetItemInstance(state.playerProfile.rightHandWeaponId);
            if (item != null)
            {
                var rightHandWeapon = (AbstractWeapon) item;
                rightHandWeapon.Init();
                ParentUnderBone(state, rightHandWeapon, HumanBodyBones.RightHand);
                state.inventory.rightHandWeapon = rightHandWeapon;
            }

            item = rm.GetItemInstance(state.playerProfile.leftHandWeaponId);
            if (item != null)
            {
                var leftHandWeapon = (AbstractWeapon) item;
                leftHandWeapon.Init();
                ParentUnderBone(state, leftHandWeapon, HumanBodyBones.LeftHand, true);
                state.inventory.leftHandWeapon = leftHandWeapon;
            }
        }

        private static void ParentUnderBone(StateManager state, AbstractWeapon weapon, HumanBodyBones targetBone, bool isLeft = false)
        {
            var bone = state.myAnimator.GetBoneTransform(targetBone);
            weapon.runtime.ModelInstance.transform.parent = bone;

            if (isLeft)
            {
                weapon.runtime.ModelInstance.transform.localPosition = state.leftHandPosition.value;
                weapon.runtime.ModelInstance.transform.localEulerAngles = state.leftHandRotation.value;
            }
            else
            {
                weapon.runtime.ModelInstance.transform.localPosition = Vector3.zero;
                weapon.runtime.ModelInstance.transform.localEulerAngles = Vector3.zero;
            }
            
            weapon.runtime.ModelInstance.transform.localScale = Vector3.one;
            weapon.runtime.ModelInstance.SetActive(true);
        }
    }
}
