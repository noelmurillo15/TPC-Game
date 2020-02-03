using UnityEngine;
using System.Collections.Generic;

namespace SA.Inventory
{
    [CreateAssetMenu(menuName = "Single Instances/Runtime References")]
    public class RuntimeReferences : ScriptableObject
    {
        public List<RuntimeWeapon> runtimeWeapons = new List<RuntimeWeapon>();

        public void Initialize()
        {
            runtimeWeapons.Clear();
        }

        public void RegisterRuntimeWeapons(RuntimeWeapon _runtimeWeapon)
        {
            runtimeWeapons.Add(_runtimeWeapon);
        }

        public void UnregisterRuntimeWeapons(RuntimeWeapon _runtimeWeapon)
        {
            if (!runtimeWeapons.Contains(_runtimeWeapon)) return;
            if (_runtimeWeapon.WeaponInstance)
                Destroy(_runtimeWeapon.WeaponInstance);
            runtimeWeapons.Remove(_runtimeWeapon);
        }
    }
}