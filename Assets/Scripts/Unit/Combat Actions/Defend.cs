using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defend : UnitAction
{
    //private UnitBase myUnit;

    public override void ActivateAction()
    {
        //request a target
        targetUnits[0] = myUnit;
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

    public override void FinalizeTarget(List<UnitBase> targets)
    {
        targetUnits = targets;
    }

    public override bool CanExecuteAction(UnitBase theUnit)
    {
        myUnit = theUnit;

        //might want to call a function from the base class to populate the potential target list
        PopulatePotentialTargets();

        
        //should always be able to defend
        return true;
    }


}
