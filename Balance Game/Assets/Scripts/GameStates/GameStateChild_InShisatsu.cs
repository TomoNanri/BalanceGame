using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameStateChild_InShisatsu : AbstractStateChild
{
    private Oshiro _oshiro;
    private GameObject _commandCanvas;
    private ShisatsuCanvas _shisatsuCanvas;

    private bool _isButtonEventOn;
    private string _buttonName;

    private Tokuten _tokutenPanel;
    private Koyomi _koyomi;
    private int _cost;

    public override void Initialize(int stateType)
    {
        _oshiro = GameObject.Find("Oshiro").GetComponent<Oshiro>();
        _commandCanvas = _oshiro.transform.Find("ShisatsuCanvas").gameObject;
        _shisatsuCanvas = _commandCanvas.GetComponent<ShisatsuCanvas>();
        Debug.Log($"[{name}] _shisatsuCanvas = {_shisatsuCanvas}");
        _shisatsuCanvas.OnButton += ButtonEventHandler;

        _tokutenPanel = GameObject.FindAnyObjectByType<Tokuten>();
        _koyomi = GameObject.FindAnyObjectByType<Koyomi>();

        _commandCanvas.SetActive(false);
        base.Initialize(stateType);
    }
    public override void OnEnter()
    {
        // ���@�R�X�g�v�Z
        _cost = _tokutenPanel.Kokudaka * 1 / 100;

        // �R�}���h�L�����o�X��\������
        Debug.Log($"[{this.name}] Enter InShisatsu_State!");
        _isButtonEventOn = false;
        _commandCanvas.SetActive(true);

        if (_tokutenPanel.KobanCount >= _cost)
        {
            _shisatsuCanvas.Setup($"{_cost} �̏���������܂��B", true);
        }
        else
        {
            _shisatsuCanvas.Setup($"{_cost} �̏������K�v�ł��B\n����������܂���B", false);
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
            if (_buttonName == "Cancel")
            {
                return (int)GameManager.StateType.InMainMenu;
            }
            else {
                // �����������
                _tokutenPanel.UseKoban(_cost);

                // ���a�l�����@���[�V���������s����iEndEvent�͎��@���[�V�������j
                Debug.Log($"[{name}] ���@�J�n�I {_buttonName}����");

                switch (_buttonName)
                {
                    case "GoNorth":
                        GameObject.Find("Kitakaidou").GetComponent<Kaidou>().StartShisatsu();
                        break;

                    case "GoEast":
                        GameObject.Find("Higasikaidou").GetComponent<Kaidou>().StartShisatsu();
                        break;

                    case "GoSouth":
                        GameObject.Find("Minamikaidou").GetComponent<Kaidou>().StartShisatsu();
                        break;

                    case "GoWest":
                        GameObject.Find("Nisikaidou").GetComponent<Kaidou>().StartShisatsu();
                        break;

                    case "Cancel":
                        break;

                    default:
                        Debug.LogError($"[{name}] Undefind button found.");
                        break;
                }

                return (int)GameManager.StateType.Progress;
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
