using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitPlayer : UnitBase
{
    [SerializeField] private UnitActionManager_Player UnitActionManager;

    
    public override void BattleSetUp()
    {
        //don't change health and mp to max value after beginning a battle
        UnitType = MyEnum.UnitType.Player;
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
    
}
