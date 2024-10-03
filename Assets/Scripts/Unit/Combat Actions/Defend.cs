using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defend : UnitAction
{
    private UnitBase myUnit;

    private UnitBase targetUnit;

    public override void ActivateAction()
    {
        //request a target
        targetUnit = myUnit;
        //play animation
        myUnit.StartAnimation(CombatActionData.AnimClipName);        
    }

    public override void ExecuteActionEffect()
    {
        //once animation finishes, increase defense with buff
        myUnit.IncreaseBuffCounter(1);
        myUnit.GetUnitBuffs().Set_PhysDef_Buff(0.3f);
        myUnit.GetUnitBuffs().Set_MagDef_Buff(0.3f);
    }

    public override bool CanExecuteAction(UnitBase theUnit)
    {
        //check if there is a valid target
        myUnit = theUnit;

        return true;
    }
}
