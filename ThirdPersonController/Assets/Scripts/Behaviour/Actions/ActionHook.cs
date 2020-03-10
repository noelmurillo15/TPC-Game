/*
* ActionHook - for Behaviour Actions (NOT STATE ACTIONS)
* Created by : Allan N. Murillo
* Last Edited : 3/10/2020
*/

using UnityEngine;

namespace ANM.Behaviour.Actions
{
    public class ActionHook : MonoBehaviour
    {
        public Action[] updateActions;

        private void Update()
        {
            foreach (var action in updateActions)
            {
                action.Execute();
            }
        }
    }
}
