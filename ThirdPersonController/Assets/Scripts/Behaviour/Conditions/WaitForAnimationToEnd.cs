/*
* WaitForAnimationToEnd - 
* Created by : Allan N. Murillo
* Last Edited : 3/10/2020
*/

using UnityEngine;
using ANM.Managers;

namespace ANM.Behaviour.Conditions
{
    [CreateAssetMenu(menuName = "Behaviours/Conditions/WaitForAnimToEnd")]
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
