using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PausePopup : BasePopup
{
    [SerializeField] private Image PausePopup_BackgroundImg;
    [SerializeField] private PopupManager PopupManager;

    public override void ActivatePopup()
    {
        base.ActivatePopup();
        PausePopup_BackgroundImg.enabled = true;
    }

    public override void DeactivatePopup()
    {
        base.DeactivatePopup();
        PausePopup_BackgroundImg.enabled = false;
    }

    public void ContinueButtonFunction()
    {
        PopupManager.Close_PausePopup();
    }

    public void ExitButtonFunction()
    {
        Application.Quit();
    }
}
