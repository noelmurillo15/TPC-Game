/*
* AbstractWeaponHook - Contains references to all damage colliders on an item and initializes them
* Created by : Allan N. Murillo
* Last Edited : 3/13/2020
*/

using System.Linq;
using UnityEngine;

namespace ANM.Inventory
{
    public class AbstractWeaponHook : MonoBehaviour
    {
        private DamageCollider[] _damageColliders;


        public void Init()
        {
            _damageColliders = transform.GetComponentsInChildren<DamageCollider>().ToArray();
            foreach (var dc in _damageColliders) dc.collider.isTrigger = true;
        }
    }
}