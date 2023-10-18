using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateChild_InTahataHiritsu : AbstractStateChild
{
    private GameManager _gm;
    private Oshiro _oshiro;
    private GameObject _commandCanvas;
    private TahataHiritsuCanvas _tahataHiritsu;
    private bool _isButtonEventOn;
    private string _buttonName;

    public override void Initialize(int stateType)
    {
        _gm = GetComponent<GameManager>();
        _oshiro = GameObject.Find("Oshiro").GetComponent<Oshiro>();
        _commandCanvas = _oshiro.transform.Find("HiritsuCanvas").gameObject;
        _tahataHiritsu = _commandCanvas.GetComponent<TahataHiritsuCanvas>();
        _tahataHiritsu.OnButton += ButtonEventHandler;

        StartCoroutine(WaitForUpdate());
        
        base.Initialize(stateType);
    }
    IEnumerator WaitForUpdate()
    {
        Debug.Log($"[{name}] *InTahataHiritsu State Child is waiting firast update*");
        yield return new WaitForEndOfFrame();
        Debug.Log($"[{name}] *InTahataHiritsu State Child waiting Complete*");
        _commandCanvas.SetActive(false);
    }
    public override void OnEnter()
    {
        // 比率指定キャンバスを表示する
        Debug.Log($"[{this.name}] Enter InTahataHiritsu_State!");
        _commandCanvas.SetActive(true);
        _isButtonEventOn = false;
        _tahataHiritsu.Setup("今は無制限");
    }
    public override void OnExit()
    {
        // 比率指定キャンバスを消去する
        _commandCanvas.SetActive(false);
    }

    public override int StateUpdate()
    {
        if (_isButtonEventOn)
        {
            switch (_buttonName)
            {
                case "Ok":
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
