using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitPlayer : UnitBase
{
    [SerializeField] private UnitActionManager_Player UnitActionManager;
    
    private PlayerActionMenu playerActionMenu;

    
    public override void BattleSetUp()
    {
        //don't change health and mp to max value after beginning a battle
        UnitType = MyEnum.UnitType.Player;
        //reset buffs
        BuffCounter = 0;
        CheckBuffs();
    }

    [ContextMenu("test defend")]
    public void TestDefend()
    {
        UnitAction testAction = UnitActionManager.GetDefend();

        //check action can be activated
        if(testAction.CanExecuteAction(this))
        {
            //store the action
            SelectedAction = testAction;

            testAction.ActivateAction();
        }
    }

    // public override void UpdateCurrentHP(float alterAmount)
    // {
    //     base.UpdateCurrentHP(alterAmount);

    //     //call hp update event
    //     hpChangeEvent.Invoke(this);
    // }

    // public override void UpdateCurrentMP(float alterAmount)
    // {
    //     base.UpdateCurrentMP(alterAmount);

    //     //call mp update event
    //     mpChangeEvent.Invoke(this);
    // }

    public override void BeginAct()
    {
        base.BeginAct();

        if(battleManager == null)
        {
            battleManager = BattleManager.Instance;
        }

        if(playerActionMenu == null)
        {
            playerActionMenu = battleManager.GetPlayerActionMenu();
        }
        //setup menus and open player battle menu
        playerActionMenu.SetupMenus(this, MyEnum.PlayerPanelType.MainActionMenu);
        //enable player controls in battle manager
        battleManager.SetPlayerControlStatus(true);
        //once an action has been selected and the action is available, select target depending on action
            //select player target
            //select enemy target
            //select everyone
        //after target has been selected, execute action. Remember to store the selected action in SelectedAction before executing it
        
        
        
    }

    public UnitActionManager_Player GetUnitActionManager()
    {
        return UnitActionManager;
    }
    
    
}
