using UnityEngine;
using SA.Scriptable.Variables;


namespace SA.MonoActions
{
    public class AssignTransform : MonoBehaviour
    {
        public TransformVariable transformVariable;

        void Start()
        {
            transformVariable.value = this.transform;
            Destroy(this);
        }
    }
}