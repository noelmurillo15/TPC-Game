/*
* WeaponHolderHook -
* Created by : Allan N. Murillo
* Last Edited : 8/19/2020
*/

using UnityEngine;

namespace ANM.TPC.Items.Weapons
{
    public class WeaponHolderHook : MonoBehaviour
    {
        public Transform parentOverride;
        private GameObject _currentModel;
        public bool isLeftHook;


        public void LoadWeaponModel(WeaponItem weaponItem)
        {
            if (weaponItem == null)
            {
                UnloadWeapon();
                return;
            }

            var model = Instantiate(weaponItem.modelPrefab);

            if (model != null)
            {
                model.transform.parent = parentOverride != null ? parentOverride : transform;
                model.transform.localScale = Vector3.one;
                model.transform.localPosition = Vector3.zero;
                model.transform.localRotation = Quaternion.identity;
            }

            _currentModel = model;
        }

        public void UnloadWeapon()
        {
            if (_currentModel != null)
            {

            }
        }

        public void UnloadWeaponAndDestroy()
        {
            if (_currentModel != null)
            {
                Destroy(_currentModel);
            }
        }
    }
}
