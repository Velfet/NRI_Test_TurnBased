using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatActionButton : BaseCombatButton, IButton
{
    // need reference to the action itself and the unit the action belongs to
    [SerializeField] protected UnitAction CombatAction;
    [SerializeField] protected UnitBase CurrentUnit;

    protected BattleManager battleManager;
    protected TargetSystem targetSystem;

    public virtual void SetCombatAction(UnitAction theAction, UnitBase theUnit)
    {
        CombatAction = theAction;
        CurrentUnit = theUnit;

        //set button to call the CombatActionButtonPressed when pressed
        MyButton.onClick.RemoveAllListeners();
        MyButton.onClick.AddListener(CombatActionButtonPressed);
    }

    //should be called only AFTER SetCombatAction has been called
    public void CombatActionButtonPressed()
    {
        if(battleManager == null)
        {
            battleManager = BattleManager.Instance;
        }

        if(ButtonIsInteractable == false)
        {
            Debug.Log("Button isn't active, can't press button");
            return;
        }

        //check if player control is enabled or not
        if(battleManager.GetPlayerControlStatus() == false)
        {
            //player control isn't enabled, can't press the button
            Debug.Log("Can't press button, player control is disabled by battle manager");
            return;
        }

        //check if the action can be performed
        if(CombatAction.CanExecuteAction(CurrentUnit))
        {
            //action can be performed, store the selected action in the unit
            CurrentUnit.SetSelectedAction(CombatAction);

            //also, change the behaviour of 'back' button to cancelling target menu and re-open last menu
            //last menu could be base menu or skill menu, or some other menu, need to manage previously opened menu then
            

            //begin target selection depending on the action targetting type. Potential targets in the action has already been populated
            if(targetSystem == null)
            {
                targetSystem = BattleManager.Instance.GetTargetSystem();
            }
            targetSystem.BeginTargetting(this, CombatAction.GetPotentialTargets(), CombatAction.GetTargetType());
        }
    }

    //should be called by the targetting system when finalizing the target. Now, execute the action
    public void ReceiveFinalizedTargets(List<UnitBase> finalizedTargets)
    {
        //also unselect all targets, visually
        foreach(UnitBase unit in finalizedTargets)
        {
            unit.DeselectUnit();
        }

        CombatAction.FinalizeTarget(finalizedTargets);
        CombatAction.ActivateAction();

        //action has been performed, disable player's ability to make another action
        if(battleManager == null)
        {
            battleManager = BattleManager.Instance;
        }
        battleManager.SetPlayerControlStatus(false);

        
    }

}
