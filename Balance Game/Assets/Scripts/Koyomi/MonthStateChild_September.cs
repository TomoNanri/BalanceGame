using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonthStateChild_September : AbstractMonthStateChild
{
    public override int StateUpdate()
    {
        if (_koyomi.IsNewMonth)
        {
            _koyomi.IsNewMonth = false;
            _fuukei.SetFuukei(MonthStateController.Kisetsu.Aki);
            return (int)MonthStateController.StateType.October;
        }
        return (int)StateType;
    }
}
