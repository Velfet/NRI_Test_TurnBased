using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BaseActionMenu : MonoBehaviour, IActionPanel
{
    [SerializeField] protected Transform PanelTransform;

    public virtual void ActivatePanel(UnitPlayer currentUnit)
    {
        PanelTransform.localScale = new Vector3(1,1,1);
    }

    public virtual void DeactivatePanel()
    {
        PanelTransform.localScale = new Vector3(0,0,0);
    }
}
