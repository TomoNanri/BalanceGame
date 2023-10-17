using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonthStateChild_July : MonthStateChild
{
    public override int StateUpdate()
    {
        if (_koyomi.IsNewMonth)
        {
            _koyomi.IsNewMonth = false;
            return (int)MonthStateController.StateType.August;
        }
        return (int)StateType;
    }
}
