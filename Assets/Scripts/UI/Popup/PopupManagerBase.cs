using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupManagerBase : MonoBehaviour
{

    protected bool hasStarted = false;
    protected MyInputManager myInputManager;
    protected PlayerInputActions PlayerInputs;
    protected void OnEnable()
    {
        if(hasStarted == true)
        {
            Start_Or_OnEnable();
        }
    }

    protected void Start()
    {
        hasStarted = true;
        Start_Or_OnEnable();
    }

    protected virtual void Start_Or_OnEnable()
    {
        GetReferences();
    }

    protected virtual void GetReferences()
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
