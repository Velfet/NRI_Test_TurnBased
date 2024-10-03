using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MyEnum
{
    public enum UnitType
    {
        Player = 0,
        Enemy = 1,
        Friendly = 2
    }

    public enum Player_Overworld_Actions_Cause
    {
        Back = 0
    }

    public enum Player_Overworld_Actions_Effect
    {
        OpenPausePopup = 0,
        ClosePausePopup = 1
    }
}
