using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BaseButton : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    [SerializeField] protected bool IsDefaultSelected = false;

    protected virtual void OnDisable()
    {
        if(EventSystem.current != null)
        {
            if(gameObject == EventSystem.current.currentSelectedGameObject)
            {
                //if this gameobject is selected, deselect this gameobject before disabling itself
                EventSystem.current.SetSelectedGameObject(null);
                var eventData = new BaseEventData(EventSystem.current);
                OnDeselect(eventData);
            }
        }
    }

    protected virtual void OnEnable()
    {
        if(IsDefaultSelected == true)
        {
            EventSystem.current.SetSelectedGameObject(gameObject);
        }
    }

    public virtual void OnDeselect(BaseEventData eventData)
    {
        
    }

    public virtual void OnSelect(BaseEventData eventData)
    {
        
    }
}
