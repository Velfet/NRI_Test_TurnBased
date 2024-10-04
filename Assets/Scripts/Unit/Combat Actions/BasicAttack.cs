using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttack : UnitAction
{
    [SerializeField] private MyEnum.DamageType DamageType;
    //determine how much the stats should affect the basic atk damage
    [SerializeField] private float PhysScaling;
    [SerializeField] private float MagScaling;

    private UnitBase myUnit;
    
    

    public override void ActivateAction()
    {
        //request a target, this should already happen in "FinalizeTarget"
        //targetUnits[0] = myUnit;
        //play animation
        myUnit.StartAnimation(CombatActionData.AnimClipName);      

    }

    public override void ExecuteActionEffect()
    {
        //attack the target once animation finishes
        float damageAmount = myUnit.GetFinalPhysAtk() * (1f + PhysScaling);
        damageAmount += myUnit.GetFinalMagAtk() * (1f + MagScaling);

        targetUnits[0].DamageUnit(damageAmount, DamageType);
    }

    public override void FinalizeTarget(List<UnitBase> targets)
    {
        targetUnits = targets;
    }

    public override bool CanExecuteAction(UnitBase theUnit)
    {
        myUnit = theUnit;

        if(battleUnitManager == null)
        {
            battleUnitManager = BattleManager.Instance.GetBattleUnitManager();
        }

        //check if there is a valid target
        potentialTargets = new List<UnitBase>();
        
        MyEnum.UnitType myUnitType = myUnit.GetUnitType();

        if(myUnitType == MyEnum.UnitType.Player)
        {
            //get valid enemies
            potentialTargets = battleUnitManager.GetEnemyUnits_Alive();
        }
        else if(myUnitType == MyEnum.UnitType.Enemy)
        {
            //get valid players
            potentialTargets = battleUnitManager.GetPlayerUnits_Alive();
        }

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
