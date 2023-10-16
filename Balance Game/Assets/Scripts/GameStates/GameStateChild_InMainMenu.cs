using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameStateChild_InMainMenu : AbstractStateChild
{
    private Oshiro _oshiro;
    private GameObject _commandCanvas;
    private MainMenuCanvas _mainMenuCanvas;
    private bool _isButtonOn = false;
    private string _buttonName;
    public override void Initialize(int stateType)
    {
        _oshiro = GameObject.Find("Oshiro").GetComponent<Oshiro>();
        _commandCanvas = _oshiro.transform.Find("MainMenuCanvas").gameObject;
        _mainMenuCanvas = _commandCanvas.GetComponent<MainMenuCanvas>();
        _mainMenuCanvas.OnButton += OnMainMenuButton;

        _commandCanvas.SetActive(false);

        base.Initialize(stateType);
    }
    public override void OnEnter()
    {
        Debug.Log($"[{this.name}] Enter InMainMenu State!");

        // ボタンイベント受信変数を初期化
        _isButtonOn = false;
        _buttonName = default;

        // 施政メニューを表示する
        _commandCanvas.SetActive(true);
    }
    public override void OnExit()
    {
        // 施政メニューを消す
        _commandCanvas.SetActive(false);
    }

    public override int StateUpdate()
    {
        if (_isButtonOn)
        {
            switch (_buttonName)
            {
                case "Nengu":
                    return (int)GameManager.StateType.InNengu;

                case "TahataHiritsu":
                    return (int)GameManager.StateType.InTahataHiritsu;

                case "SuidenGijutsu":
                    return (int)GameManager.StateType.InSuidenGijutsu;

                case "HatasakuGijutsu":
                    return (int)GameManager.StateType.InHatasakuGijutsu;

                case "NouguKounyuu":
                    return (int)GameManager.StateType.InNouguKounyuu;

                case "Shisatsu":
                    return (int)GameManager.StateType.InShisatsu;

                case "Matsuri":
                    return (int)GameManager.StateType.InMatsuri;

                case "Kyusai":
                    return (int)GameManager.StateType.InKyusai;

                case "Pass":
                    return (int)GameManager.StateType.InPass;

                default:
                    Debug.LogError($"[{name}] Undefind button found.");
                    return (int)StateType;
            }
        }
        return (int)StateType;
    }
    private void OnMainMenuButton(object sender, ButtonEventArgs args)
    {
        _isButtonOn = true;
        _buttonName = args.ButtonName;
    }
}
