using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//used by enemies that picks their actions at random
public class UnitActionManager_Basic : MonoBehaviour
{
    [SerializeField] private List<UnitAction> CombatActions;

    public UnitAction GetRandomAction()
    {
        return CombatActions[Random.Range(0, CombatActions.Count)];
    }
}
