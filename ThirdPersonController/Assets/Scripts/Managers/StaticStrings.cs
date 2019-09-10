using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace SA
{
    public static class StaticStrings
    {
        //   Input Manager Axes - Must match names exactly
        public static string Horizontal = "Horizontal";
        public static string Vertical = "Vertical";
        public static string B = "B";
        public static string A = "A";
        public static string X = "X";
        public static string Y = "Y";
        public static string RT = "RT";
        public static string RB = "RB";
        public static string LT = "LT";
        public static string LB = "LB";
        public static string L = "L";
        public static string R = "R";
        public static string DPad_X = "DPad_X";
        public static string DPad_Y = "DPad_Y";
        public static string Mouse_X = "Mouse_X";
        public static string Mouse_Y = "Mouse_Y";
        public static string RightAxis_X = "RightAxis_X";
        public static string RightAxis_Y = "RightAxis_Y";
        public static string Select = "Select";
        public static string Start = "Start";

        //  Animator Parameters
        public static string vertical = "vertical";
        public static string horizontal = "horizontal";
        public static string mirror = "mirror";
        public static string parry_attack = "parry_attack";
        public static string animSpeed = "animSpeed";
        public static string onGround = "onGround";
        public static string run = "run";
        public static string two_handed = "two_handed";
        public static string interacting = "interacting";
        public static string blocking = "blocking";
        public static string isleft = "isleft";
        public static string canMove = "canMove";
        public static string onEmpty = "onEmpty";
        public static string lockOn = "lockOn";
        public static string spellCasting = "spellCasting";
        public static string enableItem = "enableItem";

        //  Animator States
        public static string jump_Start = "jump_Start";
        public static string jump_Land = "jump_Land";
        public static string rolls = "rolls";
        public static string attack_interupt = "attack_interupt";
        public static string parry_recieved = "parry_recieved";
        public static string backStabbed = "backStabbed";
        public static string damage1 = "damage1";
        public static string damage2 = "damage2";
        public static string damage3 = "damage3";
        public static string changeWeapon = "changeWeapon";
        public static string empty_Both = "empty_Both";
        public static string empty_Left = "empty_Left";
        public static string empty_Right = "empty_Right";
        public static string equipWeapon_OH = "equipWeapon_OH";
        public static string pickup = "pickup";

        //  Other
        public static string _l = "_l";
        public static string _r = "_r";
    }
}