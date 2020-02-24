/*
 * Armor - 
 * Created by : Allan N. Murillo
 * Last Edited : 2/24/2020
 */

using UnityEngine;

namespace SA.Inventory
{
    [CreateAssetMenu(menuName = "Items/Armor")]
    public class Armor : ScriptableObject
    {
        public ArmorType armorType;
        public Mesh armorMesh;
        public Material[] materials;
        public bool baseBodyEnabled;
    }
}

public enum ArmorType
{
    CHEST, LEGS, HANDS, HEAD
}