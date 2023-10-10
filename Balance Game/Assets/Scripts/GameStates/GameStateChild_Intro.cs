using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateChild_Intro : AbstractStateChild
{
    private GameManager _gm;
    private GameObject _introCanvas;
    public override void Initialize(int stateType)
    {
        _gm = GetComponent<GameManager>();
        _introCanvas = GameObject.Find("IntroCanvas");
        base.Initialize(stateType);
    }
    public override void OnEnter()
    {
        _introCanvas.SetActive(true);
    }
    public override void OnExit()
    {
        _introCanvas.SetActive(false);
        _gm.OnLoadGame = false;
        _gm.OnNewGame = false;
    }

    public override int StateUpdate()
    {
        if (_gm.OnNewGame)
        {
            return (int)GameManager.StateType.WaitInput;
        }
        if (_gm.OnLoadGame)
        {
            return (int)GameManager.StateType.Loading;
        }
        return (int)StateType;
    }
}
