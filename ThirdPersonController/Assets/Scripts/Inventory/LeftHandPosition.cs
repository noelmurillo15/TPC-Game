/*
 * LeftHandPosition SO - 
 * Created by : Allan N. Murillo
 * Last Edited : 2/24/2020
 */

using UnityEngine;

namespace SA.Inventory
{    
    [CreateAssetMenu(menuName = "Weapons/Left Hand Position")]
    public class LeftHandPosition : ScriptableObject {
        public Vector3 position;
        public Vector3 eulersAngles;
        public Vector3 scale;
    }
}