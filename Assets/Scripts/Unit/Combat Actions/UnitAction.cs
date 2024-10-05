using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//base class for combat actions
public class UnitAction : MonoBehaviour, ICombatAction
{
    [SerializeField] protected UnitBase myUnit;
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

    public virtual void PopulatePotentialTargets()
    {
        //clear the list
        potentialTargets = new List<UnitBase>();
        
        if(battleUnitManager == null)
        {
            battleUnitManager = BattleManager.Instance.GetBattleUnitManager();
        }

        MyEnum.UnitType myUnitType = myUnit.GetUnitType();

        if(myUnitType == MyEnum.UnitType.Player)
        {
            if(TargetType == MyEnum.ActionTargetType.Self)
            {
                potentialTargets.Add(myUnit);
            }
            else if(TargetType == MyEnum.ActionTargetType.Enemy)
            {
                //select enemy of player, aka enemies
                potentialTargets.AddRange(battleUnitManager.GetEnemyUnits_Alive());
            }
            else if(TargetType == MyEnum.ActionTargetType.PartyMember)
            {
                //select party member of player, aka players
                potentialTargets.AddRange(battleUnitManager.GetPlayerUnits_Alive());
            }
            else if(TargetType == MyEnum.ActionTargetType.AllEnemies)
            {
                //select all enemies of the player, aka all enemies
                potentialTargets.AddRange(battleUnitManager.GetEnemyUnits_Alive());
            }
            else if(TargetType == MyEnum.ActionTargetType.AllPartyMembers)
            {
                //select all party members of player, aka all players
                potentialTargets.AddRange(battleUnitManager.GetPlayerUnits_Alive());
            }
            else if(TargetType == MyEnum.ActionTargetType.AllUnits)
            {
                //select all units that are still alive
                potentialTargets.AddRange(battleUnitManager.GetPlayerUnits_Alive());
                potentialTargets.AddRange(battleUnitManager.GetEnemyUnits_Alive());
            }
            else if(TargetType == MyEnum.ActionTargetType.AllUnits_ExceptSelf)
            {
                //select all units that are still alive, except self
                potentialTargets.AddRange(battleUnitManager.GetPlayerUnits_Alive());
                potentialTargets.AddRange(battleUnitManager.GetEnemyUnits_Alive());

                potentialTargets.Remove(myUnit);
            }            
        }
        else if(myUnitType == MyEnum.UnitType.Enemy)
        {
            if(TargetType == MyEnum.ActionTargetType.Self)
            {
                potentialTargets.Add(myUnit);
            }
            else if(TargetType == MyEnum.ActionTargetType.Enemy)
            {
                //select enemy of enemies, aka player
                potentialTargets.AddRange(battleUnitManager.GetPlayerUnits_Alive());
            }
            else if(TargetType == MyEnum.ActionTargetType.PartyMember)
            {
                //select party member of enemies, aka enemies
                potentialTargets.AddRange(battleUnitManager.GetEnemyUnits_Alive());
            }
            else if(TargetType == MyEnum.ActionTargetType.AllEnemies)
            {
                //select all enemies of the enemy, aka all players
                potentialTargets.AddRange(battleUnitManager.GetPlayerUnits_Alive());
            }
            else if(TargetType == MyEnum.ActionTargetType.AllPartyMembers)
            {
                //select all party members of enemies, aka all enemies
                potentialTargets.AddRange(battleUnitManager.GetEnemyUnits_Alive());
            }
            else if(TargetType == MyEnum.ActionTargetType.AllUnits)
            {
                //select all units that are still alive
                potentialTargets.AddRange(battleUnitManager.GetPlayerUnits_Alive());
                potentialTargets.AddRange(battleUnitManager.GetEnemyUnits_Alive());
            }
            else if(TargetType == MyEnum.ActionTargetType.AllUnits_ExceptSelf)
            {
                //select all units that are still alive, except self
                potentialTargets.AddRange(battleUnitManager.GetPlayerUnits_Alive());
                potentialTargets.AddRange(battleUnitManager.GetEnemyUnits_Alive());

                potentialTargets.Remove(myUnit);
            }
        }
    

    }

    public string GetActionName()
    {
        return CombatActionData.name;
    }
}
