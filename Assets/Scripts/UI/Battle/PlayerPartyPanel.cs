using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPartyPanel : MonoBehaviour
{
    [SerializeField] private Dictionary<UnitBase, PartyMemberPanel> partyMemberPanels_Dict = new Dictionary<UnitBase, PartyMemberPanel>();
    [SerializeField] private List<UnitBase> UnitPlayers = new List<UnitBase>();
    [SerializeField] private GameObject PlayerPartyPanel_Prefab;

    private BattleManager battleManager;
    public void SetupPanel()
    {   
        //receive all party members
        battleManager = BattleManager.Instance;

        UnitPlayers = battleManager.GetBattleUnitManager().GetPlayerUnits();

        for(int i = 0; i < UnitPlayers.Count; i++)
        {
            UnitBase currentUnit = UnitPlayers[i];

            //subscribe to event where player hp and mp changes
            currentUnit.hpChangeEvent += UpdateHPPanel;
            currentUnit.mpChangeEvent += UpdateMPPanel;

            //create a party member panel for each party member, in a consistent order
            //need to make sure this happens AFTER we receive all party members
            PartyMemberPanel playerPanel = Instantiate(PlayerPartyPanel_Prefab, this.transform).GetComponent<PartyMemberPanel>();

            //setup the panel
            playerPanel.SetupPanel(currentUnit, currentUnit.GetCurrentHP(), currentUnit.GetCurrentMP());

            //store connection between which unit belong to which unit panel
            partyMemberPanels_Dict.Add(currentUnit, playerPanel);
        }
    }

    public void UpdatePanel(UnitBase theUnit, MyEnum.BattleUI_UpdateType updateType)
    {
        //find the unit panel that belongs to the unit
        PartyMemberPanel playerPanel = partyMemberPanels_Dict[theUnit];

        if(updateType == MyEnum.BattleUI_UpdateType.HP)
        {
            playerPanel.UpdateHP_UI(theUnit.GetCurrentHP());
        }
        else if(updateType == MyEnum.BattleUI_UpdateType.MP)
        {
            playerPanel.UpdateMP_UI(theUnit.GetCurrentMP());
        }
        
    }

    public void UpdateHPPanel(UnitBase theUnit)
    {
        UpdatePanel(theUnit, MyEnum.BattleUI_UpdateType.HP);
    }

    public void UpdateMPPanel(UnitBase theUnit)
    {
        UpdatePanel(theUnit, MyEnum.BattleUI_UpdateType.MP);
    }

    
    


}
