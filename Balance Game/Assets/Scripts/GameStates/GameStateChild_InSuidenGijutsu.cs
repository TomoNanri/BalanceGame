using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateChild_InSuidenGijutsu : AbstractStateChild
{
    private Oshiro _oshiro;
    private GameObject _commandCanvas;
    private SuidenGijutsuCanvas _suidengijutsu;

    private bool _isButtonEventOn;
    private string _buttonName;

    private Tokuten _tokutenPanel;
    private Koyomi _koyomi;
    private int _cost;

    public override void Initialize(int stateType)
    {
        _oshiro = GameObject.Find("Oshiro").GetComponent<Oshiro>();
        _commandCanvas = _oshiro.transform.Find("SuidenGijutsuCanvas").gameObject;
        _suidengijutsu = _commandCanvas.GetComponent<SuidenGijutsuCanvas>();
        _suidengijutsu.OnButton += ButtonEventHandler;

        _tokutenPanel = GameObject.FindAnyObjectByType<Tokuten>();
        _koyomi = GameObject.FindAnyObjectByType<Koyomi>();

        _commandCanvas.SetActive(false);

        base.Initialize(stateType);
    }
    public override void OnEnter()
    {
        // ���c�Z�p�J���L�����o�X��\������
        Debug.Log($"[{this.name}] Enter InSuidenGijutsu_State!");
        _commandCanvas.SetActive(true);
        _isButtonEventOn = false;


        if (_oshiro.TaLevel <= _oshiro.LevelMax)
        {
            if (_koyomi.IsTaLevelUpOrdered)
            {
                _suidengijutsu.Setup($"���c�Z�p�J���͔N�Ɉ��ł��B", false);
            }
            else
            {
                _cost = _oshiro.LevelList[_oshiro.TaLevel] * 64;
                if (_tokutenPanel.KobanCount >= _cost)
                {
                    _suidengijutsu.Setup($"{_cost} �̏���������܂��B", true);
                }
                else
                {
                    _suidengijutsu.Setup($"{_cost} �̏������K�v�ł��B\n����������܂���B", false);
                }
            }
        }
        else
        {
            _suidengijutsu.Setup($"����ȏ�̃��x���グ�͂ł��܂���B", false);
        }

    }
    public override void OnExit()
    {
        // ���c�Z�p�J���L�����o�X����������
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

                    // ���x���グ��\�񂷂�
                    _koyomi.IsTaLevelUpOrdered = true;

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
