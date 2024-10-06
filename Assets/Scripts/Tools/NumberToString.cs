using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class NumberToString
{
    public static string GetStringFromNumber_ShowDecimalOnlyIfExist(float number)
    {
        string number_String;
        if(number % 1 == 0)
        {
            number_String = number.ToString("0");
        }
        else
        {
            number_String = number.ToString("F1");
        }

        return number_String;
    }
}
