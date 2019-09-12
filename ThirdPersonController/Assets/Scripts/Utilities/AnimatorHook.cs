using UnityEngine;


namespace SA
{
    public class AnimatorHook : MonoBehaviour
    {
        StateManager stateManager;
        Animator m_animator;


        public void Init(StateManager _state)
        {
            stateManager = _state;
            m_animator = stateManager.m_animator;
        }

        void OnAnimatorMove()
        {
            stateManager.input.animationDelta = m_animator.deltaPosition;
            transform.localPosition = Vector3.zero;
        }
    }
}