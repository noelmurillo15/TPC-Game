using UnityEngine;
using System.Collections.Generic;


namespace SA
{
    [CreateAssetMenu(menuName = "Single Instances/Runtime References")]
    public class RuntimeReferences : ScriptableObject
    {
        public List<Inventory.RuntimeWeapon> runtimeWeapons = new List<Inventory.RuntimeWeapon>();

        public void Initialize()
        {
            runtimeWeapons.Clear();
        }

        public void RegisterRuntimeWeapons(Inventory.RuntimeWeapon _runtimeWeapon)
        {
            runtimeWeapons.Add(_runtimeWeapon);
        }

        public void UnregisterRuntimeWeapons(Inventory.RuntimeWeapon _runtimeWeapon)
        {
            if (runtimeWeapons.Contains(_runtimeWeapon))
            {
                if (_runtimeWeapon.weaponInstance)
                    Destroy(_runtimeWeapon.weaponInstance);
                runtimeWeapons.Remove(_runtimeWeapon);
            }
        }
    }
}