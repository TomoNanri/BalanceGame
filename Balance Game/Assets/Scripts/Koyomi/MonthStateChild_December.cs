using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonthStateChild_December : MonthStateChild
{
    private Oshiro _oshiro;
    public override void Initialize(int stateType)
    {
        _oshiro = GameObject.FindAnyObjectByType<Oshiro>();
        base.Initialize(stateType);
    }
    public override void OnExit()
    {
        if (_koyomi.IsTaLevelUpOrdered)
        {
            _koyomi.IsTaLevelUpOrdered = false;
            _oshiro.TaLevel += 1;
        }
    }
    public override int StateUpdate()
    {
        if (_koyomi.IsNewMonth)
        {
            _koyomi.IsNewMonth = false;
            return (int)MonthStateController.StateType.January;
        }
        return (int)StateType;
    }
}
