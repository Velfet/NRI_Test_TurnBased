using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionNumbersManager : MonoBehaviour
{
    [SerializeField] private RectTransform CanvasRect;
    [SerializeField] private List<BaseActionNumber> ActiveActionNumbers = new List<BaseActionNumber>();
    [SerializeField] private List<BaseActionNumber> DormantActionNumbers = new List<BaseActionNumber>();

    public void ReturnActionNumber(BaseActionNumber returnedNumber)
    {
        //deactivate number
        returnedNumber.gameObject.SetActive(false);
        //remove from active list
        ActiveActionNumbers.Remove(returnedNumber);
        //add to dormant list
        DormantActionNumbers.Add(returnedNumber);
    }

    public void ActivateNumber(float numberValue, Vector3 worldPos, MyEnum.NumberType numberType)
    {
        //check if there are any available numbers
        if(DormantActionNumbers.Count > 0)
        {
            //get number from dormant list
            BaseActionNumber theNumber = DormantActionNumbers[0];
            //remove from dormant list
            DormantActionNumbers.Remove(theNumber);
            //add to active list
            ActiveActionNumbers.Add(theNumber);
            //activate the number
            theNumber.BeginActionNumberAnim(numberValue, worldPos, CanvasRect, this, numberType);
        }
        else
        {
            Debug.LogWarning("Not enough numbers");
        }
    }

}
