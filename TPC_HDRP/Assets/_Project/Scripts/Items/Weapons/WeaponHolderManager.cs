/*
* WeaponHolderManager -
* Created by : Allan N. Murillo
* Last Edited : 8/19/2020
*/

using UnityEngine;

namespace ANM.TPC.Items.Weapons
{
    public class WeaponHolderManager : MonoBehaviour
    {
        public WeaponHolderHook leftHook;
        public WeaponHolderHook rightHook;


        public void Initialize()
        {
            var weaponHolderHooks = GetComponentsInChildren<WeaponHolderHook>();
            foreach (var hook in weaponHolderHooks)
            {
                if (hook.isLeftHook) leftHook = hook;
                else rightHook = hook;
            }
        }

        public void LoadWeaponOnHook(WeaponItem weapon, bool isLeft = false)
        {
            if (isLeft) leftHook.LoadWeaponModel(weapon);
            else rightHook.LoadWeaponModel(weapon);
        }
    }
}
