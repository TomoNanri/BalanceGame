using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateChild_InNengu : AbstractStateChild
{
    private GameManager _gm;
    private Oshiro _oshiro;
    private GameObject _commandCanvas;

    public override void Initialize(int stateType)
    {
        _gm = GetComponent<GameManager>();
        _oshiro = GameObject.Find("Oshiro").GetComponent<Oshiro>();
        _commandCanvas = _oshiro.transform.Find("NenguCanvas").gameObject;
        _commandCanvas.SetActive(false);
        base.Initialize(stateType);
    }
    public override void OnEnter()
    {
        // 救済キャンバスを表示する
        Debug.Log($"[{this.name}] Enter InNengu_State!");
        _commandCanvas.SetActive(true);
    }
    public override void OnExit()
    {
        // 救済キャンバスを消去する
        _commandCanvas.SetActive(false);
    }

    public override int StateUpdate()
    {
        return (int)_gm.StateByButton;
    }
}
