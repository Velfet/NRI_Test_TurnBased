using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//base class for combat actions
public class UnitAction : MonoBehaviour, ICombatAction
{
    [SerializeField] protected CombatActionData_SO CombatActionData;

    public virtual void ActivateAction()
    {
        //play animation

    }

    public virtual void ExecuteActionEffect()
    {
        //once animation finishes, apply the effect

    }

    public virtual bool CanExecuteAction(UnitBase theUnit)
    {

        return false;
    }
}
