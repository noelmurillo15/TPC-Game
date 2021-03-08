/*
* InitializeAnimator -
* Created by : Allan N. Murillo
* Last Edited : 3/13/2020
*/

using UnityEngine;
using ANM.Managers;
using ANM.Utilities;

namespace ANM.Scriptables.Behaviour.StateActions.Init
{
    [CreateAssetMenu(menuName = "Scriptables/Behaviours/StateAction/Init/Animator")]
    public class InitializeAnimator : StateAction
    {
        public override void Execute(StateManager state)
        {
            if (state.myAnimator == null) return;

            state.myAnimator.applyRootMotion = false;
            state.myAnimator.GetBoneTransform(HumanBodyBones.LeftHand).localScale = Vector3.one;
            state.myAnimator.GetBoneTransform(HumanBodyBones.RightHand).localScale = Vector3.one;

            state.animatorHook = state.activeModel.AddComponent<AnimatorHook>();
            state.animatorHook.Init(state);
        }
    }
}
