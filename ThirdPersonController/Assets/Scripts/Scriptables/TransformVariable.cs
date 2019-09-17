using UnityEngine;


namespace SA.Scriptable
{
    [CreateAssetMenu(menuName = "Variables/Transform")]
    public class TransformVariable : ScriptableObject
    {
        public Transform value;

        public void Set(Transform _transform)
        {
            value = _transform;
        }
    }
}