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
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false; // Stops play mode in the editor
        #elif UNITY_WEBGL
            Application.OpenURL(Application.absoluteURL);   //refresh the page
        #elif UNITY_STANDALONE
            Application.Quit(); // Quits the application for standalone builds
        #else
            Application.Quit(); // A fallback for other platforms
        #endif
    }
}
