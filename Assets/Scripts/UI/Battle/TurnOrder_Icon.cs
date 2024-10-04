using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnOrder_Icon : MonoBehaviour
{
    [SerializeField] private Image IconImage;
    

    public void UpdateIcon((Sprite newSprite, Color color) iconData)
    {
        IconImage.sprite = iconData.newSprite;
        IconImage.color = iconData.color;
    }
}
