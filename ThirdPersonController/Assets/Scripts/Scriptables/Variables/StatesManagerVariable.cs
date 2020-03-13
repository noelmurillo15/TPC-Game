/*
 * StatesManagerVariable - 
 * Created by : Allan N. Murillo
 * Last Edited : 3/12/2020
 */

using UnityEngine;
using ANM.Managers;

namespace ANM.Scriptables.Variables
{
    [CreateAssetMenu(menuName = "Variables/States Manager")]
    public class StatesManagerVariable : ScriptableObject
    {
        public StateManager value;
    }
}