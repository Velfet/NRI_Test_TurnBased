using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IActionPanel
{
    public void ActivatePanel(UnitPlayer currentUnit);
    public void DeactivatePanel();
}
