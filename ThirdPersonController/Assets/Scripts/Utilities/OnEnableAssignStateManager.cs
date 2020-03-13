/*
* OnEnableAssignStateManager - 
* Created by : Allan N. Murillo
* Last Edited : 3/12/2020
*/

using UnityEngine;
using ANM.Managers;
using ANM.Scriptables.Variables;

namespace ANM.Utilities
{
    public class OnEnableAssignStateManager : MonoBehaviour
    {
        public StatesManagerVariable variable;


        private void OnEnable()
        {
            variable.value = GetComponent<StateManager>();
        }
    }
}