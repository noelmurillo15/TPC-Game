/*
 * ResourcesManager SO - 
 * Created by : Allan N. Murillo
 * Last Edited : 2/24/2020
 */

using UnityEngine;
using SA.Inventory;

namespace SA.Managers
{
    [CreateAssetMenu(menuName = "Single Instances/Resources Manager")]
    public class ResourcesManager : ScriptableObject
    {
        public Inventory.Inventory inventory;
        public RuntimeReferences runtime;


        public void Initialize()
        {
            runtime.Initialize();
            inventory.Initialize();
        }

        public Item GetItem(string id)
        {
            return inventory.GetItem(id);
        }

        public Weapon GetWeapon(string id)
        {
            var item = GetItem(id);
            return (Weapon)item.obj;
        }

        public Armor GetArmor(string id)
        {
            var item = GetItem(id);
            return (Armor)item.obj;
        }
    }
}