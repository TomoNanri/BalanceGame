using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateChild_WaitInput : AbstractStateChild
{
    private GameManager _gm;
    public override void Initialize(int stateType)
    {
        _gm = GetComponent<GameManager>();
        base.Initialize(stateType);
    }
    public override void OnEnter()
    {
        // �{�����j���[�̑I�����\�ɂ���i��I���\�j
        Debug.Log($"[{this.name}]");

    }
    public override void OnExit()
    {
        // �{�����j���[�̑I����s�ɂ���i��I��s�\�j
    }

    public override int StateUpdate()
    {
        if (_gm.OnTurnEnd)
        {
        }
        return (int)GameManager.StateType.WaitInput;
    }
}
