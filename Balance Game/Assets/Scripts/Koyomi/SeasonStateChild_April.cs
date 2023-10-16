using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeasonStateChild_April : AbstractStateChild
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
        _koyomi.IsMatsuriDone = false;
    }
    public override void OnExit()
    {
        //throw new System.NotImplementedException();
    }
    public override int StateUpdate()
    {
        if (_koyomi.IsNewMonth)
        {
            return (int)SeasonStateController.StateType.May;
        }
        return (int)StateType;
    }
}
