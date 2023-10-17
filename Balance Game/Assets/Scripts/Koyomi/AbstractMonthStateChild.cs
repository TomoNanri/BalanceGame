using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class AbstractMonthStateChild : AbstractStateChild
{

    protected Koyomi _koyomi;
    protected Action _tempAction;

    // �e�����ʂ̏���������
    public override void Initialize(int stateType)
    {
        _koyomi = transform.parent.GetComponent<Koyomi>();
        _tempAction = null;

        base.Initialize(stateType);
    }

    // �e�����ʂ̓�������
    public override void OnEnter()
    {
        Debug.Log($"[{this.name}] Enter ** {StateType} **");
        _koyomi.ShowKoyomi();
        if(_tempAction != null)
        {
            _tempAction();
            _tempAction = null;
        }
    }
    public override void OnExit()
    {
    }

    // ���̌��ɂȂ�����P�񂾂����s���� Event �̓o�^
    public virtual void AddSingleShotEventOnEnter(Action action)
    {
        _tempAction += action;
    }
}
