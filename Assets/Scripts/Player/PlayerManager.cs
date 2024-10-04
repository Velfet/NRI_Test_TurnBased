using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    //keep reference to player units
    [SerializeField] private List<UnitPlayer> UnitPlayers = new List<UnitPlayer>();
    [SerializeField] private SpriteRenderer PlayerSprite;

    //make sure there's only 1 instance of player
    private void Awake()
    {
        if(GameObject.FindGameObjectsWithTag("Player").Length > 1)
        {
            Destroy(this.gameObject);
        }
    }

    //make player persistent
    private void Start()
    {
        DontDestroyOnLoad(this);

        //set units hp and mp to max at start
        foreach(UnitPlayer player in UnitPlayers)
        {
            player.UpdateCurrentHP(player.GetMaxHP());
            player.UpdateCurrentMP(player.GetMaxMP());
        }
    }

    public List<UnitPlayer> GetPlayerUnits()
    {
        return UnitPlayers;
    }

    public void Toggle_PlayerSprite(bool newStatus)
    {
        PlayerSprite.enabled = newStatus;
    }
}
