using UnityEngine;
using System.Collections.Generic;


namespace SA.Inventory
{
    [CreateAssetMenu(menuName = "Single Instances/Inventory", order = 0)]
    public class Inventory : ScriptableObject
    {
        public Item[] all_items;
        Dictionary<string, int> dict = new Dictionary<string, int>();


        public void Init()
        {
            for (int i = 0; i < all_items.Length; i++)
            {
                if (dict.ContainsKey(all_items[i].itemID))
                {

                }
                else
                {
                    dict.Add(all_items[i].itemID, i);
                }
            }
        }

        public Item GetItem(string _id)
        {
            Item temp = null;
            int index = -1;

            if (dict.TryGetValue(_id, out index))
            {
                temp = all_items[index];
            }

            if (index == -1)
            {
                Debug.LogError("No Item with " + _id + " found");
            }

            return temp;
        }
    }
}