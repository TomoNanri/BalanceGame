using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonthStateChild_January : AbstractMonthStateChild
{
    public override void OnEnter()
    {
        _koyomi.IsMatsuriDone = false;
        _koyomi.SetHiyori();
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
