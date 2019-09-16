using UnityEngine;


namespace SA
{
    public class AnimatorHook : MonoBehaviour
    {   //  Used to help with Root Motion and Animaton events
        StateManager stateManager;
        Animator m_animator;
        public float rm_mult;


        public void Init(StateManager _state)
        {
            rm_mult = 1f;
            stateManager = _state;
            m_animator = stateManager.m_animator;
        }

        void OnAnimatorMove()
        {
            stateManager.m_input.animationDelta = m_animator.deltaPosition;
            stateManager.m_input.animationDelta.y = 0f;
            transform.localPosition = Vector3.zero;

            if (rm_mult == 0) rm_mult = 1;

            Vector3 v = (stateManager.m_input.animationDelta * rm_mult) / stateManager.m_delta;
            stateManager.m_rigidbody.velocity = v;
        }
    }
}