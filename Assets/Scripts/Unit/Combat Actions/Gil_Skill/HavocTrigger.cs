using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HavocTrigger : UnitAction
{
    [SerializeField] private MyEnum.DamageType DamageType;
    //determine how much the stats should affect the basic atk damage
    [SerializeField] private float PhysScaling;
    [SerializeField] private float MagScaling;
    
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
        //attack all targets once animation finishes
        float damageAmount = GetDamageAmount(PhysScaling, MagScaling);

        foreach(UnitBase unit in targetUnits)
        {
            unit.DamageUnit(damageAmount, DamageType);
        }
    }

    public override void FinalizeTarget(List<UnitBase> targets)
    {
        targetUnits = targets;
    }

    public override bool CanExecuteAction(UnitBase theUnit)
    {
        myUnit = theUnit;

        //check mp
        if(CheckEnoughMP() == false)
        {
            return false;
        }

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
