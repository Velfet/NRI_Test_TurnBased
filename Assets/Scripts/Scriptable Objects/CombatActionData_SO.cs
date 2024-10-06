using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CombatActionData_SO_", menuName = "ScriptableObjects/CombatActionData")]
public class CombatActionData_SO : ScriptableObject
{
    //name
    public string ActionName;
    //mp cost
    public float MP_Cost;
    //Animation clip name
    public string AnimClipName;
}
