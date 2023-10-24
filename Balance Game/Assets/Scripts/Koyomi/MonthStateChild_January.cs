using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonthStateChild_January : AbstractMonthStateChild
{
    public override void OnEnter()
    {
        _koyomi.IsMatsuriDone = false;
        _koyomi.SetHiyori();
        _fuukei.SetFuukei(MonthStateController.Kisetsu.Fuyu);
        _koyomi.IsAbleToIncreaseTa = true;

        base.OnEnter();
    }
    public override int StateUpdate()
    {
        if (_koyomi.IsNewMonth)
        {
            _koyomi.IsNewMonth = false;
            return (int)MonthStateController.StateType.February;
        }
        return (int)StateType;
    }
}
