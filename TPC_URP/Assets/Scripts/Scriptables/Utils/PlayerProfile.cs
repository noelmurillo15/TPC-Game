/*
 * PlayerProfile - Holds info about which items are equipped on a character
 * Created by : Allan N. Murillo
 * Last Edited : 3/14/2020
 */

using UnityEngine;
using System.Collections.Generic;

namespace ANM.Scriptables.Utils
{
    [CreateAssetMenu(menuName = "Character/Inventory Profile")]
    public class PlayerProfile : ScriptableObject
    {
        public string profileName;

        public string rightHandWeaponId;
        public string leftHandWeaponId;

        public List<string> allItemsEquipped;
    }
}