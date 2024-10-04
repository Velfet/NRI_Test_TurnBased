using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOrderUI : MonoBehaviour
{
    
    [SerializeField] private GameObject TurnOrder_Icon_Prefab;

    private List<TurnOrder_Icon> turnOrder_Icons = new List<TurnOrder_Icon>(); 
    
    //receives a list of units that is sorted basde on their wait time
    public void SetUpTurnOrderUI(List<UnitBase> sortedUnits)
    {
        //make a turn order icon for each unit
        for(int i = sortedUnits.Count-1; i >= 0; i--)
        {
            UnitBase currentUnit = sortedUnits[i];
            TurnOrder_Icon turnOrder_Icon = Instantiate(TurnOrder_Icon_Prefab, this.transform).GetComponent<TurnOrder_Icon>();

            turnOrder_Icon.UpdateIcon(currentUnit.GetUnitIconData());
            turnOrder_Icons.Add(turnOrder_Icon);
        }
    }

    //receives a list of units that is sorted basde on their wait time
    public void UpdateTurnOrderUI(List<UnitBase> sortedUnits)
    {
        //update each unit icon
        for(int i = sortedUnits.Count-1; i >= 0; i--)
        {
            UnitBase currentUnit = sortedUnits[i];
            turnOrder_Icons[sortedUnits.Count-1-i].UpdateIcon(currentUnit.GetUnitIconData());
        }
    }

    //return the status of whether the turn order UI has been setup before or not
    public bool HasSetUp()
    {
        if(turnOrder_Icons.Count > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
