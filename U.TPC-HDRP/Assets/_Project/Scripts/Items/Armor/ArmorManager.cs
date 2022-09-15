/*
* ArmorManager -
* Created by : Allan N. Murillo
* Last Edited : 8/19/2020
*/

using UnityEngine;
using System.Collections.Generic;

namespace ANM.TPC.Items
{
    public class ArmorManager : MonoBehaviour
    {
        Dictionary<ArmorItemType, ArmorItemHook> equippedArmorHooks = new Dictionary<ArmorItemType, ArmorItemHook>();


        public void Initialize()
        {
            var armorHooks = GetComponentsInChildren<ArmorItemHook>();
            foreach (var hook in armorHooks)
            {
                hook.Initialize();
            }
        }

        public void RegisterArmorHook(ArmorItemHook armorHook)
        {
            if (!equippedArmorHooks.ContainsKey(armorHook.armorItemType))
            {
                equippedArmorHooks.Add(armorHook.armorItemType, armorHook);
            }
        }

        private ArmorItemHook GetArmorHook(ArmorItemType type)
        {
            equippedArmorHooks.TryGetValue(type, out var retVal);
            return retVal;
        }

        public void LoadArmor(ArmorItem armorPiece)
        {
            if (armorPiece == null) return;
            var armorHook = GetArmorHook(armorPiece.armorType);
            armorHook.AttachArmor(armorPiece);
        }

        public void LoadListOfArmor(List<ArmorItem> list)
        {
            UnloadAllArmor();
            if (list == null || list.Count <= 0) return;
            foreach (var armorPiece in list)
            {
                LoadArmor(armorPiece);
            }
        }

        public void UnloadAllArmor()
        {
            foreach (var armorHook in equippedArmorHooks.Values)
            {
                armorHook.DetachArmor();
            }
        }
    }
}
