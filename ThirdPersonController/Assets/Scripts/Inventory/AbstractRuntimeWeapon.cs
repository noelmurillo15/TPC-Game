/*
* AbstractRuntimeWeapon - Holds data necessary for an instantiated AbstractWeapon to work runtime
* Created by : Allan N. Murillo
* Last Edited : 3/13/2020
*/

using UnityEngine;

namespace ANM.Inventory
{
    public class AbstractRuntimeWeapon
    {
        public GameObject ModelInstance;
        public AbstractWeaponHook WeaponHook;
    }
}