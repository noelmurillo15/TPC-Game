/*
 * AnimatorHook - 
 * Created by : Allan N. Murillo
 * Last Edited : 2/24/2020
 */

using UnityEngine;
using ANM.Managers;

namespace ANM.Utilities
{
    public class AnimatorHook : MonoBehaviour
    {
        private StateManager _stateManager;
        private Animator _animator;


        public void Init(StateManager state)
        {
            _stateManager = state;
            _animator = _stateManager.myAnimator;
        }

        #region Animation Callback Events

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
