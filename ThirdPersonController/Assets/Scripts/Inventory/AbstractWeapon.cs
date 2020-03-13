/*
* AbstractWeapon - Contains data necessary to create a runtime instance of a Weapon
* Created by : Allan N. Murillo
* Last Edited : 3/13/2020
*/

using UnityEngine;

namespace ANM.Inventory
{
    [CreateAssetMenu(menuName = "Inventory_2/Weapon")]
    public class AbstractWeapon : AbstractItem
    {
        public GameObject modelPrefab;
        public AbstractRuntimeWeapon runtime;
        

        public void Init()
        {
            runtime = new AbstractRuntimeWeapon {ModelInstance = Instantiate(modelPrefab)};
            runtime.ModelInstance.SetActive(false);
            
            runtime.WeaponHook = runtime.ModelInstance.GetComponentInChildren<AbstractWeaponHook>();
            runtime.WeaponHook.Init();
        }
    }
}