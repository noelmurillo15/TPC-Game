/*
 * AnimatorHook - 
 * Created by : Allan N. Murillo
 * Last Edited : 2/24/2020
 */

using ANM.Managers;
using UnityEngine;

namespace ANM
{
    public class AnimatorHook : MonoBehaviour
    {   //  Used to help with Root Motion and Animation callback events
        private StateManager _stateManager;
        private Animator _animator;
        public float rm_mult;
        public bool isEnemy = false;    //  TODO : remove this later


        public void Init(StateManager state, bool isEnemy)
        {
            rm_mult = 1f;
            _stateManager = state;
            _animator = _stateManager.myAnimator;
            this.isEnemy = isEnemy;
        }

        #region Animation Callback Events

        private void OnAnimatorMove()
        {
            _stateManager.inputVar.animationDelta = _animator.deltaPosition;
            _stateManager.inputVar.animationDelta.y = 0f;
            transform.localPosition = Vector3.zero;

            if (rm_mult == 0) rm_mult = 1;

            if (isEnemy) return;
            Vector3 v = (_stateManager.inputVar.animationDelta * rm_mult) / _stateManager.deltaTime;
            if (!float.IsNaN(v.x) && !float.IsNaN(v.y))
            {
                _stateManager.myRigidbody.velocity = v;
            }
        }

        public void CloseParticle()
        {

        }

        public void InitiateThrowForProjectile()
        {
            _stateManager.CastSpellActual();
        }

        public void OpenDamageColliders()
        {
            _stateManager.SetDamageColliderStatus(true);
        }

        public void CloseDamageColliders()
        {
            _stateManager.SetDamageColliderStatus(false);
        }
        #endregion
    }
}