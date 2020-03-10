﻿/*
 * Action - 
 * Created by : Allan N. Murillo
 * Last Edited : 2/24/2020
 */

using UnityEngine;

namespace ANM.Scriptables
{
    [System.Serializable]
    public class Action
    {
        public ActionType actionType;
        public Object animationAction;
    }

    public enum ActionType
    {
        ATTACK,
        BLOCK,
        SPELL,
        PARRY
    }
}
