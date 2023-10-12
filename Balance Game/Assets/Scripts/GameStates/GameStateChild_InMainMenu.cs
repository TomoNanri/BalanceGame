using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateChild_InMainMenu : AbstractStateChild
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
        Debug.Log($"[{this.name}] Enter InMainMenu State!");

        // �{�^���C�x���g��M�ϐ���������
        _gm.StateByButton = GameManager.StateType.InMainMenu;

        // �{�����j���[��\������
        _oshiro.ShowMainMenu(true);
    }
    public override void OnExit()
    {
        // �{�����j���[������
        _oshiro.ShowMainMenu(false);
    }

    public override int StateUpdate()
    {
        return (int)_gm.StateByButton;
    }
}
