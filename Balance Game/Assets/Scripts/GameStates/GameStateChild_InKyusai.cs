using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateChild_InKyusai : AbstractStateChild
{
    private GameManager _gm;
    private Oshiro _oshiro;
    private GameObject _commandCanvas;
    private KyusaiCanvas _kyusaiCanvas;

    private Mura _UshitoraMura;
    private Mura _InuiMura;
    private Mura _HitsujisaruMura;
    private Mura _TatsumiMura;

    private bool _isButtonEventOn;
    private string _buttonName;

    private Tokuten _tokutenPanel;
    private Koyomi _koyomi;
    private int _cost;

    public override void Initialize(int stateType)
    {
        _gm = GetComponent<GameManager>();
        _oshiro = GameObject.Find("Oshiro").GetComponent<Oshiro>();
        _commandCanvas = _oshiro.transform.Find("KyusaiCanvas").gameObject;
        _kyusaiCanvas = _commandCanvas.GetComponent<KyusaiCanvas>();
        _kyusaiCanvas.OnButton += ButtonEventHandler;

        _UshitoraMura = GameObject.Find("UshitoraMura").GetComponent<Mura>();
        _InuiMura = GameObject.Find("InuiMura").GetComponent<Mura>();
        _HitsujisaruMura = GameObject.Find("HitsujisaruMura").GetComponent<Mura>();
        _TatsumiMura = GameObject.Find("TatsumiMura").GetComponent<Mura>();

        _tokutenPanel = GameObject.FindAnyObjectByType<Tokuten>();
        _koyomi = GameObject.FindAnyObjectByType<Koyomi>();

        _commandCanvas.SetActive(false);
        
        base.Initialize(stateType);
    }
    public override void OnEnter()
    {
        // コスト計算
        _cost = _tokutenPanel.Kokudaka * 8 / 100;

        // 救済キャンバスを表示する
        Debug.Log($"[{this.name}] Enter InKyusai_State!");
        _commandCanvas.SetActive(true);
        _isButtonEventOn = false;

        if (_tokutenPanel.KobanCount >= _cost)
        {
            _kyusaiCanvas.Setup($"{_cost} の小判を消費します。", true);
        }
        else
        {
            _kyusaiCanvas.Setup($"{_cost} の小判が必要です。\n小判が足りません。", false);
        }
    }
    public override void OnExit()
    {
        // 救済キャンバスを消去する
        _commandCanvas.SetActive(false);
    }

    public override int StateUpdate()
    {
        if (_isButtonEventOn)
        {
            switch (_buttonName)
            {
                case "OkUshitora":
                    _UshitoraMura.DoKyusai(5.0f);
                    return (int)GameManager.StateType.Progress;

                case "OkInui":
                    _InuiMura.DoKyusai(5.0f);
                    return (int)GameManager.StateType.Progress;

                case "OkHitsujisaru":
                    _HitsujisaruMura.DoKyusai(5.0f);
                    return (int)GameManager.StateType.Progress;

                case "OkTatsumi":
                    _TatsumiMura.DoKyusai(5.0f);
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
