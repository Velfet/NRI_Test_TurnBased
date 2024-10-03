using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICombatAction
{
    //start animation
    public void ActivateAction();

    //once animation finishes, apply the effect
    public void ExecuteActionEffect();

    //check if action can be performed
    public bool CanExecuteAction(UnitBase theUnit);
    
}
