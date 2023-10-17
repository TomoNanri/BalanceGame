using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonthStateChild_June : AbstractMonthStateChild
{
    public override int StateUpdate()
    {
        if (_koyomi.IsNewMonth)
        {
            _koyomi.IsNewMonth = false;
            return (int)MonthStateController.StateType.July;
        }
        return (int)StateType;
    }
}
