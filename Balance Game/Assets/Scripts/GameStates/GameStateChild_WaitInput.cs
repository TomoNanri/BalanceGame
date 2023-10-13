using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateChild_WaitInput : AbstractStateChild
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
        Debug.Log($"[{this.name}] Enter WaitInput State!");

        // ボタンイベント受信変数を初期化
        _gm.StateByButton = GameManager.StateType.WaitInput;

        // 施政メニューの選択を可能にする（城選択可能）
        _oshiro.IsSelectable = true;

        // ガイドメッセージを表示
        _oshiro.ShowGuide(true);
    }
    public override void OnExit()
    {
        // 施政メニューの選択を不可にする（城選択不能）
        _oshiro.IsSelectable = false;
        _oshiro.ShowGuide(false);
    }

    public override int StateUpdate()
    {
        return (int)_gm.StateByButton;
    }
}
