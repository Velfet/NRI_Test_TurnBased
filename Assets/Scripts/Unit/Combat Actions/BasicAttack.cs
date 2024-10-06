using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttack : UnitAction
{
    [SerializeField] private MyEnum.DamageType DamageType;
    //determine how much the stats should affect the basic atk damage
    [SerializeField] private float PhysScaling;
    [SerializeField] private float MagScaling;

    //private UnitBase myUnit;
    
    

    public override void ActivateAction()
    {
        //show action name UI
        base.ActivateAction();
        //request a target, this should already happen in "FinalizeTarget"
        //targetUnits[0] = myUnit;
        //play animation
        myUnit.StartAnimation(CombatActionData.AnimClipName);      

    }

    public override void ExecuteActionEffect()
    {
        //hide action name UI
        base.ExecuteActionEffect();
        //attack the target once animation finishes
        float damageAmount = GetDamageAmount(PhysScaling, MagScaling);

        targetUnits[0].DamageUnit(damageAmount, DamageType);
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

        //if there are potential targets, action can be executed
        if(potentialTargets.Count > 0)
        {
            return true;
        }
        else
        {
            return false;
        }

        
    }
}
