using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonthStateChild_May : AbstractMonthStateChild
{
    public override int StateUpdate()
    {
        if (_koyomi.IsNewMonth)
        {
            _koyomi.IsNewMonth = false;
            return (int)MonthStateController.StateType.June;
        }
        return (int)StateType;
    }
}
