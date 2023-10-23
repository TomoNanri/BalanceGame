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
    private GodOfLuck _dice;

    private bool _isEventComplete;
    private bool _isGameOver;

    public override void Initialize(int stateType)
    {
        _gm = GetComponent<GameManager>();
        _koyomi = GameObject.Find("Koyomi").GetComponent<Koyomi>();
        _oshiro = GameObject.Find("Oshiro").GetComponent<Oshiro>();
        _oshiro.ShisakuEnd += EventEndHandler;
        _dice = FindAnyObjectByType<GodOfLuck>();

        _UshitoraMura = GameObject.Find("UshitoraMura").GetComponent<Mura>();
        _InuiMura = GameObject.Find("InuiMura").GetComponent<Mura>();
        _HitsujisaruMura = GameObject.Find("HitsujisaruMura").GetComponent<Mura>();
        _TatsumiMura = GameObject.Find("TatsumiMura").GetComponent<Mura>();

        _isEventComplete = false;
        _isGameOver = false;

        base.Initialize(stateType);
    }
    public override void OnEnter()
    {
        Debug.Log($"[{this.name}] Enter Progress State!(Next Month Start)");
    }
    public override void OnExit()
    {
        _isEventComplete = false;

        // 一揆状態ならお殿様の幸運判定を実施する
        _isGameOver = false;
        foreach (Mura m in _oshiro.MuraList)
        {
            if (m.IsIkkiJoutai)
            {
                if (!_dice.DiceCheckD100(_oshiro.Luck, 1))
                {
                    _isGameOver = true;
                }
            }
        }

        // 村の満足度チェック
        _UshitoraMura.CheckSatisfaction();
        _InuiMura.CheckSatisfaction();
        _HitsujisaruMura.CheckSatisfaction();
        _TatsumiMura.CheckSatisfaction();

        // 暦の更新
        _koyomi.GoNextMonth();
    }

    public override int StateUpdate()
    {
        if (_isEventComplete)
        {
            if (_isGameOver)
            {
                return (int)GameManager.StateType.GameOver;
            }
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
