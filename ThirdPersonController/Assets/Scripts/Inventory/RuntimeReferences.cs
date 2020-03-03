/*
 * RuntimeReferences SO - 
 * Created by : Allan N. Murillo
 * Last Edited : 2/24/2020
 */

using UnityEngine;
using System.Collections.Generic;

namespace ANM.Inventory
{
    [CreateAssetMenu(menuName = "Single Instances/Runtime References")]
    public class RuntimeReferences : ScriptableObject
    {
        public List<RuntimeWeapon> runtimeWeapons = new List<RuntimeWeapon>();

        public void Initialize()
        {
            runtimeWeapons.Clear();
        }

        public void RegisterRuntimeWeapons(RuntimeWeapon runtimeWeapon)
        {
            runtimeWeapons.Add(runtimeWeapon);
        }

        public void UnregisterRuntimeWeapons(RuntimeWeapon runtimeWeapon)
        {
            if (!runtimeWeapons.Contains(runtimeWeapon)) return;
            if (runtimeWeapon.weaponInstance)
                Destroy(runtimeWeapon.weaponInstance);
            runtimeWeapons.Remove(runtimeWeapon);
        }
    }
}