/*
 * BooleanVariable -
 * Created by : Allan N. Murillo
 * Last Edited : 5/11/2020
 */

using UnityEngine;

namespace ANM.Scriptables.Variables
{
    [CreateAssetMenu(menuName = "Scriptables/Variables/Boolean")]
    public class BooleanVariable : ScriptableObject
    {
        public bool value;
    }
}
