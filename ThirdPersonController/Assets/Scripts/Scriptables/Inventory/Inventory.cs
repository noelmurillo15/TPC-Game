﻿/*
 * Inventory - 
 * Created by : Allan N. Murillo
 * Last Edited : 3/14/2020
 */

using UnityEngine;
using System.Collections.Generic;

namespace ANM.Scriptables.Inventory
{
    [CreateAssetMenu(menuName = "Single Instances/Inventory", order = 0)]
    public class Inventory : ScriptableObject
    {
        public List<Item> allItems = new List<Item>();
        public List<Item> runtimeItems = new List<Item>();
        
        private readonly Dictionary<string, int> _itemsDictionary = new Dictionary<string, int>();

        
        public void Initialize()
        {            
            #if UNITY_EDITOR
            allItems = Utilities.EditorUtils.EditorUtilities.FindAssetsByType<Item>();
            #endif
            
            runtimeItems.Clear();
            for (var i = 0; i < allItems.Count; i++)
            {
                if (_itemsDictionary.ContainsKey(allItems[i].name)) return;
                _itemsDictionary.Add(allItems[i].name, i);
            }
        }

        public Item GetItem(string id)
        {
            Item temp = null;

            if (_itemsDictionary.TryGetValue(id, out var index))
                temp = allItems[index];
            else
                Debug.LogError("No Item with " + id + " found");

            return temp;
        }
    }
}