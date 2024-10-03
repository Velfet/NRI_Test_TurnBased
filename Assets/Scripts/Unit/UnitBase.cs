using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBase : MonoBehaviour
{
    [SerializeField] protected MyEnum.UnitType UnitType;
    [SerializeField] protected UnitStats_SO Stats;
    [SerializeField] protected UnitBuffs Buffs;
    [Space(20)]
    [SerializeField] protected Animator Animator;
    [Space(20)]
    [SerializeField] protected int BuffCounter = 0; //resets buff when this reaches 0 
    [SerializeField] protected float CurrentHP;
    [SerializeField] protected float CurrentMP;


    protected UnitAction SelectedAction;

    public virtual void BattleSetUp()
    {
        //if friendly or enemy, set current hp and mp to their max
        CurrentHP = Stats.MaxHealth;
        CurrentMP = Stats.MaxMP;
        //reset buffs
        BuffCounter = 0;
        CheckBuffs();
    }


    public virtual void ActionAnimFinished()
    {
        Debug.Log("Finish");
        SelectedAction.ExecuteActionEffect();
    }

    public float GetCurrentHP()
    {
        return CurrentHP;
    }

    public float GetCurrentMP()
    {
        return CurrentMP;
    }

    public void CheckBuffs()
    {
        if(BuffCounter == 0)
        {
            Buffs.ResetBuffs();
        }
    }

    public void StartAnimation(string clipName)
    {
        Animator.Play(clipName);
    }

    public float GetFinalPhysAtk()
    {
        return Stats.PhysAtk * (1f+Buffs.PhysAtk_Buff);
    }

    public float GetFinalMagAtk()
    {
        return Stats.MagAtk * (1f+Buffs.MagAtk_Buff);
    }

    public float GetFinalPhysDef()
    {
        return Stats.PhysDef * (1f+Buffs.PhysDef_Buff);
    }

    public float GetFinalMagDef()
    {
        return Stats.MagDef * (1f+Buffs.MagDef_Buff);
    }

    public float GetFinalSpd()
    {
        return Stats.Spd * (1f+Buffs.Spd_Buff);
    }

    public void IncreaseBuffCounter(int value)
    {
        BuffCounter += value;
    }

    public UnitBuffs GetUnitBuffs()
    {
        return Buffs;
    }

}
