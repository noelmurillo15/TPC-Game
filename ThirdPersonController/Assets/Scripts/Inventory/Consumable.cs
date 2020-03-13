/*
 * Consumable - 
 * Created by : Allan N. Murillo
 * Last Edited : 3/2/2020
 */

using UnityEngine;

namespace ANM.Inventory
{
    [CreateAssetMenu(menuName = "Items/Consumable")]
    public class Consumable : Item
    {
        public Consumable()
        {
            type = ItemType.CONSUMABLE;
        }
    }
}