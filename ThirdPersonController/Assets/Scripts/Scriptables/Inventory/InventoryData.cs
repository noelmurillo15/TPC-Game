/*
 * InventoryData - 
 * Created by : Allan N. Murillo
 * Last Edited : 3/2/2020
 */

using UnityEngine;
using System.Collections.Generic;

namespace ANM.Scriptables.Inventory
{
    [CreateAssetMenu(menuName = "Items/InventoryData")]
    public class InventoryData : ScriptableObject
    {
        public List<Item> data = new List<Item>();
    }
}