using UnityEngine;


namespace SA.Managers
{
    public class ResourcesManager : MonoBehaviour
    {
        public Inventory.Inventory inventory;


        void Awake()
        {
            inventory.Init();
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