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
        // 視察コスト計算
        _cost = _tokutenPanel.Kokudaka * 1 / 100;

        // コマンドキャンバスを表示する
        Debug.Log($"[{this.name}] Enter InShisatsu_State!");
        _isButtonEventOn = false;
        _commandCanvas.SetActive(true);

        if (_tokutenPanel.KobanCount >= _cost)
        {
            _shisatsuCanvas.Setup($"{_cost} の小判を消費します。", true);
        }
        else
        {
            _shisatsuCanvas.Setup($"{_cost} の小判が必要です。\n小判が足りません。", false);
        }
    }
    public override void OnExit()
    {
        // コマンドキャンバスを消去する
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
                // 小判を消費する
                _tokutenPanel.UseKoban(_cost);

                // お殿様が視察モーションを実行する（EndEventは視察モーション内）
                Debug.Log($"[{name}] 視察開始！ {_buttonName}方面");

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
