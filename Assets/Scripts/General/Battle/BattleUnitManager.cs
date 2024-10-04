using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//keeps reference of all units in a battle
public class BattleUnitManager : MonoBehaviour
{
    private List<UnitBase> BattleUnits; //stores all units in battle
    private List<UnitBase> PlayerUnits;
    private List<UnitBase> EnemyUnits;

    public void AddEnemyUnit(UnitBase newEnemy)
    {
        if(EnemyUnits == null)
        {
            EnemyUnits = new List<UnitBase>();
        }

        EnemyUnits.Add(newEnemy);

        //also add the enemy to battle units
        AddUnit(newEnemy);
    }

    public void AddPlayerUnit(UnitBase newPlayer)
    {
        if(PlayerUnits == null)
        {
            PlayerUnits = new List<UnitBase>();
        }

        PlayerUnits.Add(newPlayer);

        //also add the Player to battle units
        AddUnit(newPlayer);
    }

    private void AddUnit(UnitBase newUnit)
    {
        if(BattleUnits == null)
        {
            BattleUnits = new List<UnitBase>();
        }

        BattleUnits.Add(newUnit);
    }


    public List<UnitBase> GetAllUnits()
    {
        return BattleUnits;
    }

    public List<UnitBase> GetPlayerUnits()
    {
        return PlayerUnits;
    }

    public List<UnitBase> GetEnemyUnits()
    {
        return EnemyUnits;
    }


    public List<UnitBase> GetEnemyUnits_Alive()
    {
        List<UnitBase> enemy_Alive = new List<UnitBase>();
        foreach(UnitBase enemy in EnemyUnits)
        {
            if(enemy.GetDefeatedStatus() == false)
            {
                enemy_Alive.Add(enemy);
            }
        }

        return enemy_Alive;
    }

    public List<UnitBase> GetEnemyUnits_Dead()
    {
        List<UnitBase> enemy_Dead = new List<UnitBase>();
        foreach(UnitBase enemy in EnemyUnits)
        {
            if(enemy.GetDefeatedStatus() == true)
            {
                enemy_Dead.Add(enemy);
            }
        }

        return enemy_Dead;
    }

    public List<UnitBase> GetPlayerUnits_Alive()
    {
        List<UnitBase> Player_Alive = new List<UnitBase>();
        foreach(UnitBase Player in PlayerUnits)
        {
            if(Player.GetDefeatedStatus() == false)
            {
                Player_Alive.Add(Player);
            }
        }

        return Player_Alive;
    }

    public List<UnitBase> GetPlayerUnits_Dead()
    {
        List<UnitBase> Player_Dead = new List<UnitBase>();
        foreach(UnitBase Player in PlayerUnits)
        {
            if(Player.GetDefeatedStatus() == true)
            {
                Player_Dead.Add(Player);
            }
        }

        return Player_Dead;
    }

}
