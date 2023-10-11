using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateChild_InTahataHiritsu : AbstractStateChild
{
    private GameManager _gm;
    private Oshiro _oshiro;
    private GameObject _hiritsuCanvas;

    public override void Initialize(int stateType)
    {
        _gm = GetComponent<GameManager>();
        _oshiro = GameObject.Find("Oshiro").GetComponent<Oshiro>();
        _hiritsuCanvas = _oshiro.transform.Find("HiritsuCanvas").gameObject;
        _hiritsuCanvas.SetActive(false);
        base.Initialize(stateType);
    }
    public override void OnEnter()
    {
        // 比率メニューをアクティブにする
        Debug.Log($"[{this.name}] Enter !");
        _hiritsuCanvas.SetActive(true);
        _hiritsuCanvas.gameObject.GetComponentInChildren<TahataHiritsuPanel>().Activate("今は無制限");
    }
    public override void OnExit()
    {
        // 比率メニューをインアクティブにする
        _hiritsuCanvas.SetActive(false);
    }

    public override int StateUpdate()
    {

        return (int)_gm.StateByButton;
    }
}
