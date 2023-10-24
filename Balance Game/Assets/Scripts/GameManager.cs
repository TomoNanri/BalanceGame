using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : AbstractStateController
{
    public Action InitializeHandler;
    public Action SaveDataHandler;
    public enum StateType 
    {
        Initialize, 
        Intro,
        Loading, 
        WaitInput,
        InMainMenu,
        InNengu, 
        InTahataHiritsu, 
        InSuidenGijutsu, 
        InHatasakuGijutsu, 
        InNouguKounyuu, 
        InShisatsu, 
        InMatsuri,
        InKyusai,
        InPass,
        Progress, 
        GameOver
    }
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
        stateDic[(int)StateType.InMainMenu] = gameObject.AddComponent<GameStateChild_InMainMenu>();
        stateDic[(int)StateType.InMainMenu].Initialize((int)StateType.InMainMenu);
        stateDic[(int)StateType.InNengu] = gameObject.AddComponent<GameStateChild_InNengu>();
        stateDic[(int)StateType.InNengu].Initialize((int)StateType.InNengu);
        stateDic[(int)StateType.InTahataHiritsu] = gameObject.AddComponent<GameStateChild_InTahataHiritsu>();
        stateDic[(int)StateType.InTahataHiritsu].Initialize((int)StateType.InTahataHiritsu);
        stateDic[(int)StateType.InSuidenGijutsu] = gameObject.AddComponent<GameStateChild_InSuidenGijutsu>();
        stateDic[(int)StateType.InSuidenGijutsu].Initialize((int)StateType.InSuidenGijutsu);
        stateDic[(int)StateType.InHatasakuGijutsu] = gameObject.AddComponent<GameStateChild_InHatasakuGijutsu>();
        stateDic[(int)StateType.InHatasakuGijutsu].Initialize((int)StateType.InHatasakuGijutsu);
        stateDic[(int)StateType.InNouguKounyuu] = gameObject.AddComponent<GameStateChild_InNouguKounyuu>();
        stateDic[(int)StateType.InNouguKounyuu].Initialize((int)StateType.InNouguKounyuu);
        stateDic[(int)StateType.InShisatsu] = gameObject.AddComponent<GameStateChild_InShisatsu>();
        stateDic[(int)StateType.InShisatsu].Initialize((int)StateType.InShisatsu);
        stateDic[(int)StateType.InMatsuri] = gameObject.AddComponent<GameStateChild_InMatsuri>();
        stateDic[(int)StateType.InMatsuri].Initialize((int)StateType.InMatsuri);
        stateDic[(int)StateType.InKyusai] = gameObject.AddComponent<GameStateChild_InKyusai>();
        stateDic[(int)StateType.InKyusai].Initialize((int)StateType.InKyusai); 
        stateDic[(int)StateType.InPass] = gameObject.AddComponent<GameStateChild_InPass>();
        stateDic[(int)StateType.InPass].Initialize((int)StateType.InPass);
        stateDic[(int)StateType.Progress] = gameObject.AddComponent<GameStateChild_Progress>();
        stateDic[(int)StateType.Progress].Initialize((int)StateType.Progress);
        stateDic[(int)StateType.GameOver] = gameObject.AddComponent<GameStateChild_GameOver>();
        stateDic[(int)StateType.GameOver].Initialize((int)StateType.GameOver);

        CurrentState = initializeStateType;
        stateDic[CurrentState].OnEnter();
    }
    public bool IsOnBGM { get; set; }
    public bool IsOnSE { get; set; }
    public bool IsOnVoice { get; set; }
    public float SoundLevel { get; set; } = 0.5f;
    public bool OnNewGame = false;
    public bool OnLoadGame = false;

    public GameObject HoubiPrefab;

    // Load Game 選択時の処理を登録
    public Action LoadGameProc;

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

    // イントロ画面のボタンのハンドラ
    public void OnNewGameButton()
    {
        OnNewGame = true;
        Debug.Log($"New Game Button Pushued!");
    }
    public void OnLoadGameButton()
    {
        OnLoadGame = true;
        Debug.Log($"Load Game Button Pushued!");
    }
    public void OnExitButton()
    {
        Debug.Log($"Exit Game Button Pushued!");
        Application.Quit();
    }

    public void SaveData()
    {
        if(SaveDataHandler!= null)
        {
            SaveDataHandler.Invoke();
        }
    }
}
