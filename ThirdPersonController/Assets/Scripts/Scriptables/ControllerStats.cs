using UnityEngine;


namespace SA
{
    [CreateAssetMenu(menuName = "Single Instances/ControllerStats")]
    public class ControllerStats : ScriptableObject
    {
        public float moveSpeed;
        public float sprintSpeed;
        public float rotateSpeed;
    }
}