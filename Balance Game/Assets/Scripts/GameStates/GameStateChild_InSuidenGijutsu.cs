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
        // 水田技術開発キャンバスを表示する
        Debug.Log($"[{this.name}] Enter InSuidenGijutsu_State!");
        _commandCanvas.SetActive(true);
        _isButtonEventOn = false;


        if (_oshiro.TaLevel <= _oshiro.LevelMax)
        {
            if (_koyomi.IsTaLevelUpOrdered)
            {
                _suidengijutsu.Setup($"水田技術開発は年に一回です。", false);
            }
            else
            {
                _cost = _oshiro.LevelList[_oshiro.TaLevel] * 64;
                if (_tokutenPanel.KobanCount >= _cost)
                {
                    _suidengijutsu.Setup($"{_cost} の小判を消費します。", true);
                }
                else
                {
                    _suidengijutsu.Setup($"{_cost} の小判が必要です。\n小判が足りません。", false);
                }
            }
        }
        else
        {
            _suidengijutsu.Setup($"これ以上のレベル上げはできません。", false);
        }

    }
    public override void OnExit()
    {
        // 水田技術開発キャンバスを消去する
        _commandCanvas.SetActive(false);
    }

    public override int StateUpdate()
    {
        if (_isButtonEventOn)
        {
            switch (_buttonName)
            {
                case "Ok":
                    // 小判を消費する
                    _tokutenPanel.UseKoban(_cost);

                    // レベル上げを予約する
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
