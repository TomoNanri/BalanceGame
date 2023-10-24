using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonthStateChild_May : AbstractMonthStateChild
{
    public override void OnEnter()
    {
        _fuukei.SetFuukei(MonthStateController.Kisetsu.Haru);
        _koyomi.IsAbleToIncreaseTa = false;

        base.OnEnter();
    }
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
