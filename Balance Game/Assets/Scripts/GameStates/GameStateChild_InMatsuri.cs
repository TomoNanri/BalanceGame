using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameStateChild_InMatsuri : AbstractStateChild
{
    private Oshiro _oshiro;
    private GameObject _commandCanvas;
    private MatsuriCanvas _matsuriCanvas;

    private bool _isButtonEventOn;
    private string _buttonName;

    private AudioChanger _audioChanger;

    private Tokuten _tokutenPanel;
    private Koyomi _koyomi;
    private int _cost;

    public override void Initialize(int stateType)
    {
        _oshiro = GameObject.Find("Oshiro").GetComponent<Oshiro>();
        _commandCanvas = _oshiro.transform.Find("MatsuriCanvas").gameObject;
        _matsuriCanvas = _commandCanvas.GetComponent<MatsuriCanvas>();
        _matsuriCanvas.OnButton += ButtonEventHandler;

        _tokutenPanel = GameObject.FindAnyObjectByType<Tokuten>();
        _koyomi = GameObject.FindAnyObjectByType<Koyomi>();

        _audioChanger = FindAnyObjectByType<AudioChanger>();

        _commandCanvas.SetActive(false);
        base.Initialize(stateType);
    }
    public override void OnEnter()
    {
        // �R�X�g�v�Z
        _cost = _tokutenPanel.Kokudaka * 15 / 100;

        // �R�}���h�L�����o�X��\������
        Debug.Log($"[{this.name}] Enter InMatsuri State!");
        _isButtonEventOn = false;
        _commandCanvas.SetActive(true);

        if (!_koyomi.IsMatsuriDone) {
            if (_tokutenPanel.KobanCount >= _cost)
            {
                _matsuriCanvas.Setup($"{_cost} �̏���������܂��B", true);
            }
            else
            {
                _matsuriCanvas.Setup($"{_cost} �̏������K�v�ł��B\n����������܂���B", false);
            }
        }
        else
        {
            _matsuriCanvas.Setup($"�Ղ͋G�߂��ƂɂP�񂾂��ł��B", false);
        }
    }
    public override void OnExit()
    {
        // �R�}���h�L�����o�X����������
        _commandCanvas.SetActive(false);
    }
    public override int StateUpdate()
    {
        if (_isButtonEventOn)
        {
            switch (_buttonName)
            {
                case "Ok":
                    // �����������
                    _tokutenPanel.UseKoban(_cost);

                    // �Ճ��[�V�������J�n����
                    foreach(Mura e in _oshiro.MuraList)
                    {
                        e.StartMatsuri();
                    }

                    // BGM �؂�ւ�
                    _audioChanger.StopBGM();
                    _audioChanger.PlayBGM(AudioChanger.BGMType.Matsuri);

                    // ���X���ɓ���^�C�~���O�� Normal BGM �֖߂�
                    _koyomi.AddCalendarEvent((_koyomi.Tsuki + 2) % 12, StopMatsuri);

                    // ���̋G�߂̍Ղ͎��{�ς݂ɂ���B
                    _koyomi.IsMatsuriDone = true;

                    // �㑱���[�V�����������̂ŃC�x���g�I���ɂ���B�i�Տ�Ԃ͗����܂łÂ��j
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
    private void StopMatsuri()
    {
        _audioChanger.StopBGM();
        _audioChanger.PlayBGM(AudioChanger.BGMType.Normal);
    }
}
