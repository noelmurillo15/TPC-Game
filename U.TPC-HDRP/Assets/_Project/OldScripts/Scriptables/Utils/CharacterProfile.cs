/*
 * CharacterProfile - Holds info about which items are equipped on a character
 * Created by : Allan N. Murillo
 * Last Edited : 5/19/2020
 */

using UnityEngine;
using ANM.Scriptables.Inventory;
using System.Collections.Generic;

namespace ANM.Scriptables.Utils
{
    [CreateAssetMenu(menuName = "Scriptables/Utility/Character Profile")]
    public class CharacterProfile : ScriptableObject
    {
        public string profileName;

        public string rightHandWeaponId;
        public string leftHandWeaponId;

        public List<Item> equippedArmor = new List<Item>();
    }
}
