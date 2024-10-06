using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupManager_Battle : PopupManagerBase
{
    [SerializeField] private GameOverPopup GameOverPopup;
    [SerializeField] private WinBattlePopup WinBattlePopup;

    public void Open_GameOverPopup()
    {
        GameOverPopup.ActivatePopup();
    }

    public void Open_WinBattlePopup()
    {
        WinBattlePopup.ActivatePopup();
    }


}
