using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UnitStats_", menuName = "ScriptableObjects/UnitStats")]
public class UnitStats_SO : ScriptableObject
{
    //name
    public string Name;
    //stats:
    //max health
    public float MaxHealth;
    //max mp
    public float MaxMP;
    //physical atk
    public float PhysAtk;
    //magic atk
    public float MagAtk;
    //physical def
    public float PhysDef;
    //magic def
    public float MagDef;
    //speed
    public float Spd;
}
