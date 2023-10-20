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
        // コスト計算
        _cost = _tokutenPanel.Kokudaka * 15 / 100;

        // コマンドキャンバスを表示する
        Debug.Log($"[{this.name}] Enter InMatsuri State!");
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
                    foreach(Mura e in _oshiro.MuraList)
                    {
                        e.StartMatsuri();
                    }

                    // BGM 切り替え
                    _audioChanger.StopBGM();
                    _audioChanger.PlayBGM(AudioChanger.BGMType.Matsuri);

                    // 翌々月に入るタイミングで Normal BGM へ戻す
                    _koyomi.AddCalendarEvent((_koyomi.Tsuki + 2) % 12, StopMatsuri);

                    // この季節の祭は実施済みにする。
                    _koyomi.IsMatsuriDone = true;

                    // 後続モーションが無いのでイベント終了にする。（祭状態は翌月までつづく）
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
