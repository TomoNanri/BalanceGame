using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameStateChild_Progress : AbstractStateChild
{
    private GameManager _gm;
    private Oshiro _oshiro;
    private Koyomi _koyomi;
    private Mura _UshitoraMura;
    private Mura _InuiMura;
    private Mura _HitsujisaruMura;
    private Mura _TatsumiMura;
    private bool _isEventComplete;
    public override void Initialize(int stateType)
    {
        _gm = GetComponent<GameManager>();
        _koyomi = GameObject.Find("Koyomi").GetComponent<Koyomi>();
        _oshiro = GameObject.Find("Oshiro").GetComponent<Oshiro>();
        _oshiro.ShisakuEnd += EventEndHandler;

        _UshitoraMura = GameObject.Find("UshitoraMura").GetComponent<Mura>();
        _InuiMura = GameObject.Find("InuiMura").GetComponent<Mura>();
        _HitsujisaruMura = GameObject.Find("HitsujisaruMura").GetComponent<Mura>();
        _TatsumiMura = GameObject.Find("TatsumiMura").GetComponent<Mura>();

        _isEventComplete = false;

        base.Initialize(stateType);
    }
    public override void OnEnter()
    {
        // 翌月突入 
        Debug.Log($"[{this.name}] Enter Progress State!(Next Month Start)");

        // 暦の更新
        _koyomi.GoNextMonth();

        // 村の満足度チェック
        _UshitoraMura.CheckSatisfaction();
        _InuiMura.CheckSatisfaction();
        _HitsujisaruMura.CheckSatisfaction();
        _TatsumiMura.CheckSatisfaction();
    }
    public override void OnExit()
    {
        _isEventComplete = false;
    }

    public override int StateUpdate()
    {
        if (_isEventComplete)
        {
            _isEventComplete = false;
            return (int)GameManager.StateType.WaitInput;
        }
        return (int)StateType;
    }
    private void EventEndHandler(object sender, EventArgs args)
    {
        _isEventComplete = true;
    }
}
