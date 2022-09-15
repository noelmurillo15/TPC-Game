/*
* InitializeInventory -
* Created by : Allan N. Murillo
* Last Edited : 5/19/2020
*/

using System;
using UnityEngine;
using ANM.Managers;
using ANM.Inventory;
using ANM.Scriptables.Inventory;

namespace ANM.Scriptables.Behaviour.StateActions.Init
{
    [CreateAssetMenu(menuName = "Scriptables/Behaviours/StateAction/Init/Inventory")]
    public class InitializeInventory : StateAction
    {
        public override void Execute(StateManager state)
        {
            if (state.characterProfile == null) return;

            LoadEquippedArmor(state);
            LoadEquippedWeapons(state);
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

        private static void LoadEquippedWeapons(StateManager state)
        {
            var rm = LevelManager.GetResourcesManager();

            var rightHandItem = rm.GetItemInstance(state.characterProfile.rightHandWeaponId);
            if (rightHandItem != null)
            {
                if (rightHandItem as AbstractWeapon != null)
                {
                    var rightWeapon = (AbstractWeapon) rightHandItem;
                    rightWeapon.Init();
                    ParentWeaponUnderBone(state, rightWeapon, HumanBodyBones.RightHand);
                    state.inventory.rightHandItem = rightWeapon;
                }
                else if (rightHandItem as AbstractEquippableItem != null)
                {
                    var rightEquipItem = (AbstractEquippableItem) rightHandItem;
                    rightEquipItem.Init();
                    state.inventory.rightHandItem = rightEquipItem;
                }
            }

            var leftHandItem = rm.GetItemInstance(state.characterProfile.leftHandWeaponId);
            if (leftHandItem != null)
            {
                if (leftHandItem as AbstractWeapon != null)
                {
                    var leftWeapon = ((AbstractWeapon) leftHandItem);
                    leftWeapon.Init();
                    ParentWeaponUnderBone(state, leftWeapon, HumanBodyBones.LeftHand, true);
                    state.inventory.leftHandItem = leftWeapon;
                }
                else if (leftHandItem as AbstractEquippableItem != null)
                {
                    var leftEquipItem = (AbstractEquippableItem) leftHandItem;
                    leftEquipItem.Init();
                    state.inventory.leftHandItem = leftEquipItem;
                }
            }
        }

        private static void LoadEquippedArmor(StateManager state)
        {
            InitArmor(state);
            foreach (var itemToEquip in state.characterProfile.equippedArmor)
            {
                WearItem(itemToEquip, state);
            }
        }

        private static void InitArmor(StateManager state)
        {
            for (var i = 0; i < 4; i++)
            {
                var armorRenderer = GetArmorPart((ArmorType) i, state.body);
                var bodyRenderer = GetBodyPart((ArmorType) i, state.body);
                armorRenderer.enabled = false;
                bodyRenderer.enabled = true;
            }
        }

        private static void WearItem(Item item, StateManager state)
        {
            if (item.type != ItemType.ARMOR) return;

            var armor = (Armor) item;
            var meshRenderer = GetArmorPart(armor.armorType, state.body);
            var bodyPartRenderer = GetBodyPart(armor.armorType, state.body);

            meshRenderer.materials = armor.materials;
            meshRenderer.sharedMesh = armor.armorMesh;
            meshRenderer.enabled = armor.baseBodyEnabled;
            bodyPartRenderer.enabled = armor.baseBodyEnabled;
        }

        private static void TakeOffArmor(ArmorType type, StateManager state)
        {
            var armorPart = GetArmorPart(type, state.body);
            var bodyPart = GetBodyPart(type, state.body);
            armorPart.enabled = false;
            bodyPart.enabled = true;
        }

        private static SkinnedMeshRenderer GetBodyPart(ArmorType type, CharacterBody body)
        {
            switch (type)
            {
                case ArmorType.CHEST:
                    return body.torsoRenderer;
                case ArmorType.LEGS:
                    return body.legsRenderer;
                case ArmorType.ARMS:
                    return body.handsRenderer;
                case ArmorType.HEAD:
                    return body.headRenderer;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        private static SkinnedMeshRenderer GetArmorPart(ArmorType type, CharacterBody body)
        {
            switch (type)
            {
                case ArmorType.CHEST:
                    return body.armorChestRenderer;
                case ArmorType.LEGS:
                    return body.armorLegsRenderer;
                case ArmorType.ARMS:
                    return body.armorArmsRenderer;
                case ArmorType.HEAD:
                    return body.helmetRenderer;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }

    [Serializable]
    public class BodyPart
    {
        public ArmorType armorType;
        public SkinnedMeshRenderer meshRenderer;
    }

    [Serializable]
    public class CharacterBody
    {
        public SkinnedMeshRenderer handsRenderer;
        public SkinnedMeshRenderer legsRenderer;
        public SkinnedMeshRenderer torsoRenderer;
        public SkinnedMeshRenderer headRenderer;

        public SkinnedMeshRenderer armorArmsRenderer;
        public SkinnedMeshRenderer armorLegsRenderer;
        public SkinnedMeshRenderer armorChestRenderer;
        public SkinnedMeshRenderer helmetRenderer;
    }
}
