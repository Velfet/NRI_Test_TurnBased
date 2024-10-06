using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillActionMenu : BaseActionMenu, IActionPanel
{
    [SerializeField] private GameObject SkillActionButtonPrefab;
    [Space(20)]
    [SerializeField] private List<UnitAction> Skills;
    [SerializeField] private List<CombatActionButton_Skill> SkillButtons = new List<CombatActionButton_Skill>();

    private UnitPlayer myUnit;

    //setup panel, but don't activate it yet
    public void SetupPanel(UnitPlayer currentUnit)
    {
        myUnit = currentUnit;

        //get reference to action manager of current player unit
        UnitActionManager_Player unitActionManager = currentUnit.GetUnitActionManager();

        //populate skills list
        Skills = new List<UnitAction>();
        Skills = unitActionManager.GetUnitSkills();

        
        //remove unneeded skill buttons
        while(SkillButtons.Count > Skills.Count)
        {
            CombatActionButton_Skill unnededButton = SkillButtons[SkillButtons.Count - 1];
            SkillButtons.Remove(unnededButton);
            Destroy(unnededButton.gameObject);
        }

        //instantiate prefabs for each skill
        for(int i = 0; i < Skills.Count; i++)
        {
            //add skill buttons to list if entry doesn't exist
            if(SkillButtons.Count < i+1)
            {
                //make new skill button
                CombatActionButton_Skill newSkillButton = Instantiate(SkillActionButtonPrefab, this.transform).GetComponent<CombatActionButton_Skill>();
                //set up the button
                newSkillButton.SetCombatAction(Skills[i], myUnit);
                //make button uninteractable
                newSkillButton.SetButtonInteractable(false);
                
                //add to list
                SkillButtons.Add(newSkillButton);
            }
            else
            {
                //use old skill button
                CombatActionButton_Skill oldSkillButton = SkillButtons[i];
                //set old skill button with new data
                oldSkillButton.SetCombatAction(Skills[i], myUnit);
                //make button uninteractable
                oldSkillButton.SetButtonInteractable(false);
            }
        }
    }

    public override void ActivatePanel(UnitPlayer currentUnit)
    {
        //set buttons to be interactable
        foreach(CombatActionButton_Skill button in SkillButtons)
        {
            button.SetButtonInteractable(true);
        }
        //select first skill button as selectable, if it even exists
        if(SkillButtons.Count != 0)
        {
            SkillButtons[0].SetButtonSelected();
        }
        //set scale so the panel is visible
        PanelTransform.localScale = new Vector3(1,1,1);
    }

    public override void DeactivatePanel()
    {
        //set buttons to not be interactable anymore
        foreach(CombatActionButton_Skill button in SkillButtons)
        {
            button.SetButtonInteractable(false);
        }
        //set scale so panel is not visible
        PanelTransform.localScale = new Vector3(0,0,0);
    }
}
