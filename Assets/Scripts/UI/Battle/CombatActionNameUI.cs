using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CombatActionNameUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI CombatActionName_Text;

    public void ShowCombatActionName(string name)
    {
        //enable game object
        gameObject.SetActive(true);
        //update text
        CombatActionName_Text.text = name;   
    }

    public void HideCombatActionName()
    {
        //disable game object
        gameObject.SetActive(false);
    }
}
