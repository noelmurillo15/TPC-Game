using UnityEngine;


namespace SA.Inventory
{
    [CreateAssetMenu(menuName = "Items/Item")]
    public class Item : ScriptableObject
    {
        public ItemType type;
        public ItemUIStats uiInfo;
        public Object obj;

    }

    //  TODO : SA put this in the Item Class
    [System.Serializable]
    public class ItemUIStats
    {
        public string itemName;
        public string itemDescription;
        public string skillDescription;
        public Sprite itemIcon;
    }

    public enum ItemType
    {
        WEAPON, ARMOR, CONSUMABLE, SPELL
    }
}