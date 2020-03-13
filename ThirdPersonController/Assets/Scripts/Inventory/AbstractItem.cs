/*
* AbstractItem - Base Item of the new Inventory System
* Created by : Allan N. Murillo
* Last Edited : 3/13/2020
*/

using UnityEngine;

namespace ANM.Inventory
{
    public abstract class AbstractItem : ScriptableObject
    {
        public string uiName;
        public Sprite uiIcon;
    }
}
