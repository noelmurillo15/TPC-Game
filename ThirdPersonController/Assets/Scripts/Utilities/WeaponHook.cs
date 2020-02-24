/*
 * WeaponHook - 
 * Created by : Allan N. Murillo
 * Last Edited : 2/24/2020
 */

using UnityEngine;
using SA.Managers;

namespace SA.Utilities
{
    public class WeaponHook : MonoBehaviour
    {
        public Collider[] damageColliders;


        public void Initialize(StateManager stateManager)
        {
            damageColliders = GetComponentsInChildren<Collider>();
            InitColliders(stateManager);
        }

        private void InitColliders(StateManager stateManager)
        {
            foreach (var t in damageColliders)
            {
                t.isTrigger = true;
                t.enabled = false;

                var d = t.gameObject.AddComponent<DamageColliders>();
                d.onHit = stateManager.HandleDamageCollision;
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