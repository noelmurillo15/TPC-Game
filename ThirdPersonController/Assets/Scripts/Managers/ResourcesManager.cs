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

        public Inventory.Item GetItem(string _id)
        {
            return inventory.GetItem(_id);
        }

        public Inventory.Weapon GetWeapon(string _id)
        {
            Inventory.Item item = GetItem(_id);
            return (Inventory.Weapon)item.obj;
        }

        public Inventory.Armor GetArmor(string _id)
        {
            Inventory.Item item = GetItem(_id);
            return (Inventory.Armor)item.obj;
        }
    }
}