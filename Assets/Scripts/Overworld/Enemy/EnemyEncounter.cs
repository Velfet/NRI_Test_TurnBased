using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEncounter : MonoBehaviour
{
    [SerializeField] private EnemyEncounterManager EnemyEncounterManager;
    //keep track of what enemies to spawn
    [SerializeField] private List<GameObject> EnemyEncounter_GO;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.GetComponent<PlayerMovement>())
        {
            Debug.Log("player touch enemy encounter, commence battle shortly");
            //save position of player
            EnemyEncounterManager.SavePlayerPos(other.transform.position);
            //send data about which enemies to spawn during battle, also reference to self
            EnemyEncounterManager.StartEnemyEncounter(this.gameObject, EnemyEncounter_GO);
        }
    }


}
