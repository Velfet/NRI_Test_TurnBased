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

    private void Awake()
    {
        PlayerInputs = new PlayerInputActions();
        Dictionary_OverworldAction = new Dictionary<MyEnum.Player_Overworld_Actions_Effect, Action<InputAction.CallbackContext>>();
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
        PlayerInputs.Overworld.Enable();
    }

    private void OnDisable()
    {
        //Disable all action maps
        PlayerInputs.Overworld.Disable();
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

    public PlayerInputActions GetPlayerInputs()
    {
        return PlayerInputs;
    }

}
