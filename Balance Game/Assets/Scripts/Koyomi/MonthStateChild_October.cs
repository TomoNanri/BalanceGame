using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonthStateChild_October : MonthStateChild
{
    public override void OnEnter()
    {
        _koyomi.ShowKoyomi();
        _koyomi.IsNenguAvailable = true;
        _koyomi.IsMatsuriDone = false;
        base.OnEnter();
    }
    public override void OnExit()
    {
        _koyomi.IsNenguAvailable = false;
        base.OnExit();
    }
    public override int StateUpdate()
    {
        if (_koyomi.IsNewMonth)
        {
            _koyomi.IsNewMonth = false;
            return (int)MonthStateController.StateType.November;
        }
        return (int)StateType;
    }
}
