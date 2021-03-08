/*
* AnimatorHook -
* Created by : Allan N. Murillo
* Last Edited : 8/18/2020
*/

using UnityEngine;
using ANM.TPC.StateManagers;

namespace ANM.TPC.Utilities
{
    public class AnimatorHook : MonoBehaviour
    {
        private CharacterStateManager _csm;

        public virtual void Initialize(StateManager stateManager)
        {
            _csm = (CharacterStateManager) stateManager;
        }


        public void OnAnimatorMove()
        {
            OnAnimatorMoveOverride();
        }

        protected virtual void OnAnimatorMoveOverride()
        {
            if (!_csm.useRootMotion) return;

            if (_csm.isGrounded && _csm.delta > 0f)
            {
                Vector3 v = _csm.myAnimator.deltaPosition / _csm.delta;
                v.y = _csm.myRigidbody.velocity.y;
                _csm.myRigidbody.velocity = v;
            }
        }
    }
}
