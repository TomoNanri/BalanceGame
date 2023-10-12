using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameStateChild_Progress : AbstractStateChild
{
    private GameManager _gm;
    private Oshiro _oshiro;
    private Koyomi _koyomi;
    private bool _isEventComplete;
    public override void Initialize(int stateType)
    {
        _gm = GetComponent<GameManager>();
        _koyomi = GameObject.Find("Koyomi").GetComponent<Koyomi>();
        _oshiro = GameObject.Find("Oshiro").GetComponent<Oshiro>();
        base.Initialize(stateType);
    }
    public override void OnEnter()
    {
        // —‚Œ“Ë“ü 
        Debug.Log($"[{this.name}] Enter Progress State!(Next Month Start)");

        // —ï‚ÌXV
        _koyomi.GoNextMonth();
        _isEventComplete = true;
    }
    public override void OnExit()
    {
        // None
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

}
