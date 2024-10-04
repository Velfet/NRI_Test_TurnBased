using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//base class for combat actions
public class UnitAction : MonoBehaviour, ICombatAction
{
    [SerializeField] protected MyEnum.ActionTargetType TargetType;
    [SerializeField] protected List<UnitBase> potentialTargets;
    [SerializeField] protected CombatActionData_SO CombatActionData;

    protected BattleUnitManager battleUnitManager;
    //unit or units that will be targeted. For a different action, this might be a list instead
    protected List<UnitBase> targetUnits;

    public virtual void ActivateAction()
    {
        //play animation

    }

    public virtual void ExecuteActionEffect()
    {
        //once animation finishes, apply the effect

    }

    public virtual void FinalizeTarget(List<UnitBase> targets)
    {

    }

    //pass in the unit performing the action
    public virtual bool CanExecuteAction(UnitBase theUnit)
    {

        return false;
    }

    public virtual List<UnitBase> GetPotentialTargets()
    {
        return potentialTargets;
    }

    public virtual MyEnum.ActionTargetType GetTargetType()
    {
        return TargetType;
    }

    public string GetActionName()
    {
        return CombatActionData.name;
    }
}
