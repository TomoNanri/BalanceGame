using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateChild_Initialize : AbstractStateChild
{
    private bool _gameInitialized = false;
    private GameManager _gm;

    public override void Initialize(int stateType)
    {
        _gm = FindAnyObjectByType<GameManager>();
        base.Initialize(stateType);
    }
    public override void OnEnter()
    {
        Debug.Log($"[{this.name}] Enter Initialize State!");
        if(_gm.InitializeHandler != null)
        {
            _gm.InitializeHandler.Invoke();
        }
        _gameInitialized = true;
    }
    public override void OnExit()
    {
        // None
    }
    public override int StateUpdate()
    {
        if (_gameInitialized)
        {
            return (int)GameManager.StateType.Intro;
        }
        return (int)StateType;
    }
}
