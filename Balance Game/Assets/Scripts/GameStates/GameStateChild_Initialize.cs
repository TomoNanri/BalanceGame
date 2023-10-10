using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateChild_Initialize : AbstractStateChild
{
    private bool _gameInitialized = false;
    public override void OnEnter()
    {
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
