using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : AbstractStateController
{
    public enum StateType {Initialize, Intro, Loading, WaitInput, Progress, GameOver }
    public override void Initialize(int initializeStateType)
    {
        stateDic[(int)StateType.Initialize] = gameObject.AddComponent<GameStateChild_Initialize>();
        stateDic[(int)StateType.Initialize].Initialize((int)StateType.Initialize);
        stateDic[(int)StateType.Intro] = gameObject.AddComponent<GameStateChild_Intro>();
        stateDic[(int)StateType.Intro].Initialize((int)StateType.Intro);
        stateDic[(int)StateType.Loading] = gameObject.AddComponent<GameStateChild_Loading>();
        stateDic[(int)StateType.Loading].Initialize((int)StateType.Loading);
        stateDic[(int)StateType.WaitInput] = gameObject.AddComponent<GameStateChild_WaitInput>();
        stateDic[(int)StateType.WaitInput].Initialize((int)StateType.WaitInput);
    }
    public float SoundLevel { get; set; }
    public bool OnNewGame = false;
    public bool OnLoadGame = false;
    public bool OnTurnEnd = false;

    // Start is called before the first frame update
    void Start()
    {
        // ステートマシンの初期化
        Initialize((int)StateType.Initialize);
    }

    // Update is called once per frame
    void Update()
    {
        // ステートマシンの更新
        this.UpdateSequence();
    }
    public void OnNewGameButton()
    {
        OnNewGame = true;
    }
    public void OnLoadGameButton()
    {
        OnLoadGame = true;
    }
    public void OnExitButton()
    {
        Application.Quit();
    }
}
