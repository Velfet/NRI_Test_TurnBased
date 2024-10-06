using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinBattlePopup : BasePopup
{
    [SerializeField] private Image WinBattlePopup_BackgroundImg;
    
    private BattleUnitManager battleUnitManager;

    public override void ActivatePopup()
    {
        base.ActivatePopup();
        WinBattlePopup_BackgroundImg.enabled = true;
    }

    public override void DeactivatePopup()
    {
        base.DeactivatePopup();
        WinBattlePopup_BackgroundImg.enabled = false;
    }

    public void ContinueButtonFunction()
    {
        //update current encounter to being defeated
        MyGameManager.Instance.GetOverworldManager().UpdateDefeatedStatusOfEnemyEncounter(true);
        //send player to overworld
        SceneManager.LoadScene(MyEnum.SceneNames.Overworld.ToString());
        
    }

    
}
