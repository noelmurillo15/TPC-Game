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
        public List<Item> allItems = new List<Item>();
        private Dictionary<string, int> dict = new Dictionary<string, int>();


        public void Initialize()
        {            
            #if UNITY_EDITOR
            allItems = EditorUtilities.FindAssetsByType<Item>();
            #endif

            for (var i = 0; i < allItems.Count; i++)
            {
                if (dict.ContainsKey(allItems[i].name)) return;
                dict.Add(allItems[i].name, i);
            }
        }

        public Item GetItem(string _id)
        {
            Item temp = null;
            int index = -1;

            if (dict.TryGetValue(_id, out index))
            {
                temp = allItems[index];
            }

            if (index == -1)
            {
                Debug.LogError("No Item with " + _id + " found");
            }

            return temp;
        }
    }
}