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