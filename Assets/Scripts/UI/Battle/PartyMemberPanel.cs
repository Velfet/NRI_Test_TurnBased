using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PartyMemberPanel : BasePanel
{
    [SerializeField] private TextMeshProUGUI MPAmountText;
    
    private float MaxMP;

    public override void SetupPanel(UnitBase theUnit, float currentHP, float currentMP)
    {
        myUnit = theUnit;
        MaxHP = myUnit.GetMaxHP();
        MaxMP = myUnit.GetMaxMP();

        UpdateHP_UI(currentHP);
        UpdateMP_UI(currentMP);
    }

    public void UpdateMP_UI(float currentMP)
    {
        MPAmountText.text = NumberToString.GetStringFromNumber_ShowDecimalOnlyIfExist(currentMP)+"/"+NumberToString.GetStringFromNumber_ShowDecimalOnlyIfExist(MaxMP);
    }
}
