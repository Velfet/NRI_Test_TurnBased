using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MarkedButton : BaseButton, ISelectHandler, IDeselectHandler
{
    [SerializeField] private GameObject Mark_GO;

    public override void OnSelect(BaseEventData eventData)
    {
        //Debug.Log("Button " + gameObject.name + " selected");
        Mark_GO.SetActive(true);
    }

    public override void OnDeselect(BaseEventData eventData)
    {
        //Debug.Log("Button " + gameObject.name + " de-selected");
        Mark_GO.SetActive(false);
    }
}
