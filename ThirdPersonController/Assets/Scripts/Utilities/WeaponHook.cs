using UnityEngine;
using SA.Managers;


namespace SA.Utilities
{
    public class WeaponHook : MonoBehaviour
    {
        public Collider[] damageColliders;


        public void Initialize(StateManager _stateManager)
        {
            damageColliders = GetComponentsInChildren<Collider>();
            InitColliders(_stateManager);
        }

        private void InitColliders(StateManager _stateManager)
        {
            foreach (var t in damageColliders)
            {
                t.isTrigger = true;
                t.enabled = false;

                DamageColliders d = t.gameObject.AddComponent<DamageColliders>();
                d.onHit = _stateManager.HandleDamageCollision;
            }
        }

        public void OpenDamageColliders()
        {
            foreach (var t in damageColliders)
            {
                t.enabled = true;
            }
        }

        public void CloseDamageColliders()
        {
            foreach (var t in damageColliders)
            {
                t.enabled = false;
            }
        }
    }
}