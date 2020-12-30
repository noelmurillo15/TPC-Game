/*
* ArmorItem -
* Created by : Allan N. Murillo
* Last Edited : 8/19/2020
*/

using UnityEngine;

namespace ANM.TPC.Items
{
    [CreateAssetMenu(menuName = "Refactor/Items/Armor")]
    public class ArmorItem : Item
    {
        public ArmorItemType armorType;
        public Mesh mesh;
        public Material armorMaterial;
    }
}
