using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Koyomi : MonoBehaviour
{
    public bool IsNewMonth { get; set; }
    public bool IsNenguAvailable { get; set; }
    public bool IsTaLevelUpOrdered { get; set; }
    public bool IsHatakeLevelUpOrdered { get; set; }
    public bool IsMatsuriDone { get; set; }
    public int Tsuki => _tsuki;
    public int Nen => _nen;

    private int _tsuki;
    private int _nen;

    [SerializeField]
    MonthStateController stateController = default;
    [SerializeField]
    private int _tsukisuu;

    private GameManager _gm;
    private MonthStateController _calendar;

    private TextMeshProUGUI _nengouText;
    private TextMeshProUGUI _tsukiText;
    private string[] _tsukimei = {"正月", "二月", "三月", "四月", "五月", "六月", "七月", "八月", "九月", "十月", "十一月", "十二月" };

    // Start is called before the first frame update
    void Start()
    {
        _calendar = transform.Find("State").GetComponent<MonthStateController>();

        _tsukisuu = 0;
        IsNewMonth = false;
        _gm = FindAnyObjectByType<GameManager>();
        _gm.LoadGameProc += LoadGame;

        // 暦キャンバスの初期化
        _nengouText = transform.Find("Canvas/Panel/NengouText").gameObject.GetComponent<TextMeshProUGUI>();
        _tsukiText = transform.Find("Canvas/Panel/TsukiText").gameObject.GetComponent<TextMeshProUGUI>();

        // state controller スタート
        stateController.Initialize((int)MonthStateController.StateType.January);
    }

    // Update is called once per frame
    void Update()
    {
        stateController.UpdateSequence();
    }
    public void ShowKoyomi()
    {

        _nen = _tsukisuu / 12 + 1;
        _tsuki = _tsukisuu % 12;

        if (_nen == 0)
        {
            _nengouText.SetText($"令〇 元年");
        }
        else
        {
            _nengouText.SetText($"令〇 {_nen}年");
        }
        _tsukiText.SetText($"{_tsukimei[_tsuki]}");
    }
    public void GoNextMonth()
    {
        _tsukisuu++;
        IsNewMonth = true;
    }
    public void AddCalendarEvent(int tsuki, Action action)
    {
        _calendar.AddCalendarEvent(tsuki, action);
    }
    public void LoadGame()
    {

    }
    public void PurchaseNougu()
    {
        Debug.LogError("農具購入は未実装！");
    }
}
