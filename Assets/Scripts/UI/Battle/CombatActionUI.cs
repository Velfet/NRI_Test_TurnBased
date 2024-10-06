using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//manages UI related to combat action, such as displaying combat actio name
public class CombatActionUI : MonoBehaviour
{
    [SerializeField] private CombatActionNameUI CombatActionName;
    [SerializeField] private ActionNumbersManager ActionNumbersManager;

    public void ShowCombatActionName(string newName)
    {
        CombatActionName.ShowCombatActionName(newName);
    }

    public void HideCombatActionName()
    {
        CombatActionName.HideCombatActionName();
    }

    public void ActivateNumber(float numberValue, Vector3 worldPos, MyEnum.NumberType numberType)
    {
        ActionNumbersManager.ActivateNumber(numberValue, worldPos, numberType);
    }


}
