/*
 * AnimatorHook - 
 * Created by : Allan N. Murillo
 * Last Edited : 2/24/2020
 */

using UnityEngine;
using SA.Managers;

namespace SA
{
    public class AnimatorHook : MonoBehaviour
    {   //  Used to help with Root Motion and Animaton callback events
        private StateManager stateManager;
        private Animator m_animator;
        public float rm_mult;
        public bool isEnemy = false;    //  TODO : remove this later


        public void Init(StateManager state, bool isEnemy)
        {
            rm_mult = 1f;
            stateManager = state;
            m_animator = stateManager.myAnimator;
            this.isEnemy = isEnemy;
        }

        #region Animation Callback Events

        private void OnAnimatorMove()
        {
            stateManager.inputVar.animationDelta = m_animator.deltaPosition;
            stateManager.inputVar.animationDelta.y = 0f;
            transform.localPosition = Vector3.zero;

            if (rm_mult == 0) rm_mult = 1;

            if (isEnemy) return;
            Vector3 v = (stateManager.inputVar.animationDelta * rm_mult) / stateManager.deltaTime;
            if (!float.IsNaN(v.x) && !float.IsNaN(v.y))
            {
                stateManager.myRigidbody.velocity = v;
            }
        }

        public void CloseParticle()
        {

        }

        public void InitiateThrowForProjectile()
        {
            stateManager.CastSpellActual();
        }

        public void OpenDamageColliders()
        {
            stateManager.SetDamageColliderStatus(true);
        }

        public void CloseDamageColliders()
        {
            stateManager.SetDamageColliderStatus(false);
        }
        #endregion
    }
}