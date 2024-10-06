using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseCombatButton : MonoBehaviour, IButton
{
    [SerializeField] protected Button MyButton;
    [SerializeField] protected bool ButtonIsInteractable = true;


    public void SetButtonInteractable(bool newStatus)
    {
        MyButton.enabled = newStatus;
        ButtonIsInteractable = newStatus;
    }

    public void SetButtonSelected()
    {
        //set this button as selected
        MyButton.Select();
    }
}
