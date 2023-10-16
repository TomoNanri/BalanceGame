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
        // �_��w���L�����o�X��\������
        Debug.Log($"[{this.name}] Enter InKyusai_State!");
        _commandCanvas.SetActive(true);

        if (_tokutenPanel.KobanCount >= _cost)
        {
            _nouguKounyuuCanvas.Setup($"{_cost} �̏���������܂��B", true);
        }
        else
        {
            _nouguKounyuuCanvas.Setup($"{_cost} �̏������K�v�ł��B\n����������܂���B", false);
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
            switch (_buttonName)
            {
                case "Ok":
                    // �����������
                    _tokutenPanel.UseKoban(_cost);

                    // ���x���グ��\�񂷂�
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
