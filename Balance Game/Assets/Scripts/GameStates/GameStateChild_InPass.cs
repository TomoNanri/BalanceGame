using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameStateChild_InPass : AbstractStateChild
{
    private GameManager _gm;
    private Oshiro _oshiro;
    private GameObject _commandCanvas;
    private PassCanvas _passCanvas;
    private GodOfLuck _godOfDice;

    private int _nengu;

    public override void Initialize(int stateType)
    {
        _gm = GetComponent<GameManager>();
        _godOfDice = GameObject.FindAnyObjectByType<GodOfLuck>();
        _oshiro = GameObject.Find("Oshiro").GetComponent<Oshiro>();
        _commandCanvas = _oshiro.transform.Find("PassCanvas").gameObject;
        _passCanvas = _commandCanvas.GetComponent<PassCanvas>();

        // パスキャンバスを不可視にする
        _commandCanvas.SetActive(false);
        base.Initialize(stateType);

    }
    public override void OnEnter()
    {
        // パスキャンバスを表示する
        Debug.Log($"[{this.name}] Enter Pass_State!");
        _passCanvas.Setup("");
        _commandCanvas.SetActive(true);
    }
    public override void OnExit()
    {
        // パスキャンバスを消去する
        _commandCanvas.SetActive(false);
    }
    public override int StateUpdate()
    {
        if (_passCanvas.OnButtonName != null)
        {
            switch (_passCanvas.OnButtonName)
            {
                case "Ok":
                    // 幸運1d100 成功での幸運値を１上昇
                    if (_godOfDice.DiceCheckD100(_oshiro.Luck, 1))
                        if (_oshiro.Luck < 96)
                            _oshiro.Luck++;

                    // 後続モーションが無いのでイベント終了にする。
                    _oshiro.RaiseShisakuEnd(this, EventArgs.Empty);

                    return (int)GameManager.StateType.Progress;

                case "Cancel":
                    return (int)GameManager.StateType.InMainMenu;

                default:
                    Debug.LogError($"[{name}] Undefind button found.");
                    return (int)StateType;
            }
        }
        return (int)StateType;
    }
}
