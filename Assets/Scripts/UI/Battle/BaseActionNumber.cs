using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BaseActionNumber : MonoBehaviour
{
    [SerializeField] private RectTransform MyRect;
    [SerializeField] private TextMeshProUGUI NumberText;
    [SerializeField] private Color NumberText_Color_Damage;
    [SerializeField] private Color NumberText_Color_Heal;

    [SerializeField] private Animator Animator;
    [SerializeField] private string AnimationClipName;

    private ActionNumbersManager actionNumbersManager;

    public void BeginActionNumberAnim(float NumberText_Value, Vector3 targetWorldPos, RectTransform canvasRect, ActionNumbersManager newManager, MyEnum.NumberType numberType)
    {
        gameObject.SetActive(true);

        actionNumbersManager = newManager;

        //set position
        Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, targetWorldPos);
        Vector2 canvasPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, screenPoint, null, out canvasPos);
        MyRect.anchoredPosition = canvasPos;

        //set color
        SetColor(numberType);

        //set text
        NumberText.text = NumberToString.GetStringFromNumber_ShowDecimalOnlyIfExist(NumberText_Value);
        Animator.Play(AnimationClipName);
    }

    public void OnAnimFinished()
    {
        //return self to pooler, pooler should be the one to deactivate this object
        actionNumbersManager.ReturnActionNumber(this);
    }

    private void SetColor(MyEnum.NumberType numberType)
    {
        if(numberType == MyEnum.NumberType.Damage)
        {
            NumberText.color = NumberText_Color_Damage;
        }
        else if(numberType == MyEnum.NumberType.Heal)
        {
            NumberText.color = NumberText_Color_Heal;
        }
    }
}
