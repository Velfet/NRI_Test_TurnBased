using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CombatActionButton_Skill : CombatActionButton, IButton
{
    [SerializeField] private TextMeshProUGUI SkillNameText;
    [SerializeField] private TextMeshProUGUI MPCostText;

    public override void SetCombatAction(UnitAction theAction, UnitBase theUnit)
    {
        CombatAction = theAction;
        CurrentUnit = theUnit;

        //set button to call the CombatActionButtonPressed when pressed
        MyButton.onClick.RemoveAllListeners();
        MyButton.onClick.AddListener(CombatActionButtonPressed);

        //change button visual
        SkillNameText.text = CombatAction.GetActionName();
        MPCostText.text = "MP:" + CombatAction.GetActionMPCost().ToString("F0");    //ensure no decimal for MP cost number
    }
}
