/*
 * ResourcesManager SO - 
 * Created by : Allan N. Murillo
 * Last Edited : 3/2/2020
 */

using UnityEngine;
using ANM.Inventory;
using System.Collections.Generic;

namespace ANM.Managers
{
    [CreateAssetMenu(menuName = "Single Instances/Resources Manager")]
    public class ResourcesManager : ScriptableObject
    {
        public RuntimeReferences runtime;
        private InventoryData _inventoryData;
        private Inventory.Inventory _inventory;


        public void Initialize()
        {
            runtime = Resources.Load("RuntimeReferences") as RuntimeReferences;
            runtime?.Initialize();
            
            _inventory = Resources.Load("Inventory") as Inventory.Inventory;
            _inventory?.Initialize();
            
            _inventoryData = Resources.Load("PlayerInventory") as InventoryData;
        }

        public void InitPlayerInventory()
        {
            _inventoryData.data.Clear();
        }

        private void AddItemOnInventory(string id)
        {
            Item newItem = GetItem(id);
            AddItemOnInventory(newItem);
        }

        private void AddItemOnInventory(Item item)
        {
            if (item == null) return;
            Item newItem = Instantiate(item);
            _inventoryData.data.Add(newItem);
        }

        public Item GetItem(string id)
        {
            return _inventory.GetItem(id);
        }

        public Weapon GetWeapon(string id)
        {
            var item = GetItem(id);
            return (Weapon)item;
        }
        
        public Armor GetArmor(string id)
        {
            var item = GetItem(id);
            return (Armor)item;
        }

        public List<Item> GetAllItemsOfType(ItemType itemType)
        {
            var itemsOfType = new List<Item>();
            for (var x = 0; x < itemsOfType.Count; x++)
            {
                if (_inventory.allItems[x].type == itemType)
                {
                    itemsOfType.Add(_inventory.allItems[x]);
                }
            }
            return itemsOfType;
        }
    }
}