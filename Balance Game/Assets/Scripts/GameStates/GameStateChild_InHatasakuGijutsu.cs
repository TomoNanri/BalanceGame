using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateChild_InHatasakuGijutsu : AbstractStateChild
{
    private Oshiro _oshiro;
    private GameObject _commandCanvas;
    private HatasakuGijutsuCanvas _hatasakugijutsu;

    private bool _isButtonEventOn;
    private string _buttonName;

    private Tokuten _tokutenPanel;
    private Koyomi _koyomi;
    private int _cost;
    private List<Nouchi> _nouchi = new List<Nouchi>();


    public override void Initialize(int stateType)
    {
        //_gm = GetComponent<GameManager>();
        _oshiro = GameObject.Find("Oshiro").GetComponent<Oshiro>();
        _commandCanvas = _oshiro.transform.Find("HatasakuGijutsuCanvas").gameObject;
        _hatasakugijutsu = _commandCanvas.GetComponent<HatasakuGijutsuCanvas>();
        _hatasakugijutsu.OnButton += ButtonEventHandler;

        _tokutenPanel = GameObject.FindAnyObjectByType<Tokuten>();
        _koyomi = GameObject.FindAnyObjectByType<Koyomi>();

        _nouchi.Clear();
        _nouchi.Add(GameObject.Find("UshitoraMura/Nouchi").GetComponent<Nouchi>());
        _nouchi.Add(GameObject.Find("InuiMura/Nouchi").GetComponent<Nouchi>());
        _nouchi.Add(GameObject.Find("HitsujisaruMura/Nouchi").GetComponent<Nouchi>());
        _nouchi.Add(GameObject.Find("TatsumiMura/Nouchi").GetComponent<Nouchi>());

        _commandCanvas.SetActive(false);

        base.Initialize(stateType);
    }
    public override void OnEnter()
    {
        // 畑作技術開発キャンバスを表示する
        Debug.Log($"[{this.name}] Enter InHatasakuGijutsu_State!");
        _commandCanvas.SetActive(true);
        _isButtonEventOn = false;

        if (_oshiro.HatakeLevel <= _oshiro.LevelMax)
        {
                _cost = _oshiro.LevelList[_oshiro.HatakeLevel] * 64;
                if (_tokutenPanel.KobanCount >= _cost)
                {
                    _hatasakugijutsu.Setup($"{_cost} の小判を消費します。", true);
                }
                else
                {
                    _hatasakugijutsu.Setup($"{_cost} の小判が必要です。\n小判が足りません。", false);
                }
        }
        else
        {
            _hatasakugijutsu.Setup($"これ以上のレベル上げはできません。", false);
        }
    }
    public override void OnExit()
    {
        // 畑作技術開発キャンバスを消去する
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

                    // レベル上げを行う
                    _oshiro.HatakeLevel++;

                    // 各村の農地表示を更新
                    foreach(Nouchi n in _nouchi)
                    {
                        n.SetUpdateFlag();
                    }

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
