using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class AbstractMonthStateChild : AbstractStateChild
{
    protected Fuukei _fuukei;
    protected Koyomi _koyomi;
    protected Action _tempAction;

    // 各月共通の初期化処理
    public override void Initialize(int stateType)
    {
        _fuukei = GameObject.Find("KuniNoKanban/").GetComponent<Fuukei>();
        _koyomi = transform.parent.GetComponent<Koyomi>();
        _tempAction = null;

        base.Initialize(stateType);
    }

    // 各月共通の入口処理
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

    // その月になったら１回だけ実行する Event の登録
    public virtual void AddSingleShotEventOnEnter(Action action)
    {
        _tempAction += action;
    }
}
