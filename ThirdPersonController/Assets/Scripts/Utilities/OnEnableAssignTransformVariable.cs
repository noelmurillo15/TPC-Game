/*
 * OnEnableAssignTransformVariable -
 * Created by : Allan N. Murillo
 * Last Edited : 3/10/2020
 */

using UnityEngine;
using ANM.Scriptables.Variables;

namespace ANM.Utilities
{
    public class OnEnableAssignTransformVariable : MonoBehaviour
    {
        public TransformVariable targetVariable;


        private void OnEnable()
        {
            targetVariable.value = this.transform;
        }
    }
}