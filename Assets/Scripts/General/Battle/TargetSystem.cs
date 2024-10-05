using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSystem : MonoBehaviour
{
    //keeps reference to Combat Action Button that requested the target(s)
    [SerializeField] private CombatActionButton Client_CombatActionButton;
    [SerializeField] private List<UnitBase> currentTargets = new List<UnitBase>();  //can be one target or multiple in the list
    [SerializeField] private List<UnitBase> PotentialTargets = new List<UnitBase>();
    [SerializeField] private bool IsNavigating = false;
    [Space(20)]
    [SerializeField] private int CurrentTargetIndex = -1;   //if multi-target, index is -1

    private PlayerActionMenu playerActionMenu;
    private PlayerInputActions playerInputs;

    public void BeginTargetting(CombatActionButton newClient, List<UnitBase> potentialTargets, MyEnum.ActionTargetType targetType)
    {
        PotentialTargets = potentialTargets;
        //clear list
        currentTargets = new List<UnitBase>();
        //set current target index to -1
        CurrentTargetIndex = -1;

        Client_CombatActionButton = newClient;

        //if target type is single target, select single member in list (self, enemy, party member)
        if(targetType == MyEnum.ActionTargetType.Self || targetType == MyEnum.ActionTargetType.Enemy || targetType == MyEnum.ActionTargetType.PartyMember)
        {
            //default, select the first unit on the list
            currentTargets.Add(potentialTargets[0]);
            //activate select visual
            currentTargets[0].SelectUnit();
            //set current target index to 0
            CurrentTargetIndex = 0;
        }

        //if target type is multiple target, select all in list (all enemies, all party members, all units, all units except self)
        if(targetType == MyEnum.ActionTargetType.AllEnemies || targetType == MyEnum.ActionTargetType.AllPartyMembers ||
        targetType == MyEnum.ActionTargetType.AllUnits || targetType == MyEnum.ActionTargetType.AllUnits_ExceptSelf)
        {
            //default, select every target on the list
            currentTargets.AddRange(potentialTargets);
            //activate select visual
            foreach(UnitBase unit in currentTargets)
            {
                unit.SelectUnit();
            }
        }

        if(playerActionMenu == null)
        {
            playerActionMenu = BattleManager.Instance.GetPlayerActionMenu();
        }
        //contact PlayerActionMenu and add an empty panel onto its stack for targetting
        playerActionMenu.ActivateMenu(MyEnum.PlayerPanelType.Targetting);

        //get input data from player input
        if(playerInputs == null)
        {
            playerInputs = MyGameManager.Instance.GetInputManager().GetPlayerInputs();
        }

        //reset input
        playerInputs.Battle.Back.Reset();
        playerInputs.Battle.Confirm.Reset();
        playerInputs.Battle.ToggleTarget.Reset();

        //might want to begin navigation in update, use bool to toggle whether we are targetting or not
        IsNavigating = true;
    }

    private void Update()
    {
        if(IsNavigating)
        {
            //we're navigating and the action requires only 1 target
            //detect input for toggling between target

            if(CurrentTargetIndex != -1 && playerInputs.Battle.ToggleTarget.triggered)
            {
                float toggleValue = playerInputs.Battle.ToggleTarget.ReadValue<float>();
                if(toggleValue > 0)
                {
                    //deselect previous unit
                    PotentialTargets[CurrentTargetIndex].DeselectUnit();

                    //increase index, loop around if needed
                    CurrentTargetIndex += 1;
                    if(CurrentTargetIndex >= PotentialTargets.Count)
                    {
                        CurrentTargetIndex = 0;
                    }
                    //select the unit
                    currentTargets[0] = PotentialTargets[CurrentTargetIndex];
                    //activate select visual
                    PotentialTargets[CurrentTargetIndex].SelectUnit();
                    
                }
                else if(toggleValue < 0)
                {
                    //deselect previous unit
                    PotentialTargets[CurrentTargetIndex].DeselectUnit();

                    //decrease index, loop around if needed
                    CurrentTargetIndex -= 1;
                    if(CurrentTargetIndex < 0)
                    {
                        CurrentTargetIndex = PotentialTargets.Count-1;
                    }
                    //select the unit
                    currentTargets[0] = PotentialTargets[CurrentTargetIndex];
                    //activate select visual
                    PotentialTargets[CurrentTargetIndex].SelectUnit();
                }
            }
            

            //detect input to go ahead with the target selection
            if(playerInputs.Battle.Confirm.triggered)
            {
                Debug.LogWarning("Confirm the action");
                //no longer navigating
                IsNavigating = false;
                //contact player action menu to clear out the active panel stack
                playerActionMenu.EmptyActivePanelStack();
                //contact button to execute action, which will end the turn
                Client_CombatActionButton.ReceiveFinalizedTargets(currentTargets);

                //deseletc all selected units
                //aaa
            }

            //detect input to cancel target selection, need to contact PlayerActionMenu to go back to previous panel
            if(playerInputs.Battle.Back.triggered)
            {
                Debug.LogWarning("Back out from targetting");
                //no longer navigating
                IsNavigating = false;
                //deactivate the selected units
                foreach(UnitBase unit in currentTargets)
                {
                    unit.DeselectUnit();
                }
            }
        }
    }




}
