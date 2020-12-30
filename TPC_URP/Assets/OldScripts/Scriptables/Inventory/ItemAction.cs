/*
* ItemAction -
* Created by : Allan N. Murillo
* Last Edited : 5/19/2020
*/

using UnityEngine;
using ANM.Managers;

namespace ANM.Scriptables.Inventory
{
    public abstract class ItemAction : ScriptableObject
    {
        public abstract void Execute(StateManager state);
    }
}
