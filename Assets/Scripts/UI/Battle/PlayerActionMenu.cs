using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//acts as a manager of all menus of player actions
public class PlayerActionMenu : MonoBehaviour
{
    //keeps reference to currentplayer unit that is acting
    [SerializeField] private UnitPlayer CurrentPlayerUnit;
    [SerializeField] private Stack<IActionPanel> ActivePanelStacks = new Stack<IActionPanel>();
    [Space(20)]
    //keeps reference of all player action menu.
    [SerializeField] private BaseActionMenu BaseMenu;
    [SerializeField] private IActionPanel SkillMenu;    //need to change this type to something else, interface can't appear in the inspector

    public void SetCurrentPlayer(UnitPlayer newCurrentPlayer)
    {
        CurrentPlayerUnit = newCurrentPlayer;
    }


    //SetCurrentPlayer needs to run first
    public void ActivateMenu(MyEnum.PlayerPanelType panelType)
    {
        IActionPanel activatePanel = null;
        if(panelType == MyEnum.PlayerPanelType.BaseMenu)
        {
            activatePanel = BaseMenu;
        }
        else if(panelType == MyEnum.PlayerPanelType.SkillMenu)
        {
            activatePanel = SkillMenu;
        }
        else if(panelType == MyEnum.PlayerPanelType.Targetting)
        {
            //targetting doesn't actually have a panel for now, add a null panel for navigation purposes
            activatePanel = null;
        }

        //deactivate previous panel if it exists
        if(ActivePanelStacks.Count != 0)
        {
            IActionPanel previousPanel = ActivePanelStacks.Peek();
            previousPanel.DeactivatePanel();
        }

        //put the new panel onto the stack
        ActivePanelStacks.Push(activatePanel);
        //activate the new panel if it's not null
        if(activatePanel != null)
        {
            activatePanel.ActivatePanel(CurrentPlayerUnit);
        }
        
    }

    public void GoBackToPreviousMenu(InputAction.CallbackContext context)
    {
        GoBackToPreviousMenu();
    }

    public void GoBackToPreviousMenu()
    {
        if(ActivePanelStacks.Count < 2)
        {
            //don't allow to remove the last panel
            Debug.LogWarning("only one or zero panels are on the stack, can't go back");
            return;
        }

        //close current panel if it's not null
        IActionPanel currentPanel = ActivePanelStacks.Pop();
        if(currentPanel != null)
        {
            currentPanel.DeactivatePanel();
        }

        //activate previous panel
        if(ActivePanelStacks.Count == 0)
        {
            //no previous panel to go back to
            Debug.LogWarning("no previous panel to go back to");
            return;
        }

        IActionPanel previousPanel = ActivePanelStacks.Peek();
        previousPanel.ActivatePanel(CurrentPlayerUnit);
    }

    public void EmptyActivePanelStack()
    {
        if(ActivePanelStacks.Count > 0)
        {
            //make sure panel is deactivated, ensuring all buttons in that panel to be non-interactable
            IActionPanel currentPanel = ActivePanelStacks.Pop();
            if(currentPanel != null)
            {
                currentPanel.DeactivatePanel();
            }
        }

        //empty the active panel stack
        ActivePanelStacks.Clear();
    }
}
