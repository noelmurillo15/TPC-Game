/*
* BatchStateAction - Contains multiple state actions to execute
* Created by : Allan N. Murillo
* Last Edited : 3/13/2020
*/

using UnityEngine;
using ANM.Managers;

namespace ANM.Scriptables.Behaviour.StateActions
{
    [CreateAssetMenu(menuName = "Behaviours/StateAction/Batch")]
    public class BatchStateAction : StateAction
    {
        public StateAction[] stateActions;
        
        
        public override void Execute(StateManager state)
        {
            foreach (var a in stateActions)
            {
                a.Execute(state);
            }
        }
    }
}
