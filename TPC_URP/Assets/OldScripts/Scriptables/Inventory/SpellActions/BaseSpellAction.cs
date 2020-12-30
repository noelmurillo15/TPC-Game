/*
* BaseSpellAction -
* Created by : Allan N. Murillo
* Last Edited : 5/19/2020
*/

using UnityEngine;
using ANM.Managers;

namespace ANM.Scriptables.Inventory.SpellActions
{
    public abstract class BaseSpellAction : ScriptableObject
    {
        public abstract bool Cast(StateManager state);
    }
}
