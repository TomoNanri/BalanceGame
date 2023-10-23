using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class GameStateChild_WaitInput : AbstractStateChild
{
    private GameManager _gm;
    private Oshiro _oshiro;
    private bool _isOshiroSelected;
    private GodOfLuck _dice;
    //private bool _isGameOver;
    public override void Initialize(int stateType)
    {
        _gm = GetComponent<GameManager>();
        _oshiro = GameObject.Find("Oshiro").GetComponent<Oshiro>();
        _oshiro.OshiroSelected += OnOshiroSelected;
        _dice = FindAnyObjectByType<GodOfLuck>();

        base.Initialize(stateType);
    }
    public override void OnEnter()
    {
        Debug.Log($"[{this.name}] Enter WaitInput State!");
        
        // お城のクリックを可能にし、クリックイベントの受信を開始する
        _oshiro.IsSelectable = true;
        _isOshiroSelected = false;

        // ガイドメッセージを表示
        _oshiro.ShowOtonosama(true);
    }
    public override void OnExit()
    {
        // お城のクリックを不可にする
        _oshiro.IsSelectable = false;
        _oshiro.ShowOtonosama(false);
    }
    public override int StateUpdate()
    {
        if (_isOshiroSelected)
        {
            return (int)GameManager.StateType.InMainMenu;
        }
        return (int)StateType;
    }
    private void OnOshiroSelected(object sender, EventArgs args)
    {
        _isOshiroSelected = true;
    }
}
