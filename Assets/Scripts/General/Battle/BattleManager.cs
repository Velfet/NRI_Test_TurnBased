using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : Singleton<BattleManager>
{
    [SerializeField] private TurnSystem TurnSystem;
    [SerializeField] private EnemySpawner EnemySpawner;
    [SerializeField] private PlayerUnitBattlePlacer PlayerUnitBattlePlacer;
    [SerializeField] private BattleUnitManager BattleUnitManager;
    [SerializeField] private TargetSystem TargetSystem;
    [Space(20)]
    [SerializeField] private PlayerPartyPanel PlayerPartyPanel;
    [SerializeField] private EnemyPartyPanel EnemyPartyPanel;
    [SerializeField] private PlayerActionMenu PlayerActionMenu;
    [Space(20)]
    [SerializeField] private bool IsPlayerControlEnabled = true;

    private MyInputManager inputManager;

    public override void Awake()
    {
        base.Awake();
        GetReferences();
    }

    private void Start()
    {
        //spawn enemies, data is in game manager
        EnemySpawner.SpawnEnemies();
        //set player positions
        PlayerUnitBattlePlacer.PlacePlayerUnits();
        //fill panels with player and enemy units
        PlayerPartyPanel.SetupPanel();
        EnemyPartyPanel.SetupPanel();
        //turn system need to get references to all units in battle
        TurnSystem.GetUnitReferences();

        if (inputManager == null)
        {
            inputManager = MyGameManager.Instance.GetInputManager();
        }
        //disable overworld controls
        inputManager.Toggle_PlayerOverworldControls(false);
        //enable battle controls
        inputManager.Toggle_PlayerBattleControls(true);
        //sub back button here
        inputManager.SubscribeTo_Player_Battle_Action(MyEnum.Player_Battle_Actions_Cause.Back, PlayerActionMenu.GoBackToPreviousMenu, MyEnum.Player_Battle_Actions_Effect.GoBackToPreviousMenu);

    }

    private void OnDisable()
    {
        //unsubscribe back button
        inputManager.UnsubscribeTo_Player_Battle_Action(MyEnum.Player_Battle_Actions_Cause.Back, MyEnum.Player_Battle_Actions_Effect.GoBackToPreviousMenu);
    }

    public TurnSystem GetTurnSystem()
    {
        if(TurnSystem == null)
        {
            GetReferences();
        }

        return TurnSystem;
    }

    public EnemySpawner GetEnemySpawner()
    {
        if(EnemySpawner == null)
        {
            GetReferences();
        }

        return EnemySpawner;
    }

    public BattleUnitManager GetBattleUnitManager()
    {
        if(BattleUnitManager == null)
        {
            GetReferences();
        }

        return BattleUnitManager;
    }

    public PlayerUnitBattlePlacer GetPlayerUnitBattlePlacer()
    {
        if(PlayerUnitBattlePlacer == null)
        {
            GetReferences();
        }

        return PlayerUnitBattlePlacer;
    }

    public PlayerPartyPanel GetPlayerPartyPanel()
    {
        if(PlayerPartyPanel == null)
        {
            GetReferences();
        }

        return PlayerPartyPanel;
    }

    public EnemyPartyPanel GetEnemyPartyPanel()
    {
        if(EnemyPartyPanel == null)
        {
            GetReferences();
        }

        return EnemyPartyPanel;
    }

    public TargetSystem GetTargetSystem()
    {
        if(TargetSystem == null)
        {
            GetReferences();
        }

        return TargetSystem;
    }

    public PlayerActionMenu GetPlayerActionMenu()
    {
        if(PlayerActionMenu == null)
        {
            GetReferences();
        }

        return PlayerActionMenu;
    }


    private void GetReferences()
    {
        if(TurnSystem == null)
        {
            TurnSystem = GetComponentInChildren<TurnSystem>();
        }

        if(EnemySpawner == null)
        {
            EnemySpawner = GetComponentInChildren<EnemySpawner>();
        }

        if(BattleUnitManager == null)
        {
            BattleUnitManager = GetComponentInChildren<BattleUnitManager>();
        }

        if(PlayerUnitBattlePlacer == null)
        {
            PlayerUnitBattlePlacer = GetComponentInChildren<PlayerUnitBattlePlacer>();
        }

        if(PlayerPartyPanel == null)
        {
            PlayerPartyPanel = GetComponentInChildren<PlayerPartyPanel>();
        }

        if(EnemyPartyPanel == null)
        {
            EnemyPartyPanel = GetComponentInChildren<EnemyPartyPanel>();
        }

        if(TargetSystem == null)
        {
            TargetSystem = GetComponentInChildren<TargetSystem>();
        }

        if(PlayerActionMenu == null)
        {
            PlayerActionMenu = GetComponentInChildren<PlayerActionMenu>();
        }


    }

    public void SetPlayerControlStatus(bool newStatus)
    {
        IsPlayerControlEnabled = newStatus;
    }

    public bool GetPlayerControlStatus()
    {
        return IsPlayerControlEnabled;
    }
}
