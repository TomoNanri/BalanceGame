using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeasonStateChild_December : AbstractStateChild
{
    private Koyomi _koyomi;
    private Oshiro _oshiro;
    public override void Initialize(int stateType)
    {
        _koyomi = GameObject.FindAnyObjectByType<Koyomi>();
        _oshiro = GameObject.FindAnyObjectByType<Oshiro>();

        base.Initialize(stateType);
    }
    public override void OnEnter()
    {
        Debug.Log($"[{this.name}] Enter ** {StateType} **");
        _koyomi.ShowKoyomi();
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
            return (int)SeasonStateController.StateType.January;
        }
        return (int)StateType;
    }
}
