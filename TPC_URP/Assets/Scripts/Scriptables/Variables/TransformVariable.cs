/*
 * TransformVariable - 
 * Created by : Allan N. Murillo
 * Last Edited : 3/13/2020
 */

using UnityEngine;

namespace ANM.Scriptables.Variables
{
    [CreateAssetMenu(menuName = "Variables/Transform")]
    public class TransformVariable : ScriptableObject
    {
        public Transform value;

        public void Set(Transform transform)
        {
            value = transform;
        }
    }
}
