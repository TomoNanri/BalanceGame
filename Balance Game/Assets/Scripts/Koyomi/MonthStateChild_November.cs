using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonthStateChild_November : AbstractMonthStateChild
{
    public override void OnEnter()
    {
        _fuukei.SetFuukei(MonthStateController.Kisetsu.Aki);
        _koyomi.IsAbleToIncreaseTa = true ;

        base.OnEnter();
    }
    public override int StateUpdate()
    {
        if (_koyomi.IsNewMonth)
        {
            _koyomi.IsNewMonth = false;
            return (int)MonthStateController.StateType.December;
        }
        return (int)StateType;
    }
}
