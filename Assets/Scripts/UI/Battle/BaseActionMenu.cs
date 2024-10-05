using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseActionMenu : MonoBehaviour, IActionPanel
{
    [SerializeField] private Transform PanelTransform;
    //keep reference of buttons of this menu
    [SerializeField] private CombatActionButton BasicAttackButton;
    //[todo] need to add skill button
    [SerializeField] private CombatActionButton DefendButton;
    [SerializeField] private CombatActionButton RunButton;

    private UnitPlayer myUnit;

    public void ActivatePanel(UnitPlayer currentUnit)
    {
        myUnit = currentUnit;
        Debug.Log("Current unit: " + myUnit.name);

        //get reference to action manager of current player unit
        UnitActionManager_Player unitActionManager = currentUnit.GetUnitActionManager();

        //set up the buttons for this menu, buttons should have functionality when clicked after this
        BasicAttackButton.SetCombatAction(unitActionManager.GetBasicAttack(), myUnit);
        DefendButton.SetCombatAction(unitActionManager.GetDefend(), myUnit);
        RunButton.SetCombatAction(unitActionManager.GetRun(), myUnit);

        //set buttons to be interactable
        BasicAttackButton.SetButtonInteractable(true);
        DefendButton.SetButtonInteractable(true);
        RunButton.SetButtonInteractable(true);

        //select basic attack button as selectable
        BasicAttackButton.SetButtonSelected();

        //set scale so the panel is visible
        PanelTransform.localScale = new Vector3(1,1,1);
    }

    public void DeactivatePanel()
    {
        //set buttons to not be interactable anymore
        BasicAttackButton.SetButtonInteractable(false);
        DefendButton.SetButtonInteractable(false);
        RunButton.SetButtonInteractable(false);

        //set scale so panel is not visible
        PanelTransform.localScale = new Vector3(0,0,0);
    }
}
