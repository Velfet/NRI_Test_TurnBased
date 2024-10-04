using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : Singleton<BattleManager>
{
    [SerializeField] private TurnSystem TurnSystem;
    [SerializeField] private EnemySpawner EnemySpawner;
    [SerializeField] private PlayerUnitBattlePlacer PlayerUnitBattlePlacer;
    [SerializeField] private BattleUnitManager BattleUnitManager;
    [Space(20)]
    [SerializeField] private PlayerPartyPanel PlayerPartyPanel;
    [SerializeField] private EnemyPartyPanel EnemyPartyPanel;

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


    }
}
