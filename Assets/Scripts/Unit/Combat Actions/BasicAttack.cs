using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttack : UnitAction
{
    public override void ActivateAction()
    {
        //request a target
        //play animation

    }

    public override void ExecuteActionEffect()
    {
        //attack the target once animation finishes

    }

    public override bool CanExecuteAction(UnitBase theUnit)
    {
        //check if there is a valid target


        return true;
    }
}
