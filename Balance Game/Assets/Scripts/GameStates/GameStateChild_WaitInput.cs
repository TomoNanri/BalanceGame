using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateChild_WaitInput : AbstractStateChild
{
    private GameManager _gm;
    private Oshiro _oshiro;
    public override void Initialize(int stateType)
    {
        _gm = GetComponent<GameManager>();
        _oshiro = GameObject.Find("Oshiro").GetComponent<Oshiro>();
        base.Initialize(stateType);
    }
    public override void OnEnter()
    {
        // �{�����j���[�̑I�����\�ɂ���i��I���\�j
        Debug.Log($"[{this.name}] Enter WaitInput State!");

        _gm.StateByButton = GameManager.StateType.WaitInput;
        _oshiro.IsSelectable = true;
    }
    public override void OnExit()
    {
        // �{�����j���[�̑I����s�ɂ���i��I��s�\�j
        _oshiro.IsSelectable = false;
    }

    public override int StateUpdate()
    {
        return (int)_gm.StateByButton;
    }
}