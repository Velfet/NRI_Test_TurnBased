using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadyRemedy : UnitAction
{
    //determine how much the stats should affect the basic atk damage
    [SerializeField] private float PhysAtkScaling;
    [SerializeField] private float MagAtkScaling;
    
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
        //heal the target once animation finishes
        float healAmount = GetHealAmount_Atk(PhysAtkScaling, MagAtkScaling);

        targetUnits[0].HealUnit(healAmount);
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
