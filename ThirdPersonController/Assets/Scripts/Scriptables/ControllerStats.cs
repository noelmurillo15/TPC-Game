using UnityEngine;


namespace SA.Scriptable
{
    [CreateAssetMenu(menuName = "Single Instances/ControllerStats")]
    public class ControllerStats : ScriptableObject
    {   //  Holds Movement Data for Player
        public float moveSpeed;
        public float rollSpeed;
        public float backstepSpeed;
        public float sprintSpeed;
        public float rotateSpeed;
        public float walkSpeed;
    }
}