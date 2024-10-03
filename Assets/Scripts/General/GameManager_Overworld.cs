using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_Overworld : Singleton<GameManager_Overworld>
{
    [SerializeField] private MyInputManager MyInputManager;

    public override void Awake()
    {
        base.Awake();
        GetReferences();
    }

    public MyInputManager GetInputManager()
    {
        if(MyInputManager == null)
        {
            GetReferences();
        }

        return MyInputManager;
    }

    private void GetReferences()
    {
        if(MyInputManager == null)
        {
            MyInputManager = GetComponentInChildren<MyInputManager>();
        }
    }
}
