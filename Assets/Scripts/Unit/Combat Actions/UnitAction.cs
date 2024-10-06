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
    protected CombatActionUI combatActionUI;
    //unit or units that will be targeted. For a different action, this might be a list instead
    protected List<UnitBase> targetUnits;

    public virtual void ActivateAction()
    {
        //show action name UI
        if(combatActionUI == null)
        {
            combatActionUI = BattleManager.Instance.GetCombatActionUI();
        }
        combatActionUI.ShowCombatActionName(CombatActionData.Name);

        //consume mp
        if(GetActionMPCost() > 0)
        {
            myUnit.UpdateCurrentMP(-GetActionMPCost());
        }

        //play animation

    }

    public virtual void ExecuteActionEffect()
    {
        //hide action name UI
        if(combatActionUI == null)
        {
            combatActionUI = BattleManager.Instance.GetCombatActionUI();
        }
        combatActionUI.HideCombatActionName();
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

    public float GetActionMPCost()
    {
        return CombatActionData.MP_Cost;
    }

    protected float GetDamageAmount(float physScaling, float magScaling)
    {
        float damageAmount = myUnit.GetFinalPhysAtk() * (physScaling);
        damageAmount += myUnit.GetFinalMagAtk() * (magScaling);

        return damageAmount;
    }

    //healing that scales off of unit's attack
    protected float GetHealAmount_Atk(float physATkScaling, float MagAtkScaling)
    {
        float healAmount = myUnit.GetFinalPhysAtk() * (physATkScaling);
        healAmount += myUnit.GetFinalMagAtk() * (MagAtkScaling);

        return healAmount;
    }

    //healing that scales off of unit's defense
    protected float GetHealAmount_Def(float physDefScaling, float MagDefScaling)
    {
        float healAmount = myUnit.GetFinalPhysDef() * (physDefScaling);
        healAmount += myUnit.GetFinalMagDef() * (MagDefScaling);

        return healAmount;
    }

    //check if the unit has enough MP to perform the action
    protected bool CheckEnoughMP()
    {
        if(myUnit.GetCurrentMP() >= GetActionMPCost())
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
