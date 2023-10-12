using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateChild_InMainMenu : AbstractStateChild
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
        Debug.Log($"[{this.name}] Enter InMainMenu State!");

        // ボタンイベント受信変数を初期化
        _gm.StateByButton = GameManager.StateType.InMainMenu;

        // 施政メニューを表示する
        _oshiro.ShowMainMenu(true);
    }
    public override void OnExit()
    {
        // 施政メニューを消す
        _oshiro.ShowMainMenu(false);
    }

    public override int StateUpdate()
    {
        return (int)_gm.StateByButton;
    }
}
