using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeasonStateChild_January : AbstractStateChild
{
    private Koyomi _koyomi;
    public override void Initialize(int stateType)
    {
        _koyomi = transform.parent.GetComponent<Koyomi>();
        base.Initialize(stateType);
    }
    public override void OnEnter()
    {
        Debug.Log($"[{this.name}] Enter ** {StateType} **");
        _koyomi.ShowKoyomi();
    }
    public override void OnExit()
    {

    }
    public override int StateUpdate()
    {
        if (_koyomi.IsNewMonth)
        {
            return (int)SeasonStateController.StateType.February;
        }
        return (int)StateType;
    }
}
