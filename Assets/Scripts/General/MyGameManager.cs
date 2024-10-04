using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGameManager : SingletonPersistent<MyGameManager>
{
    [SerializeField] private MyInputManager MyInputManager;
    [SerializeField] private OverworldManager OverworldManager;

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

    public OverworldManager GetOverworldManager()
    {
        return OverworldManager;
    }

    private void GetReferences()
    {
        if(MyInputManager == null)
        {
            MyInputManager = GetComponentInChildren<MyInputManager>();
        }

        if(OverworldManager == null)
        {
            OverworldManager = GetComponentInChildren<OverworldManager>();
        }
    }
}
