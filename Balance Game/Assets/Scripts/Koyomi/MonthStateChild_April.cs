using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonthStateChild_April : AbstractMonthStateChild
{
    public override void OnEnter()
    {
        _koyomi.IsMatsuriDone = false;
        _fuukei.SetFuukei(MonthStateController.Kisetsu.Haru);
        _koyomi.IsAbleToIncreaseTa = true;

        base.OnEnter();
    }
    
    public override int StateUpdate()
    {
        if (_koyomi.IsNewMonth)
        {
            _koyomi.IsNewMonth = false;
            return (int)MonthStateController.StateType.May;
        }
        return (int)StateType;
    }
}
