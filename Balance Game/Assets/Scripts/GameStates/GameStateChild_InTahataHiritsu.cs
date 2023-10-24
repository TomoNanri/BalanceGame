using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameStateChild_InTahataHiritsu : AbstractStateChild
{
    private GameManager _gm;
    private Oshiro _oshiro;
    private Koyomi _koyomi;
    private GameObject _commandCanvas;
    private TahataHiritsuCanvas _tahataHiritsu;
    private bool _isButtonEventOn;
    private string _buttonName;

    public override void Initialize(int stateType)
    {
        _gm = GetComponent<GameManager>();
        _oshiro = GameObject.Find("Oshiro").GetComponent<Oshiro>();
        _koyomi = GameObject.Find("Koyomi").GetComponent<Koyomi>();
        _commandCanvas = _oshiro.transform.Find("HiritsuCanvas").gameObject;
        _tahataHiritsu = _commandCanvas.GetComponent<TahataHiritsuCanvas>();
        _tahataHiritsu.OnButton += ButtonEventHandler;

        _commandCanvas.SetActive(false);

        base.Initialize(stateType);
    }
    public override void OnEnter()
    {
        // �䗦�w��L�����o�X��\������
        Debug.Log($"[{this.name}] Enter InTahataHiritsu_State!");
        _commandCanvas.SetActive(true);
        _isButtonEventOn = false;
        _tahataHiritsu.Setup(_koyomi.IsAbleToIncreaseTa?"���͖�����" :"���͓c�𑝂₹�܂���", !_koyomi.IsAbleToIncreaseTa);
    }
    public override void OnExit()
    {
        // �䗦�w��L�����o�X����������
        _commandCanvas.SetActive(false);
    }

    public override int StateUpdate()
    {
        if (_isButtonEventOn)
        {
            switch (_buttonName)
            {
                case "Ok":
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
    private void ButtonEventHandler(object sender, ButtonEventArgs args)
    {
        _isButtonEventOn = true;
        _buttonName = args.ButtonName;
    }
}
