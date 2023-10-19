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

        // �p�X�L�����o�X��s���ɂ���
        _commandCanvas.SetActive(false);
        base.Initialize(stateType);

    }
    public override void OnEnter()
    {
        // �p�X�L�����o�X��\������
        Debug.Log($"[{this.name}] Enter Pass_State!");
        _passCanvas.Setup("");
        _commandCanvas.SetActive(true);
    }
    public override void OnExit()
    {
        // �p�X�L�����o�X����������
        _commandCanvas.SetActive(false);
    }
    public override int StateUpdate()
    {
        if (_passCanvas.OnButtonName != null)
        {
            switch (_passCanvas.OnButtonName)
            {
                case "Ok":
                    // �K�^1d100 �����ł̍K�^�l���P�㏸
                    if (_godOfDice.DiceCheckD100(_oshiro.Luck, 1))
                        if (_oshiro.Luck < 96)
                            _oshiro.Luck++;

                    // �㑱���[�V�����������̂ŃC�x���g�I���ɂ���B
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
