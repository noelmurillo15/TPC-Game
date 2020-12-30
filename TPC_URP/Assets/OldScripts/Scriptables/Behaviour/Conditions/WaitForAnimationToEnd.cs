/*
* WaitForAnimationToEnd - Checks if the States' IsInteracting animator bool is set to false
* IsInteracting is set true when an animation override is playing (attack, roll, sprint animations)
* Created by : Allan N. Murillo
* Last Edited : 3/12/2020
*/

using UnityEngine;
using ANM.Managers;

namespace ANM.Scriptables.Behaviour.Conditions
{
    [CreateAssetMenu(menuName = "Scriptables/Behaviours/Conditions/Wait For Animation To End")]
    public class WaitForAnimationToEnd : Condition
    {
        public string targetBool = "isInteracting";

        public override bool CheckCondition(StateManager state)
        {
            var retVal = !state.myAnimator.GetBool(targetBool);
            return retVal;
        }
    }
}
