using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PopupManager : MonoBehaviour
{
    [SerializeField] private PausePopup PausePopup;
    
    private bool hasStarted = false;
    private MyInputManager myInputManager;
    private PlayerInputActions PlayerInputs;

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
        GetReferences();
        //Subscribe to events
        myInputManager.SubscribeTo_Player_Overworld_Action(MyEnum.Player_Overworld_Actions_Cause.Back, Open_PausePopup, MyEnum.Player_Overworld_Actions_Effect.OpenPausePopup);
    }

    private void OnDisable()
    {
        //Unsubscribe from events
        if(PausePopup.GetPopupActive() == true)
        {
            myInputManager.UnsubscribeTo_Player_Overworld_Action(MyEnum.Player_Overworld_Actions_Cause.Back, MyEnum.Player_Overworld_Actions_Effect.ClosePausePopup);
        }
        else
        {
            myInputManager.UnsubscribeTo_Player_Overworld_Action(MyEnum.Player_Overworld_Actions_Cause.Back, MyEnum.Player_Overworld_Actions_Effect.OpenPausePopup);
        }
        
    }

    public void Open_PausePopup(InputAction.CallbackContext context)
    {
        Open_PausePopup();
    }

    public void Open_PausePopup()
    {
        //Back button now no longer opens the pause popup, instead it will exit the pause popup
        myInputManager.UnsubscribeTo_Player_Overworld_Action(MyEnum.Player_Overworld_Actions_Cause.Back, MyEnum.Player_Overworld_Actions_Effect.OpenPausePopup);
        myInputManager.SubscribeTo_Player_Overworld_Action(MyEnum.Player_Overworld_Actions_Cause.Back, Close_PausePopup, MyEnum.Player_Overworld_Actions_Effect.ClosePausePopup);

        Stop_TimeAndPlayerControls();

        PausePopup.ActivatePopup();
    }
    
    public void Close_PausePopup(InputAction.CallbackContext context)
    {
        Close_PausePopup();
    }

    public void Close_PausePopup()
    {
        //Back button now no longer exit the pause popup, instead it goes back to opening up the pause popup
        myInputManager.UnsubscribeTo_Player_Overworld_Action(MyEnum.Player_Overworld_Actions_Cause.Back, MyEnum.Player_Overworld_Actions_Effect.ClosePausePopup);
        myInputManager.SubscribeTo_Player_Overworld_Action(MyEnum.Player_Overworld_Actions_Cause.Back, Open_PausePopup, MyEnum.Player_Overworld_Actions_Effect.OpenPausePopup);

        Resume_TimeAndPlayerControls();

        PausePopup.DeactivatePopup();
    }

    public void Stop_TimeAndPlayerControls()
    {
        //Pause timer
        Time.timeScale = 0;
    }

    public void Resume_TimeAndPlayerControls()
    {
        //Continue timer
        Time.timeScale = 1;
    }

    private void GetReferences()
    {
        if(myInputManager == null)
        {
            myInputManager = MyGameManager.Instance.GetInputManager();
        }

        if(PlayerInputs == null)
        {
            PlayerInputs = myInputManager.GetPlayerInputs();
        }
    }
}
