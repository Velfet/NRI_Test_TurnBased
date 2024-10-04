using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OverworldManager : MonoBehaviour
{
    //store position of player before going to battle
    [SerializeField] private Vector3 LastPosition;
    //keep track of which enemy we encountered in the overworld, so we know what to spawn in the battle
    [SerializeField] private List<GameObject> EnemyEncounter_Data;
    //make dictionary to keep track of which enemy encounter has been defeated. bool is true if enemy has been defeated
    [SerializeField] private Dictionary<int, bool> EnemyEncounterStatuses = new Dictionary<int, bool>();
    //keep track of current enemy encounter that we're fighting
    [SerializeField] private int CurrentEnemyEncounterIndex;

    public void UpdateCurrentEnemyEncounterData(List<GameObject> enemyData, int enemyEncounterIndex)
    {
        EnemyEncounter_Data = enemyData;
        CurrentEnemyEncounterIndex = enemyEncounterIndex;

        //add to dictionary if not yet encountered
        if(EnemyEncounterStatuses.ContainsKey(enemyEncounterIndex) == false)
        {
            EnemyEncounterStatuses.Add(enemyEncounterIndex, false);
        }
        else
        {
            EnemyEncounterStatuses[enemyEncounterIndex] = false;
        }
        
    }

    public void GoToBattleScene()
    {
        SceneManager.LoadScene("Battle");
    }

    //returns which enemy to spawn in battle
    public List<GameObject> GetEnemyEncounterData()
    {
        if (EnemyEncounter_Data.Count == 0)
        {
            Debug.LogWarning("no enemies");
            return null;
        }
        
        return EnemyEncounter_Data;
    }

    public void UpdatePlayerPosition(Vector3 newPos)
    {
        LastPosition = newPos;
    }

    public Vector3 GetPlayerLastPosition()
    {
        return LastPosition;
    }

    public Dictionary<int, bool> GetEnemyEncounterStatuses()
    {
        return EnemyEncounterStatuses;
    }

    public void UpdateDefeatedStatusOfEnemyEncounter(bool newStatus)
    {
        EnemyEncounterStatuses[CurrentEnemyEncounterIndex] = newStatus;
    }
}
