using UnityEngine;


namespace SA
{
    public class DamageColliders : MonoBehaviour
    {
        public delegate void OnHit(StateManager stateManager);
        public OnHit onHit;


        void CheckCollisions()
        {

        }

        private void OnTriggerEnter(Collider other)
        {
            StateManager m_StateManager = other.transform.GetComponentInChildren<StateManager>();
            if (m_StateManager != null)
            {
                if (onHit != null)
                {
                    onHit(m_StateManager);
                }
            }
        }
    }
}