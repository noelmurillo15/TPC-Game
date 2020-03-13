/*
 * StringVariable - 
 * Created by : Allan N. Murillo
 * Last Edited : 3/13/2020
 */

using UnityEngine;

namespace ANM.Scriptables.Variables
{
    [CreateAssetMenu(menuName = "Variables/String")]
    public class StringVariable : ScriptableObject
    {
        public string value;
    }
}
