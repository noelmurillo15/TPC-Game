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
    [CreateAssetMenu(menuName = "Behaviours/StateAction/Init/Inventory")]
    public class InitializeInventory : StateAction
    {

        public override void Execute(StateManager state)
        {
            if (state.playerProfile == null) return;

            var rm = LevelManager.GetResourcesManager();

            var rightHandItem = rm.GetItemInstance(state.playerProfile.rightHandWeaponId);
            if (rightHandItem != null)
            {
                if (rightHandItem as AbstractWeapon != null)
                {
                    var rightWeapon = (AbstractWeapon) rightHandItem;
                    rightWeapon.Init();
                    ParentWeaponUnderBone(state, rightWeapon, HumanBodyBones.RightHand);
                    state.inventory.rightHandWeapon = rightWeapon;
                }
                else if (rightHandItem as AbstractEquippableItem != null)
                {
                    var rightEquipItem = (AbstractEquippableItem) rightHandItem;
                    rightEquipItem.Init();
                    state.inventory.rightHandWeapon = rightEquipItem;
                }
            }

            var leftHandItem = rm.GetItemInstance(state.playerProfile.leftHandWeaponId);
            if (leftHandItem != null)
            {
                if (leftHandItem as AbstractWeapon != null)
                {
                    var leftWeapon = ((AbstractWeapon) leftHandItem);
                    leftWeapon.Init();
                    ParentWeaponUnderBone(state, leftWeapon, HumanBodyBones.LeftHand, true);
                    state.inventory.leftHandWeapon = leftWeapon;
                }
                else if (leftHandItem as AbstractEquippableItem != null)
                {
                    var leftEquipItem = (AbstractEquippableItem) leftHandItem;
                    leftEquipItem.Init();
                    state.inventory.leftHandWeapon = leftEquipItem;
                }
            }
        }

        private static void ParentWeaponUnderBone(StateManager state, AbstractWeapon weapon, HumanBodyBones targetBone,
            bool isLeft = false)
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
