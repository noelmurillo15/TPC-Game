/*
* AbstractWeapon - Contains data necessary to create a runtime instance of a Weapon
* Created by : Allan N. Murillo
* Last Edited : 3/13/2020
*/

using UnityEngine;
using ANM.Inventory;
using ANM.Scriptables.Variables;

namespace ANM.Scriptables.Inventory
{
    [CreateAssetMenu(menuName = "Inventory_2/Weapon")]
    public class AbstractWeapon : AbstractItem
    {
        public GameObject modelPrefab;
        public AbstractRuntimeWeapon runtime;
        public StringVariable anim1;
        public StringVariable anim2;
        public StringVariable anim3;
        public StringVariable anim4;


        public void Init()
        {
            runtime = new AbstractRuntimeWeapon {ModelInstance = Instantiate(modelPrefab)};
            runtime.ModelInstance.SetActive(false);

            runtime.WeaponHook = runtime.ModelInstance.GetComponentInChildren<AbstractWeaponHook>();
            runtime.WeaponHook.Init();
        }
    }
}
