using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Run : UnitAction
{
    public override void ActivateAction()
    {
        //request a target
        targetUnits[0] = myUnit;
        
        //get reference to all firendly units
        List<UnitBase> friendlyUnits = battleUnitManager.GetPlayerUnits_Alive();
        //remove selft from list
        friendlyUnits.Remove(myUnit);
        //set all friendly unit's selected action to null
        foreach(UnitBase unit in friendlyUnits)
        {
            unit.SetSelectedAction(null);
            //play animation for all friendly units
            unit.StartAnimation(CombatActionData.AnimClipName);
        }

        myUnit.StartAnimation(CombatActionData.AnimClipName);        
    }

    public override void ExecuteActionEffect()
    {
        //once animation finishes, run away
        //change scene to overworld
        SceneManager.LoadScene(MyEnum.SceneNames.Overworld.ToString());
        //don't update current encounter to being defeated
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

        
        //should always be able to defend
        return true;
    }
}
