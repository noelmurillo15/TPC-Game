/*
 * InputButtonVariable - 
 * Created by : Allan N. Murillo
 * Last Edited : 3/13/2020
 */

using UnityEngine;
using ANM.Managers;

namespace ANM.Scriptables.Variables
{
    [CreateAssetMenu(menuName = "Variables/InputButton Variable")]
    public class InputButtonVariable : ScriptableObject
    {
        public StateManager.InputButton value;


        public void Set(StateManager state)
        {
            if (state.rb)
            {
                value = StateManager.InputButton.RB;
            }
            if (state.lb)
            {
                value = StateManager.InputButton.LB;
            }
            if (state.rt)
            {
                value = StateManager.InputButton.RT;
            }
            if (state.lt)
            {
                value = StateManager.InputButton.LT;
            }
        }
    }
}
