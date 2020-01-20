using UnityEngine;
#if UNITY_EDITOR
using SA.Utilities.Editor;
#endif
using System.Collections.Generic;


namespace SA.Inventory
{
    [CreateAssetMenu(menuName = "Single Instances/Inventory", order = 0)]
    public class Inventory : ScriptableObject
    {
        public List<Item> all_items = new List<Item>();
        private Dictionary<string, int> dict = new Dictionary<string, int>();


        public void Initialize()
        {            
            #if UNITY_EDITOR
            all_items = EditorUtilities.FindAssetsByType<Item>();
            #endif

            for (int i = 0; i < all_items.Count; i++)
            {
                if (dict.ContainsKey(all_items[i].name))
                {

                }
                else
                {
                    dict.Add(all_items[i].name, i);
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