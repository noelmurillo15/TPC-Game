/*
* ArmorItemType -
* Created by : Allan N. Murillo
* Last Edited : 8/19/2020
*/

using UnityEngine;

namespace ANM.TPC.Items
{
    [CreateAssetMenu(menuName = "Refactor/Items/ArmorType")]
    public class ArmorItemType : ScriptableObject
    {
        public bool isDisabledWhenNoItem;
    }
}
