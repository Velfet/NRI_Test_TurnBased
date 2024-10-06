using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//unlike CombatActionButton, this button doesn't activate an action, it opens another menu
public class CombatMenuButton : BaseCombatButton, IButton
{
    //need reference to menu that is going to be opened
    [SerializeField] private MyEnum.PlayerPanelType ActionMenuType;

    private PlayerActionMenu playerActionMenu;

    public void SetMenuButton(MyEnum.PlayerPanelType theMenu)
    {
        ActionMenuType = theMenu;

        //set button to call the MenuButtonPressed when pressed
        MyButton.onClick.RemoveAllListeners();
        MyButton.onClick.AddListener(MenuButtonPressed);
    }

    public void MenuButtonPressed()
    {
        if(ButtonIsInteractable == false)
        {
            Debug.Log("Button isn't active, can't press button");
            return;
        }
        
        if(playerActionMenu == null)
        {
            playerActionMenu = BattleManager.Instance.GetPlayerActionMenu();
        }
        //tell the player action menu to open the menu
        playerActionMenu.ActivateMenu(ActionMenuType);
    }
    
}
