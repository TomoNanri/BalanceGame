using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateChild_Loading : AbstractStateChild
{
    private GameManager _gm;
    public override void Initialize(int stateType)
    {
        _gm = GetComponent<GameManager>();
        base.Initialize(stateType);
    }
    public override void OnEnter()
    {
        // PlayerPrefsの読み込み
        Debug.Log($"[{this.name}] Enter Loading State!");
        _gm.LoadGameProc.Invoke();
    }
    public override void OnExit()
    {
        // None
    }

    public override int StateUpdate()
    {
        return (int)GameManager.StateType.WaitInput;
    }
}
