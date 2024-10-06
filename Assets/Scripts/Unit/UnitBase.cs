using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class UnitBase : MonoBehaviour
{
    [SerializeField] protected MyEnum.UnitType UnitType;
    [SerializeField] protected UnitStats_SO Stats;
    [SerializeField] protected UnitBuffs Buffs;
    [Space(20)]
    [Space(20)]
    [SerializeField] protected SpriteRenderer UnitBattleSprite;
    [SerializeField] protected SpriteRenderer UnitSelectedIndicatorSprite;
    [SerializeField] protected Sprite UnitIcon;
    [SerializeField] protected Color UnitIcon_Color;
    [SerializeField] protected Animator Animator;
    [Space(20)]
    [SerializeField] protected int BuffCounter = 0; //resets buff when this reaches 0 
    [SerializeField] protected float CurrentHP;
    [SerializeField] protected float CurrentMP;
    [SerializeField] protected float CurrentWaitTime;
    [SerializeField] protected bool IsDefeated = false;
    [Space(20)]
    [SerializeField] protected int UI_Priority;

    public Action<UnitBase> hpChangeEvent;
    public Action<UnitBase> mpChangeEvent;
    public Action<UnitBase> unitSelectedEvent;
    public Action<UnitBase> unitDeselectedEvent;

    protected UnitAction SelectedAction;
    protected TurnSystem turnSystem;
    protected BattleManager battleManager;

    public virtual void BattleSetUp()
    {
        //reset defeated status
        IsDefeated = false;
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
        if(SelectedAction == null)
        {
            Debug.LogWarning("no action");
            return;
        }
        SelectedAction.ExecuteActionEffect();

        //move on to next turn
        if(turnSystem == null)
        {
            turnSystem = BattleManager.Instance.GetTurnSystem();
        }

        turnSystem.EndTurnOfUnit(this);
    }

    public virtual void BeginAct()
    {
        //reduce buff counter and check if buffs expire
        BuffCounter = Math.Max(0, BuffCounter - 1);
        CheckBuffs();
        //Debug.Log("Default act. Need to do action to move the game along");
    }

    public virtual void UpdateCurrentHP(float alterAmount)
    {
        CurrentHP = Math.Clamp(CurrentHP+alterAmount, 0, GetMaxHP());

        hpChangeEvent?.Invoke(this);

        if(CurrentHP == 0)
        {
            //unit is defeated
            Debug.Log("Unit is defeated, alter turn order action");
            SetIsDefeated(true);

            if(turnSystem == null)
            {
                turnSystem = BattleManager.Instance.GetTurnSystem();
            }

            turnSystem.ReceiveUnitIsDeadReport(this);
        }
    }

    public virtual void UpdateCurrentMP(float alterAmount)
    {
        CurrentMP = Math.Clamp(CurrentMP+alterAmount, 0, GetMaxMP());

        mpChangeEvent?.Invoke(this);
    }

    public void SetIsDefeated(bool newStatus)
    {
        IsDefeated = newStatus;
    }

    public float GetMaxHP()
    {
        return Stats.MaxHealth;
    }

    public float GetMaxMP()
    {
        return Stats.MaxMP;
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
        //Debug.LogWarning("Clip name: " + clipName);
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

    public bool GetDefeatedStatus()
    {
        return IsDefeated;
    }

    public void IncreaseBuffCounter(int value)
    {
        BuffCounter += value;
    }

    public UnitBuffs GetUnitBuffs()
    {
        return Buffs;
    }

    public (Sprite unitIcon, Color unitIcon_Color) GetUnitIconData()
    {
        return (UnitIcon, UnitIcon_Color);
    }

    public float GetCurrentWaitTime()
    {
        return CurrentWaitTime;
    }

    public void SetCurrentWaitTime(float newVal)
    {
        CurrentWaitTime = newVal;
    }

    public void AlterCurrentWaitTime(float alterAmount)
    {
        CurrentWaitTime += alterAmount;
    }

    public MyEnum.UnitType GetUnitType()
    {
        return UnitType;
    }

    public void DamageUnit(float damageAmount, MyEnum.DamageType damageType)
    {
        float finalDamageAmount = damageAmount;
        float reduceAmount = 0f;

        if(damageType == MyEnum.DamageType.Physical)
        {
            reduceAmount = GetFinalPhysDef();
        }
        else if(damageType == MyEnum.DamageType.Magic)
        {
            reduceAmount = GetFinalMagDef();
        }
        else if(damageType == MyEnum.DamageType.Combination)
        {
            reduceAmount = (GetFinalMagDef() + GetFinalPhysDef())/2f;
        }

        finalDamageAmount = Math.Max(1, finalDamageAmount - reduceAmount);

        //do damage number visual here

        //affect HP
        UpdateCurrentHP(-finalDamageAmount);
    }

    public void HealUnit(float healAmount)
    {
        //do heal visual here

        //affet HP
        UpdateCurrentHP(healAmount);
    }

    public void SetSelectedAction(UnitAction theAction)
    {
        SelectedAction = theAction;
    }

    //unit was selected by the targetting system, update visuals accordingly
    public void SelectUnit()
    {
        UnitSelectedIndicatorSprite.gameObject.SetActive(true);
        unitSelectedEvent?.Invoke(this);
    }

    //unit was de-selected by the targetting system, update visuals accordingly
    public void DeselectUnit()
    {
        UnitSelectedIndicatorSprite.gameObject.SetActive(false);
        unitDeselectedEvent?.Invoke(this);
    }

    public void Toggle_UnitBattleSprite(bool newStatus)
    {
        UnitBattleSprite.gameObject.SetActive(newStatus);
    }

    public int GetUI_Priority()
    {
        return UI_Priority;
    }

}
