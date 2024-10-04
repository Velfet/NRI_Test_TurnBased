using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject EnemyParent_GO;
    [SerializeField] private List<Vector3> EnemyPositions;

    private OverworldManager overworldManager;
    private List<GameObject> enemyData;
    

    public void SpawnEnemies()
    {
        //get enemy prefabs from singleton
        if(overworldManager == null)
        {
            overworldManager = MyGameManager.Instance.GetOverworldManager();
        }

        enemyData = overworldManager.GetEnemyEncounterData();
        
        //spawn the enemies
        for(int i = 0; i < enemyData.Count; i++)
        {
            UnitBase enemy = Instantiate(enemyData[i], EnemyPositions[i], Quaternion.identity, EnemyParent_GO.transform).GetComponent<UnitBase>();
            enemy.BattleSetUp();
            //add enemy units to battle unit manager
            BattleManager.Instance.GetBattleUnitManager().AddEnemyUnit(enemy.GetComponent<UnitBase>());
        }
        
        
    }
}
