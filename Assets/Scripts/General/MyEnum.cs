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

    public enum ActionTargetType
    {
        Self = 0,
        Enemy = 1,
        PartyMember = 2,
        AllEnemies = 3,
        AllPartyMembers = 4,
        AllUnits = 5,
        AllUnits_ExceptSelf = 6
    }

    public enum DamageType
    {
        Physical = 0,
        Magic = 1,
        Combination = 2,
        Absolute = 3    //goes through defense
    }

    public enum PlayerPanelType
    {
        MainActionMenu = 0,
        SkillMenu = 1,
        Targetting = 2
    }

    public enum NumberType
    {
        Damage = 0,
        Heal = 1
    }

    public enum BattleUI_UpdateType
    {
        HP = 0,
        MP = 1
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

    public enum Player_Battle_Actions_Cause
    {
        Back = 0,
        Confirm = 1
    }

    public enum Player_Battle_Actions_Effect
    {
        GoBackToPreviousMenu = 0,
        ConfirmTargetSelection = 1
    }

    public enum SceneNames
    {
        Overworld = 0,
        Battle = 1
    }
}
