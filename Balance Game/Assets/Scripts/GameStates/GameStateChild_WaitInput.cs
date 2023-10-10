using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateChild_WaitInput : AbstractStateChild
{
    private GameManager _gm;
    public override void Initialize(int stateType)
    {
        _gm = GetComponent<GameManager>();
        base.Initialize(stateType);
    }
    public override void OnEnter()
    {
        // 施政メニューの選択を可能にする（城選択可能）
        Debug.Log($"[{this.name}]");

    }
    public override void OnExit()
    {
        // 施政メニューの選択を不可にする（城選択不能）
    }

    public override int StateUpdate()
    {
        if (_gm.OnTurnEnd)
        {
        }
        return (int)GameManager.StateType.WaitInput;
    }
}
