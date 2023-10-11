using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateChild_Loading : AbstractStateChild
{
    private GameManager _gm;
    public override void Initialize(int stateType)
    {
        _gm = GetComponent<GameManager>();
        base.Initialize(stateType);
    }
    public override void OnEnter()
    {
        // PlayerPrefsÇÃì«Ç›çûÇ›
        Debug.Log($"[{this.name}] Enter Loading State!");
    }
    public override void OnExit()
    {
        // None
    }

    public override int StateUpdate()
    {
        return (int)GameManager.StateType.WaitInput;
    }
}
