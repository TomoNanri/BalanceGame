using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonthStateChild_July : AbstractMonthStateChild
{
    public override void OnEnter()
    {
        _fuukei.SetFuukei(MonthStateController.Kisetsu.Natsu);
        base.OnEnter();
    }
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
