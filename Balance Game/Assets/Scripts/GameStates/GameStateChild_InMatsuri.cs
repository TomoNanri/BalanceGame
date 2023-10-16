using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateChild_InMatsuri : AbstractStateChild
{
    private GameManager _gm;
    private Oshiro _oshiro;
    private GameObject _commandCanvas;
    private MatsuriCanvas _matsuriCanvas;

    private bool _isButtonEventOn;
    private string _buttonName;

    private Tokuten _tokutenPanel;
    private Koyomi _koyomi;
    private int _cost;

    public override void Initialize(int stateType)
    {
        _gm = GetComponent<GameManager>();
        _oshiro = GameObject.Find("Oshiro").GetComponent<Oshiro>();
        _commandCanvas = _oshiro.transform.Find("MatsuriCanvas").gameObject;
        _matsuriCanvas = _commandCanvas.GetComponent<MatsuriCanvas>();
        _matsuriCanvas.OnButton += ButtonEventHandler;

        _tokutenPanel = GameObject.FindAnyObjectByType<Tokuten>();
        _koyomi = GameObject.FindAnyObjectByType<Koyomi>();
        
        _commandCanvas.SetActive(false);
        base.Initialize(stateType);
    }
    public override void OnEnter()
    {
        // コスト計算
        _cost = _tokutenPanel.Kokudaka * 15 / 100;

        // コマンドキャンバスを表示する
        Debug.Log($"[{this.name}] Enter InKyusai_State!");
        _isButtonEventOn = false;
        _commandCanvas.SetActive(true);

        if (!_koyomi.IsMatsuriDone) {
            if (_tokutenPanel.KobanCount >= _cost)
            {
                _matsuriCanvas.Setup($"{_cost} の小判を消費します。", true);
            }
            else
            {
                _matsuriCanvas.Setup($"{_cost} の小判が必要です。\n小判が足りません。", false);
            }
        }
        else
        {
            _matsuriCanvas.Setup($"祭は季節ごとに１回だけです。", false);
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
            switch (_buttonName)
            {
                case "Ok":
                    // 小判を消費する
                    _tokutenPanel.UseKoban(_cost);

                    // 祭モーションを開始する
                    Debug.Log($"[{name}] 祭開催中");
                    _koyomi.IsMatsuriDone = true;

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
