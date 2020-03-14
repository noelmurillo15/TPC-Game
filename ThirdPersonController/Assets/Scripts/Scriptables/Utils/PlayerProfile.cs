/*
 * PlayerProfile - 
 * Created by : Allan N. Murillo
 * Last Edited : 3/13/2020
 */

using UnityEngine;
using System.Collections.Generic;

namespace ANM.Scriptables.Utils
{
    [CreateAssetMenu(menuName = "Single Instances/Player Profile")]
    public class PlayerProfile : ScriptableObject
    {
        public string profileName;

        public string rightHandWeaponId;
        public string leftHandWeaponId;

        public List<string> allItemsEquipped;
    }
}