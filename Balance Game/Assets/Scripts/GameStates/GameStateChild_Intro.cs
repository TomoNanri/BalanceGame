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
        Debug.Log($"[{this.name}] Enter Intro State!");

        // イントロキャンバスの表示
        _introCanvas.SetActive(true);
    }
    public override void OnExit()
    {
        // イントロキャンバスの消去
        _introCanvas.SetActive(false);
        _gm.OnLoadGame = false;
        _gm.OnNewGame = false;
    }

    public override int StateUpdate()
    {
        if (_gm.OnNewGame)
        {
            _gm.OnNewGame = false;
            return (int)GameManager.StateType.WaitInput;
        }
        if (_gm.OnLoadGame)
        {
            _gm.OnLoadGame = false;
            return (int)GameManager.StateType.Loading;
        }
        return (int)StateType;
    }
}
