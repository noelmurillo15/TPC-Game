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

        void InitColliders(StateManager _stateManager)
        {
            for (int x = 0; x < damageColliders.Length; x++)
            {
                damageColliders[x].isTrigger = true;
                damageColliders[x].enabled = false;

                DamageColliders d = damageColliders[x].gameObject.AddComponent<DamageColliders>();
                d.onHit = _stateManager.HandleDamageCollision;
            }
        }

        public void OpenDamageColliders()
        {
            for (int i = 0; i < damageColliders.Length; i++)
            {
                damageColliders[i].enabled = true;
            }
        }

        public void CloseDamageColliders()
        {
            for (int i = 0; i < damageColliders.Length; i++)
            {
                damageColliders[i].enabled = false;
            }
        }
    }
}