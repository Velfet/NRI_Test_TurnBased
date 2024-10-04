using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnSystem : MonoBehaviour
{
    [SerializeField] private TurnOrderUI TurnOrderUI;
    [Space(20)]
    [SerializeField] private List<UnitBase> Units = new List<UnitBase>();
    [SerializeField] private List<UnitBase> PartyMembers = new List<UnitBase>();
    [SerializeField] private List<UnitBase> Enemies = new List<UnitBase>();
    [Space(20)]
    [SerializeField] private List<UnitBase> ActiveUnits = new List<UnitBase>(); //list of units that can still act
    [SerializeField] private List<UnitBase> WaitTimeSorted_Units = new List<UnitBase>();    //units that is sorted based on their current wait time
    //[SerializeField] private Dictionary<UnitBase, float> UnitWaitTime = new Dictionary<UnitBase, float>();  //used to keep track of which unit will act first, second, etc

    private BattleUnitManager battleUnitManager;

    public void GetUnitReferences()
    {
        battleUnitManager = BattleManager.Instance.GetBattleUnitManager();
        //get party members
        PartyMembers = battleUnitManager.GetPlayerUnits();
        //get enemies
        Enemies = battleUnitManager.GetEnemyUnits();

        //put all non-defeated units in the active unit list
        foreach (UnitBase unit in PartyMembers)
        {
            if(unit.GetDefeatedStatus() == false)
            {
                ActiveUnits.Add(unit);
            }
        }
        foreach (UnitBase unit in Enemies)
        {
            if(unit.GetDefeatedStatus() == false)
            {
                ActiveUnits.Add(unit);
            }
        }

        Units.AddRange(PartyMembers);
        Units.AddRange(Enemies);

        //set up wait time for each active unit
        foreach(UnitBase unit in ActiveUnits)
        {
            unit.SetCurrentWaitTime(1000f/unit.GetFinalSpd());
        }

        //populate the wait-time sorted list of units
        foreach(UnitBase unit in Units)
        {
            //set defeated units to the last of the turn order
            if(unit.GetDefeatedStatus() == true)
            {
                unit.SetCurrentWaitTime(1000f);
            }
            
            WaitTimeSorted_Units.Add(unit);
        }

        //begin process of finding which unit to act next
        TurnProcess();

    }

    public void TurnProcess()
    {
        //find unit that will act next, that is the one with the lowest wait time
        UnitBase nextUnit = GetNextUnit();
        float lowestWaitTime = nextUnit.GetCurrentWaitTime();

        //substract every active unit's wait time by the wait time of the next unit
        for(int i = 0; i < WaitTimeSorted_Units.Count; i++)
        {
            if(WaitTimeSorted_Units[i].GetDefeatedStatus() == false)
            {
                //only 'move forward' the wait time of non-defeated units
                WaitTimeSorted_Units[i].AlterCurrentWaitTime(-lowestWaitTime);
            }
        }

        //reconstruct turn order UI, use the wait-time sorted unit list
        if(TurnOrderUI.HasSetUp())
        {
            //has been setup, update
            Debug.Log("update");
            TurnOrderUI.UpdateTurnOrderUI(WaitTimeSorted_Units);
        }
        else
        {
            //hasn't been setup, setup first
            Debug.Log("setup");
            TurnOrderUI.SetUpTurnOrderUI(WaitTimeSorted_Units);
        }

        //let the next unit act
        Debug.Log(nextUnit + " will act next");
        nextUnit.BeginAct();
        
    }

    private UnitBase GetNextUnit()
    {
        //re-sort the wait time sorted list
        WaitTimeSorted_Units.Sort((a,b) => a.GetCurrentWaitTime().CompareTo(b.GetCurrentWaitTime()));

        //get unit with lowest wait time, should be the 1st entry in the sorted list
        UnitBase nextUnit = WaitTimeSorted_Units[0];



        return nextUnit;
    }

    public void EndTurnOfUnit(UnitBase theUnit)
    {
        //refresh wait time of the unit that has just ended its turn
        theUnit.SetCurrentWaitTime(1000f/theUnit.GetFinalSpd());
        //begin a new turn
        TurnProcess();
    }

    public void ReceiveUnitIsDeadReport(UnitBase theUnit)
    {
        //unit is dead, set its wait time to 1000f and remove it from active unit list
        theUnit.SetCurrentWaitTime(1000f);
        ActiveUnits.Remove(theUnit);

        //also might want to check if the game is over if enemy wins, or battle is over if player wins
        if(battleUnitManager.GetEnemyUnits_Alive().Count == 0)
        {
            //player wins
            Debug.Log("Player wins");
        }
        else if(battleUnitManager.GetPlayerUnits_Alive().Count == 0)
        {
            //enemy wins
            Debug.Log("Enemy wins");
        }
    }

}
