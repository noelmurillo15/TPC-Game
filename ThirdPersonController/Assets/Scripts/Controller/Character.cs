/*
 * Character - Used for equipping items on a Character
 * Created by : Allan N. Murillo
 * Last Edited : 3/12/2020
 */

using System;
using UnityEngine;
using ANM.Inventory;

namespace ANM.Controller
{
    public class Character : MonoBehaviour
    {
        public CharacterBody body;
        public InventoryData inventoryData;

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

        private void Start()
        {
            LoadItemsFromData();
        }

        private void InitArmor()
        {
            for (var i = 0; i < 4; i++)
            {
                var armorRenderer = GetArmorPart((ArmorType) i);
                var bodyRenderer = GetBodyPart((ArmorType) i);
                armorRenderer.enabled = false;
                bodyRenderer.enabled = true;
            }
        }

        private void LoadItemsFromData()
        {
            InitArmor();
            if (inventoryData == null) return;
            foreach (var itemToEquip in inventoryData.data)
            {
                WearItem(itemToEquip);
            }
        }

        private void WearItem(Item item)
        {
            if (item.type != ItemType.ARMOR) return;

            var armor = (Armor) item;
            var meshRenderer = GetArmorPart(armor.armorType);
            var bodyPartRenderer = GetBodyPart(armor.armorType);

            meshRenderer.materials = armor.materials;
            meshRenderer.sharedMesh = armor.armorMesh;
            meshRenderer.enabled = armor.baseBodyEnabled;
            bodyPartRenderer.enabled = armor.baseBodyEnabled;
        }

        public void TakeOffItem(ArmorType type)
        {
            var armorPart = GetArmorPart(type);
            var bodyPart = GetBodyPart(type);
            armorPart.enabled = false;
            bodyPart.enabled = true;
        }

        private SkinnedMeshRenderer GetBodyPart(ArmorType type)
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

        private SkinnedMeshRenderer GetArmorPart(ArmorType type)
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
}
