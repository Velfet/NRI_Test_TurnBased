using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitActionManager_Player : MonoBehaviour
{
    [SerializeField] private BasicAttack BasicAttack;
    [SerializeField] private Defend Defend;
    [SerializeField] private Run Run;
    [SerializeField] private List<UnitAction> Skills;


    public BasicAttack GetBasicAttack()
    {
        return BasicAttack;
    }

    public Defend GetDefend()
    {
        return Defend;
    }

    public Run GetRun()
    {
        return Run;
    }

    public List<UnitAction> GetUnitSkills()
    {
        return Skills;
    }
}
