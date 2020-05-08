/*
 * ResourcesManager - Contains a reference to all created AbstractItems
 * Created by : Allan N. Murillo
 * Last Edited : 3/14/2020
 */

using UnityEngine;
using ANM.Scriptables.Inventory;
using System.Collections.Generic;

namespace ANM.Scriptables.Managers
{
    [CreateAssetMenu(menuName = "Single Instances/Resources Manager")]
    public class ResourcesManager : ScriptableObject
    {
        public List<AbstractItem> allItems = new List<AbstractItem>();
        private readonly Dictionary<string, AbstractItem> _itemDict = new Dictionary<string, AbstractItem>();


        public void Initialize()
        {
            foreach (var item in allItems)
            {
                if (!_itemDict.ContainsKey(item.name))
                {
                    _itemDict.Add(item.name, item);
                }
                else
                {
                    Debug.Log("Duplicate Item");
                }
            }
        }

        public AbstractItem GetItem(string id)
        {
            _itemDict.TryGetValue(id, out var item);
            return item;
        }
        
        public AbstractItem GetItemInstance(string id)
        {
            if (string.IsNullOrEmpty(id)) return null;
            
            var defaultItem = GetItem(id);
            var item = Instantiate(defaultItem);
            item.name = defaultItem.name;
            
            return item;
        }
    }
}
