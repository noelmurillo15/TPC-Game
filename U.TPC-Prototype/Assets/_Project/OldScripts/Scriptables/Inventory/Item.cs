/*
 * Item - 
 * Created by : Allan N. Murillo
 * Last Edited : 3/2/2020
 */

using UnityEngine;

namespace ANM.Scriptables.Inventory
{
    public class Item : ScriptableObject
    {
        public ItemType type;
        public ItemUiStats uiInfo;
        public Runtime runtime;

        public class Runtime
        {
            public bool Equipped;
        }
        
        [System.Serializable]
        public class ItemUiStats
        {
            public string itemName;
            public string itemDescription;
            public string skillDescription;
            public Sprite itemIcon;
        }
    }
    
    public enum ItemType
    {
        WEAPON, ARMOR, CONSUMABLE, SPELL
    }
}
