using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateChild_InNouguKounyuu : AbstractStateChild
{
    private GameManager _gm;
    private Oshiro _oshiro;
    public override void Initialize(int stateType)
    {
        _gm = GetComponent<GameManager>();
        _oshiro = GameObject.Find("Oshiro").GetComponent<Oshiro>();
        base.Initialize(stateType);
    }
    public override void OnEnter()
    {
        //// 施政メニューの選択を可能にする（城選択可能）
        //Debug.Log($"[{this.name}] Enter WaitInput State!");
        //_oshiro.IsSelectable = true;
    }
    public override void OnExit()
    {
        //// 施政メニューの選択を不可にする（城選択不能）
        //_oshiro.IsSelectable = false;
    }

    public override int StateUpdate()
    {
        //if (_gm.OnTurnEnd)
        //{
        //}
        return (int)GameManager.StateType.WaitInput;
    }

}
