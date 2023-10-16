using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateChild_InNouguKounyuu : AbstractStateChild
{
    private GameManager _gm;
    private Oshiro _oshiro;
    private GameObject _commandCanvas;
    private NouguKounyuuCanvaas _nouguKounyuuCanvas;

    private bool _isButtonEventOn;
    private string _buttonName;

    private Tokuten _tokutenPanel;
    private Koyomi _koyomi;
    private int _cost;

    public override void Initialize(int stateType)
    {
        _cost = 5000;

        _gm = GetComponent<GameManager>();
        _oshiro = GameObject.Find("Oshiro").GetComponent<Oshiro>();
        _commandCanvas = _oshiro.transform.Find("NouguKounyuuCanvas").gameObject;
        _nouguKounyuuCanvas = _commandCanvas.GetComponent<NouguKounyuuCanvaas>();
        _nouguKounyuuCanvas.OnButton += ButtonEventHandler;

        _tokutenPanel = GameObject.FindAnyObjectByType<Tokuten>();
        _koyomi = GameObject.FindAnyObjectByType<Koyomi>();

        _commandCanvas.SetActive(false);

        base.Initialize(stateType);
    }
    public override void OnEnter()
    {
        // 農具購入キャンバスを表示する
        Debug.Log($"[{this.name}] Enter InKyusai_State!");
        _commandCanvas.SetActive(true);

        if (_tokutenPanel.KobanCount >= _cost)
        {
            _nouguKounyuuCanvas.Setup($"{_cost} の小判を消費します。", true);
        }
        else
        {
            _nouguKounyuuCanvas.Setup($"{_cost} の小判が必要です。\n小判が足りません。", false);
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

                    // レベル上げを予約する
                    _koyomi.PurchaseNougu();

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
