/*
* Spell -
* Created by : Allan N. Murillo
* Last Edited : 5/19/2020
*/

//    TODO : LEFT OFF ON SOULS-LIKE 94 Spell Items Base 15:27

using ANM.Managers;
using ANM.Scriptables.Inventory.SpellActions;
using UnityEngine;

namespace ANM.Scriptables.Inventory
{
    [CreateAssetMenu(menuName = "Scriptables/Inventory/Spell")]
    public class Spell : Item
    {
        public BaseSpellAction spellAction;


        public void SetSpellToCast(StateManager state)
        {
            state.equippedSpellAction = spellAction;
        }
    }
}
