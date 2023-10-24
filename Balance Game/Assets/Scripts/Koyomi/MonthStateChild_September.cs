using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonthStateChild_September : AbstractMonthStateChild
{
    public override void OnEnter()
    {
        _fuukei.SetFuukei(MonthStateController.Kisetsu.Inekari);
        _koyomi.IsAbleToIncreaseTa = false;

        base.OnEnter();
    }
    public override int StateUpdate()
    {
        if (_koyomi.IsNewMonth)
        {
            _koyomi.IsNewMonth = false;
            return (int)MonthStateController.StateType.October;
        }
        return (int)StateType;
    }
}
