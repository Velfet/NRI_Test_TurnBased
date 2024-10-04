using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitEnemy : UnitBase
{
    [SerializeField] private UnitActionManager_Basic UnitActionManager;

    private BattleUnitManager battleUnitManager;

    public override void BattleSetUp()
    {
        UnitType = MyEnum.UnitType.Enemy;
        base.BattleSetUp();
    }

    public override void BeginAct()
    {
        bool hasActed = false;

        while(hasActed == false)
        {
            //choose action
            UnitAction theAction = UnitActionManager.GetRandomAction();
            Debug.Log("Action: " + theAction.GetActionName());
            //check if action can be performed
            if(theAction.CanExecuteAction(this))
            {
                Debug.Log("Action: " + theAction.GetActionName() + " can be performed");
                //store selected action
                SelectedAction = theAction;

                //select target or targets depending on action type
                List<UnitBase> selectedUnits = GetSelectedTargets(SelectedAction.GetTargetType(), SelectedAction.GetPotentialTargets());
                Debug.Log("selected unit 0: " + selectedUnits[0]);
                //finalize selected target of action
                SelectedAction.FinalizeTarget(selectedUnits);

                //if yes, perform action
                theAction.ActivateAction();

                hasActed = true;
            }
        }
        
    }

    //enemy exclusive function, so we can always assume that the unity type is 'enemy'
    public List<UnitBase> GetSelectedTargets(MyEnum.ActionTargetType targetType, List<UnitBase> potentialTargets)
    {
        if(battleUnitManager == null)
        {
            battleUnitManager = BattleManager.Instance.GetBattleUnitManager();
        }

        List<UnitBase> selectedUnits = new List<UnitBase>();

        if(targetType == MyEnum.ActionTargetType.Self)
        {
            selectedUnits.Add(this);
        }
        else if(targetType == MyEnum.ActionTargetType.Enemy)
        {
            //select enemy of enemies, aka player
            List<UnitBase> playerUnits = battleUnitManager.GetPlayerUnits_Alive();
            UnitBase randomUnit = playerUnits[UnityEngine.Random.Range(0, playerUnits.Count)];
            selectedUnits.Add(randomUnit);
        }
        else if(targetType == MyEnum.ActionTargetType.PartyMember)
        {
            //select party member of enemies, aka enemies
            List<UnitBase> enemyUnits = battleUnitManager.GetEnemyUnits_Alive();
            UnitBase randomUnit = enemyUnits[UnityEngine.Random.Range(0, enemyUnits.Count)];
            selectedUnits.Add(randomUnit);
        }
        else if(targetType == MyEnum.ActionTargetType.AllEnemies)
        {
            //select all enemies of the enemy, aka all players
            selectedUnits = battleUnitManager.GetPlayerUnits_Alive();
        }
        else if(targetType == MyEnum.ActionTargetType.AllPartyMembers)
        {
            //select all party members of enemies, aka all enemies
            selectedUnits = battleUnitManager.GetEnemyUnits_Alive();
        }
        else if(targetType == MyEnum.ActionTargetType.AllUnits)
        {
            //select all units that are still alive
            selectedUnits.AddRange(battleUnitManager.GetPlayerUnits_Alive());
            selectedUnits.AddRange(battleUnitManager.GetEnemyUnits_Alive());
        }
        else if(targetType == MyEnum.ActionTargetType.AllUnits_ExceptSelf)
        {
            //select all units that are still alive, except self
            selectedUnits.AddRange(battleUnitManager.GetPlayerUnits_Alive());
            selectedUnits.AddRange(battleUnitManager.GetEnemyUnits_Alive());

            selectedUnits.Remove(this);
        }

        return selectedUnits;
    }

    // public override void UpdateCurrentHP(float alterAmount)
    // {
    //     base.UpdateCurrentHP(alterAmount);

    //     //call hp update event
    //     hpChangeEvent.Invoke(this);
    // }

    // public override void UpdateCurrentMP(float alterAmount)
    // {
    //     base.UpdateCurrentMP(alterAmount);

    //     //call mp update event
    //     mpChangeEvent.Invoke(this);
    // }

}
