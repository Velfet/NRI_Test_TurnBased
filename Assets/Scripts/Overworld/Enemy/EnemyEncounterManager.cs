using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEncounterManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> EnemyEncounters_GO;

    private OverworldManager overworldManager;

    private void Start()
    {
        //check singleton, deactivate any enemy encounter that has been defeated
        overworldManager = MyGameManager.Instance.GetOverworldManager();

        Dictionary<int, bool> encounterStatuses = overworldManager.GetEnemyEncounterStatuses();

        foreach(GameObject singleEnemyEncounter in EnemyEncounters_GO)
        {
            //get index
            int indexGO = EnemyEncounters_GO.IndexOf(singleEnemyEncounter);

            if(encounterStatuses.ContainsKey(indexGO))
            {
                //enemy encounter NOT yet been defeated
                if(encounterStatuses[indexGO] == false)
                {
                    SetEnemyEncounterActive(singleEnemyEncounter);
                }
            }
            else
            {
                //enemy encounter not in dictionary, NOT yet been defeated
                SetEnemyEncounterActive(singleEnemyEncounter);
            }
        }
    }

    public void SetEnemyEncounterActive(GameObject theEnemyEncounter)
    {
        if(EnemyEncounters_GO.Contains(theEnemyEncounter))
        {
            theEnemyEncounter.SetActive(true);
        }
        else
        {
            Debug.LogWarning("No enemy encounter in list");
        }
    }

    public void StartEnemyEncounter(GameObject theEnemyEncounter, List<GameObject> enemyEncounterData)
    {
        overworldManager = MyGameManager.Instance.GetOverworldManager();

        overworldManager.UpdateCurrentEnemyEncounterData(enemyEncounterData, EnemyEncounters_GO.IndexOf(theEnemyEncounter));
        overworldManager.GoToBattleScene();
    }

    public void SavePlayerPos(Vector3 currentPlayerPos)
    {
        if(overworldManager == null)
        {
            overworldManager = MyGameManager.Instance.GetOverworldManager();
        }

        overworldManager.UpdatePlayerPosition(currentPlayerPos);
    }
}
