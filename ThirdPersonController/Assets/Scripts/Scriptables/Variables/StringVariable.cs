/*
 * StringVariable SO - 
 * Created by : Allan N. Murillo
 * Last Edited : 2/24/2020
 */

using UnityEngine;

namespace SA.Scriptable.Variables
{
    [CreateAssetMenu(menuName = "Variables/String")]
    public class StringVariable : ScriptableObject
    {   //  Holds Animation Name
        public string value;
    }
}