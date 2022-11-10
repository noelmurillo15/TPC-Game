/*
* StatChangeAction -
* Created by : Allan N. Murillo
* Last Edited : 5/19/2020
*/

using UnityEngine;
using ANM.Managers;

namespace ANM.Scriptables.Inventory.SpellActions
{
    public class StatChangeAction : BaseSpellAction
    {
        //    TODO : add enum for stat and use as type
        [SerializeField] private float stateChangeAmount = 10f;
        public float timer = 2f;


        public override bool Cast(StateManager state)
        {
            state.generalDelta += state.delta;

            if (!(state.generalDelta > timer)) return false;
            //    TODO : update stat from StateManager here
            state.generalDelta = 0f;

            return true;
        }
    }
}
