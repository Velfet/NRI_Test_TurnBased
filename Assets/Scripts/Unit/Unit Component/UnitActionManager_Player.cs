using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitActionManager_Player : MonoBehaviour
{
    [SerializeField] private BasicAttack BasicAttack;
    [SerializeField] private Defend Defend;
    //skills
    //run


    public BasicAttack GetBasicAttack()
    {
        return BasicAttack;
    }

    public Defend GetDefend()
    {
        return Defend;
    }
}
