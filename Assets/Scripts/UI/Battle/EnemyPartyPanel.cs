using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPartyPanel : MonoBehaviour
{
    [SerializeField] private Dictionary<UnitBase, BasePanel> enemyPanels_Dict = new Dictionary<UnitBase, BasePanel>();
    [SerializeField] private List<UnitBase> UnitEnemies = new List<UnitBase>();
    [SerializeField] private GameObject EnemyPanel_Prefab;

    private BattleManager battleManager;
    public void SetupPanel()
    {   
        //receive all enemies
        battleManager = BattleManager.Instance;

        UnitEnemies = battleManager.GetBattleUnitManager().GetEnemyUnits();

        for(int i = 0; i < UnitEnemies.Count; i++)
        {
            UnitBase currentUnit = UnitEnemies[i];

            //subscribe to event where enemy hp
            currentUnit.hpChangeEvent += UpdatePanel;

            //create a base panel for each enemy
            //need to make sure this happens AFTER we receive all enemies
            BasePanel enemyPanel = Instantiate(EnemyPanel_Prefab, this.transform).GetComponent<BasePanel>();

            //setup the panel
            enemyPanel.SetupPanel(currentUnit, currentUnit.GetCurrentHP(), currentUnit.GetCurrentMP());

            //store connection between which unit belong to which unit panel
            enemyPanels_Dict.Add(currentUnit, enemyPanel);
        }
    }

    public void UpdatePanel(UnitBase theUnit)
    {
        //find the unit panel that belongs to the unit
        BasePanel enemyPanel = enemyPanels_Dict[theUnit];

        enemyPanel.UpdateHP_UI(theUnit.GetCurrentHP());
    }

}
