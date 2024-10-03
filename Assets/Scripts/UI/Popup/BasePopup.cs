using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePopup : MonoBehaviour
{
    [SerializeField] private GameObject Popup_GO;

    private bool isPopupActive = false;

    public virtual void ActivatePopup()
    {
        //popup already active, do nothing
        if(isPopupActive == true)
        {
            return;
        }

        Popup_GO.SetActive(true);
        isPopupActive = true;
    }

    public virtual void DeactivatePopup()
    {
        //popup already deactivated, do nothing
        if(isPopupActive == false)
        {
            return;
        }

        Popup_GO.SetActive(false);
        isPopupActive = false;
    }

    public bool GetPopupActive()
    {
        return isPopupActive;
    }
}
