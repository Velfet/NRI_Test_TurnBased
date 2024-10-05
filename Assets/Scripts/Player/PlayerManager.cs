using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    //keep reference to player units
    [SerializeField] private List<UnitPlayer> UnitPlayers = new List<UnitPlayer>();
    [SerializeField] private SpriteRenderer PlayerSprite;
    [SerializeField] private PlayerMovement PlayerOverworld;
    [SerializeField] private string PreviousScene = "";

    //make sure there's only 1 instance of player
    private void Awake()
    {
        if(GameObject.FindGameObjectsWithTag("Player").Length > 1)
        {
            Destroy(gameObject);
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

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public List<UnitPlayer> GetPlayerUnits()
    {
        return UnitPlayers;
    }

    public void Toggle_PlayerSprite(bool newStatus)
    {
        PlayerSprite.gameObject.SetActive(newStatus);
    }

    public PlayerMovement GetPlayerOverworld()
    {
        return PlayerOverworld;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.name == MyEnum.SceneNames.Overworld.ToString() && PreviousScene != "")
        {
            //enable player overworld sprite
            Toggle_PlayerSprite(true);
        }

        //if previous scene was battle, begin IFrame
        if(PreviousScene == MyEnum.SceneNames.Battle.ToString())
        {
            PlayerOverworld.Start_RunAwayIFrame();
        }

        //overwrite previous scene name
        PreviousScene = scene.name;
    }
}
