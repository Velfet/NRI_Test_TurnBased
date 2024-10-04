using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnitBattlePlacer : MonoBehaviour
{
    [SerializeField] private List<Vector3> PlayerUnitPositions;
    private List<GameObject> playerUnits_GO;

    public void PlacePlayerUnits()
    {
        //get reference of player
        PlayerManager player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();

        //hide player sprite
        player.Toggle_PlayerSprite(false);

        List<UnitPlayer> playerUnits = player.GetPlayerUnits();
        //sort the player units based on their UI priority
        playerUnits.Sort((a,b) => a.GetUI_Priority().CompareTo(b.GetUI_Priority()));

        
        for(int i = 0; i < playerUnits.Count; i++)
        {
            //add player unit to dictionary
            BattleManager.Instance.GetBattleUnitManager().AddPlayerUnit(playerUnits[i]);
            //place the player units
            playerUnits[i].gameObject.transform.SetPositionAndRotation(PlayerUnitPositions[i], Quaternion.identity);
            //set up the player units
            playerUnits[i].BattleSetUp();
        }

        
    }

}
