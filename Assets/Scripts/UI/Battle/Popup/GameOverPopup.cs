using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverPopup : BasePopup
{
    [SerializeField] private Image GameOverPopup_BackgroundImg;
    
    private BattleUnitManager battleUnitManager;

    public override void ActivatePopup()
    {
        base.ActivatePopup();
        GameOverPopup_BackgroundImg.enabled = true;
    }

    public override void DeactivatePopup()
    {
        base.DeactivatePopup();
        GameOverPopup_BackgroundImg.enabled = false;
    }

    public void RetryButtonFunction()
    {
        battleUnitManager = BattleManager.Instance.GetBattleUnitManager();
        List<UnitBase> playerUnits = battleUnitManager.GetPlayerUnits();
        //set their health, mp, and defeated status to normal
        foreach (UnitBase playerUnit in playerUnits)
        {
            playerUnit.RefreshUnit();
        }

        //send player to overworld
        SceneManager.LoadScene(MyEnum.SceneNames.Overworld.ToString());
        //don't update current encounter to being defeated
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
