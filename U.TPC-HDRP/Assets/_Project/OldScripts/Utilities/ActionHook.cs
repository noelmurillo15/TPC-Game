/*
* ActionHook - Executes Action logic inside a MonoBehaviours' Update
* Created by : Allan N. Murillo
* Last Edited : 3/12/2020
*/

using UnityEngine;
using ANM.Scriptables.Actions;

namespace ANM.Utilities
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
