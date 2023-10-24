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
    public bool IsAbleToIncreaseTa { get; set; }
    public int Tsuki => _tsuki;
    public int Nen => _nen;
    public int Hiyori => _hiyori;
    public int TaHosei => _taHosei;
    public int HatakeHosei =>_hatakeHosei;

    [SerializeField]
    private int _hiyori = 0;
    [SerializeField]
    private int _taHosei = 0;
    [SerializeField]
    private int _hatakeHosei = 0;

    private int _tsuki;
    private int _nen;

    [SerializeField]
    MonthStateController stateController = default;
    [SerializeField]
    private int _tsukisuu;

    private GameManager _gm;
    private MonthStateController _calendar;

    private GodOfLuck _dice;

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
        _gm.InitializeHandler += ResetKoyomi;
        _gm.SaveDataHandler += SaveData;
        _gm.LoadGameProc += LoadData;

        _dice = FindAnyObjectByType<GodOfLuck>();

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
    private void ResetKoyomi()
    {
        IsNewMonth = false;
        IsNenguAvailable = false;
        IsTaLevelUpOrdered = false;
        IsHatakeLevelUpOrdered = false;
        IsMatsuriDone = false;
        _hiyori = 0;
        _taHosei = 0;
        _hatakeHosei = 0;
        _tsukisuu = 0;

        // 暦 state controller スタート
        stateController.Initialize((int)MonthStateController.StateType.January);
    }
    public void ShowKoyomi()
    {
        _nen = _tsukisuu / 12 + 1;
        _tsuki = _tsukisuu % 12;
        Debug.Log($"[{name}] Show Koyomi called! _tsukisuu={_tsukisuu}");

        if (_nen == 1)
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
    public void SetHiyori()
    {
        int[] _taHousakuTable = { 5, 10, 15, 20 };
        int[] _taKyousakuTable = { -5, -10, -15, -20 };
        int[] _hatakeHousakuTable = { 5, 10, 15 };
        int[] _hatakeKyousakuTable = { -5, -10, -15, -20, -25 };

        if(Nen%2 == 0)
        {
            _hiyori = 0;
        }
        else
        {
            _hiyori = _dice.DiceCheckD100(50,1) ? 1 : 2;
        }
        switch(_hiyori)
        {
            case 1:
                // 豊作補正
                _taHosei = _taHousakuTable[UnityEngine.Random.Range(0, 4)];
                _hatakeHosei = _hatakeHousakuTable[UnityEngine.Random.Range(0, 3)];
                break;

            case 2:
                // 凶作補正
                _taHosei = _taKyousakuTable[UnityEngine.Random.Range(0, 4)];
                _hatakeHosei = _hatakeKyousakuTable[UnityEngine.Random.Range(0, 5)];
                break;

            default:
                // 通常作
                _taHosei = 0;
                _hatakeHosei = 0;
                break;
        }

    }
    public void AddCalendarEvent(int tsuki, Action action)
    {
        _calendar.AddCalendarEvent(tsuki, action);
    }
    private void SaveData()
    {
        PlayerPrefs.SetInt("_tsukisuu", _tsukisuu);
        PlayerPrefs.SetInt("_taHosei", _taHosei);
        PlayerPrefs.SetInt("_hatakeHosei", _hatakeHosei);
    }
    private void LoadData()
    {
        _tsukisuu = PlayerPrefs.GetInt("_tsukisuu", 0);
        Debug.Log($"[{name}] _tsukisuu reload! {_tsukisuu}");
        _taHosei = PlayerPrefs.GetInt("_taHosei", 0);
        _hatakeHosei = PlayerPrefs.GetInt("_hatakeHosei", 0);
        ShowKoyomi();

        stateController.ResetState((int)_tsukisuu % 12);
    }
    public void PurchaseNougu()
    {
        int[] _taTable = { 18, 16, 14, 12, 10, 8, 6, 4, 2, 0, 0, 0 };
        _taHosei += _taTable[_tsuki];
    }
}
