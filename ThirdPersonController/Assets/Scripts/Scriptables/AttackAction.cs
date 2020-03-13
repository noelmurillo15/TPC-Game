/*
 * AttackAction - 
 * Created by : Allan N. Murillo
 * Last Edited : 2/24/2020
 */

using UnityEngine;
using ANM.Scriptables.Variables;

namespace ANM.Scriptables
{
    [CreateAssetMenu(menuName = "Attack Action/Attack")]
    public class AttackAction : ScriptableObject
    {
        //  Holds Static String Animation Name and other info
        public StringVariable attackAnimation;
        public bool canBeParried = true;
        public bool changeSpeed = false;
        public float animSpeed = 1f;
        public bool canParry = false;
        public bool canBackStab = false;
    }
}
