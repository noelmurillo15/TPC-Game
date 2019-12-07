using UnityEngine;
using SA.Managers;


namespace SA
{
    public class AnimatorHook : MonoBehaviour
    {   //  Used to help with Root Motion and Animaton callback events
        StateManager stateManager;
        Animator m_animator;
        public float rm_mult;
        public bool isEnemy = false;    //  TODO : remove this later


        public void Init(StateManager _state, bool _isEnemy)
        {
            rm_mult = 1f;
            stateManager = _state;
            m_animator = stateManager.m_animator;
            isEnemy = _isEnemy;
        }

        #region Animation Callback Events
        void OnAnimatorMove()
        {
            stateManager.m_input.animationDelta = m_animator.deltaPosition;
            stateManager.m_input.animationDelta.y = 0f;
            transform.localPosition = Vector3.zero;

            if (rm_mult == 0) rm_mult = 1;

            if (!isEnemy)
            {
                Vector3 v = (stateManager.m_input.animationDelta * rm_mult) / stateManager.m_delta;
                if (!float.IsNaN(v.x))
                {
                    stateManager.m_rigidbody.velocity = v;
                }
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