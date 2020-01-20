using UnityEngine;
using SA.Managers;


namespace SA.Utilities
{
    public class DamageColliders : MonoBehaviour
    {
        public delegate void OnHit(StateManager stateManager);
        public OnHit onHit;


        private void CheckCollisions()
        {

        }

        private void OnTriggerEnter(Collider other)
        {
            StateManager m_StateManager = other.transform.GetComponentInChildren<StateManager>();
            if (m_StateManager == null) return;
            onHit?.Invoke(m_StateManager);
        }
    }
}