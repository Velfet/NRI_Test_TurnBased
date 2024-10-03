using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBuffs : MonoBehaviour
{
    //buffs increases the potency of actions that rely on certain stats. 0.1 buff means that the stats will be x1.1 when applied to actions

    //max health buff
    public float Health_Buff = 0f;
    //max mp buff
    public float MP_Buff = 0f;
    //physical atk buff
    public float PhysAtk_Buff = 0f;
    //magic atk buff
    public float MagAtk_Buff = 0f;
    //physical def buff
    public float PhysDef_Buff = 0f;
    //magic def buff
    public float MagDef_Buff = 0f;
    //speed buff
    public float Spd_Buff = 0f;

    public void ResetBuffs()
    {
        Health_Buff = 0f;
        MP_Buff = 0f;
        PhysAtk_Buff = 0f;
        MagAtk_Buff = 0f;
        PhysDef_Buff = 0f;
        MagDef_Buff = 0f;
        Spd_Buff = 0f;
    }

    public void Set_Health_Buff(float value)
    {
        Health_Buff = value;
    }

    public void Set_MP_Buff(float value)
    {
        MP_Buff = value;
    }

    public void Set_PhysAtk_Buff(float value)
    {
        PhysAtk_Buff = value;
    }

    public void Set_MagAtk_Buff(float value)
    {
        MagAtk_Buff = value;
    }

    public void Set_PhysDef_Buff(float value)
    {
        PhysDef_Buff = value;
    }

    public void Set_MagDef_Buff(float value)
    {
        MagDef_Buff = value;
    }

    public void Set_Spd_Buff(float value)
    {
        Spd_Buff = value;
    }


}
