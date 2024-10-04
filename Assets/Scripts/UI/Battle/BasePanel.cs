using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BasePanel : MonoBehaviour
{
    [SerializeField] protected Image UnitIconImg;
    [SerializeField] protected TextMeshProUGUI HPAmountText;

    protected UnitBase myUnit;
    protected float MaxHP;

    public virtual void SetupPanel(UnitBase theUnit, float currentHP, float currentMP)
    {
        myUnit = theUnit;
        MaxHP = myUnit.GetMaxHP();

        SetIconImage(myUnit.GetUnitIconData());
        UpdateHP_UI(currentHP);
    }

    public void UpdateHP_UI(float currentHP)
    {
        HPAmountText.text = currentHP.ToString()+"/"+MaxHP.ToString();
    }

    public void SetIconImage((Sprite theSprite, Color theColor) iconData)
    {
        UnitIconImg.sprite = iconData.theSprite;
        UnitIconImg.color = iconData.theColor;
    }
}
