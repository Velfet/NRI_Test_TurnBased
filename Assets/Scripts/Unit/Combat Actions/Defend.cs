using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defend : UnitAction
{
    private UnitBase myUnit;

    public override void ActivateAction()
    {
        //request a target
        // if(myUnit == null)
        // {
        //     Debug.Log("myunit is null");
        // }

        // if(targetUnits[0] == null)
        // {

        // }

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

        //check if there is a valid target
        potentialTargets = new List<UnitBase>
        {
            myUnit
        };

        

        return true;
    }


}
