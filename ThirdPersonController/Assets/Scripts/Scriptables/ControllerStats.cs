using UnityEngine;


namespace SA
{
    [CreateAssetMenu(menuName = "Single Instances/ControllerStats")]
    public class ControllerStats : ScriptableObject
    {   //  Holds Movement Data for Player
        public float moveSpeed;
        public float sprintSpeed;
        public float rotateSpeed;
    }
}