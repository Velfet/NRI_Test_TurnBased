using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MyInputManager : MonoBehaviour
{

    private PlayerInputActions PlayerInputs;
    private bool hasStarted = false;    //True when "Start" has occurred and false when "Start" has not yet occurred
    private Dictionary<MyEnum.Player_Overworld_Actions_Effect, Action<InputAction.CallbackContext>> Dictionary_OverworldAction;
    private Dictionary<MyEnum.Player_Battle_Actions_Effect, Action<InputAction.CallbackContext>> Dictionary_BattleAction;

    private void Awake()
    {
        PlayerInputs = new PlayerInputActions();
        Dictionary_OverworldAction = new Dictionary<MyEnum.Player_Overworld_Actions_Effect, Action<InputAction.CallbackContext>>();
        Dictionary_BattleAction = new Dictionary<MyEnum.Player_Battle_Actions_Effect, Action<InputAction.CallbackContext>>();
    }

    private void OnEnable()
    {
        if(hasStarted == true)
        {
            Start_Or_OnEnable();
        }
    }

    private void Start()
    {
        hasStarted = true;
        Start_Or_OnEnable();
    }

    private void Start_Or_OnEnable()
    {
        //Enable certain default action maps
        Toggle_PlayerOverworldControls(true);
    }

    private void OnDisable()
    {
        //Disable all action maps
        Toggle_PlayerOverworldControls(false);
        Toggle_PlayerBattleControls(false);
    }

    public void SubscribeTo_Player_Overworld_Action(MyEnum.Player_Overworld_Actions_Cause theAction, Action<InputAction.CallbackContext> theSubscribedAction, MyEnum.Player_Overworld_Actions_Effect theSubscribedActionID)
    {
        if(theAction == MyEnum.Player_Overworld_Actions_Cause.Back)
        {
            //bind effect to cause
            PlayerInputs.Overworld.Back.performed += theSubscribedAction;
            //record the binding in the action dictionary
            Dictionary_OverworldAction.Add(theSubscribedActionID, theSubscribedAction);
        }
    }

    public void UnsubscribeTo_Player_Overworld_Action(MyEnum.Player_Overworld_Actions_Cause theAction, MyEnum.Player_Overworld_Actions_Effect theSubscribedActionID)
    {
        if(theAction == MyEnum.Player_Overworld_Actions_Cause.Back)
        {
            if(Dictionary_OverworldAction.ContainsKey(theSubscribedActionID) == false)
            {
                Debug.LogWarning("Failed to remove " + theSubscribedActionID + " because it doesn't exist in the action dictionary");
                return;
            }
            //unbind an effect from a cause
            PlayerInputs.Overworld.Back.performed -= Dictionary_OverworldAction[theSubscribedActionID];
            //record the unbinding in the action dictionary
            Dictionary_OverworldAction.Remove(theSubscribedActionID);
        }
    }

    public void SubscribeTo_Player_Battle_Action(MyEnum.Player_Battle_Actions_Cause theCause, Action<InputAction.CallbackContext> theEffect, MyEnum.Player_Battle_Actions_Effect theEffectID)
    {
        if(theCause == MyEnum.Player_Battle_Actions_Cause.Back)
        {
            //bind effect to cause
            PlayerInputs.Battle.Back.performed += theEffect;
            //record the binding in the action dictionary
            Dictionary_BattleAction.Add(theEffectID, theEffect);
        }
        else if(theCause == MyEnum.Player_Battle_Actions_Cause.Confirm)
        {
            //bind effect to cause
            PlayerInputs.Battle.Confirm.performed += theEffect;
            //record the binding in the action dictionary
            Dictionary_BattleAction.Add(theEffectID, theEffect);
        }
    }

    public void UnsubscribeTo_Player_Battle_Action(MyEnum.Player_Battle_Actions_Cause theCause, MyEnum.Player_Battle_Actions_Effect theEffectID)
    {
        //check if dictionary entry exists
        if(Dictionary_BattleAction.ContainsKey(theEffectID) == false)
        {
            Debug.LogWarning("Failed to remove " + theEffectID + " because it doesn't exist in the action dictionary");
            return;
        }

        if(theCause == MyEnum.Player_Battle_Actions_Cause.Back)
        {
            //unbind effect from cause
            PlayerInputs.Battle.Back.performed -= Dictionary_BattleAction[theEffectID];
            //record unbinding in the action dictionary
            Dictionary_BattleAction.Remove(theEffectID);
        }
        else if(theCause == MyEnum.Player_Battle_Actions_Cause.Confirm)
        {
            //unbind effect from cause
            PlayerInputs.Battle.Confirm.performed -= Dictionary_BattleAction[theEffectID];
            //record unbinding in the action dictionary
            Dictionary_BattleAction.Remove(theEffectID);
        }
    }

    public PlayerInputActions GetPlayerInputs()
    {
        return PlayerInputs;
    }

    public void Toggle_PlayerOverworldControls(bool newStatus)
    {
        if(newStatus == true)
        {
            PlayerInputs.Overworld.Enable();
        }
        else
        {
            PlayerInputs.Overworld.Enable();
        }
        
    }

    public void Toggle_PlayerBattleControls(bool newState)
    {
        if(newState == true)
        {
            PlayerInputs.Battle.Enable();
        }
        else
        {
            PlayerInputs.Battle.Disable();
        }
    }

}
