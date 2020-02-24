/*
 * OnStateEnterBool - 
 * Created by : Allan N. Murillo
 * Last Edited : 2/24/2020
 */

using UnityEngine;

namespace SA
{
    public class OnStateEnterBool : StateMachineBehaviour
    {   //  Helps with Animation Transitions
        public string boolName;
        public bool status;
        public bool resetOnExit;


        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetBool(boolName, status);
        }

        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (resetOnExit) animator.SetBool(boolName, !status);
        }
    }
}