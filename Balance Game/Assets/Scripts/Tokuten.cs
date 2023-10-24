using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Tokuten : MonoBehaviour
{
    // 国の石高
    public int Kokudaka => _kokudaka;

    // 小判の量
    public int KobanCount => _kobanCount;

    // 幸運値
    public int Luck { get; set; }

    [SerializeField]
    private int _kokudaka = 10000;
    private int _previousKokudaka;

    [SerializeField]
    private int _kobanCount = 10000;
    private int _previousKoban;

    // 石高変化時のイベントハンドラ
    public Action<KokudakaEventArgs> KokudakaEvent;

    // 小判数変動時のイベントハンドラ

    private GameManager _gm;
    private TextMeshProUGUI _kokudakaText;
    private TextMeshProUGUI _kobansuuText;

    // Start is called before the first frame update
    void Start()
    {
        _gm = GameObject.FindAnyObjectByType<GameManager>();
        _gm.InitializeHandler += ResetTokuten;
        _gm.SaveDataHandler += SaveData;
        _gm.LoadGameProc += LoadData;

        _kokudakaText = transform.Find("Canvas/KokudakaPanel/CountText").GetComponent<TextMeshProUGUI>();
        _kobansuuText = transform.Find("Canvas/KobanPanel/CountText").GetComponent<TextMeshProUGUI>();
        _previousKokudaka = _kokudaka;
        _previousKoban = _kobanCount;
    }

    // Update is called once per frame
    void Update()
    {
        // 石高増加を検知
        if(_kokudaka > _previousKokudaka)
        {
            _previousKokudaka = _kokudaka;

            // 石高パネルの更新
            _kokudakaText.SetText($"{Kokudaka}");
            if (KokudakaEvent != null)
            {
                KokudakaEvent.Invoke(new KokudakaEventArgs(_kokudaka));
            }
        }

        // 小判数変動を検知
        if(_kobanCount != _previousKoban)
        {
            _previousKoban = _kobanCount;

            // 小判数パネルの更新
            _kobansuuText.SetText($"{KobanCount}");
        }
    }
    private void ResetTokuten()
    {
        _kokudaka = 10000;
        _kobanCount = 10000;
        _previousKokudaka = 0;
        _previousKoban = 0;
    }
    private void SaveData()
    {
        PlayerPrefs.SetInt("_kokudaka", _kokudaka);
        PlayerPrefs.SetInt("_kobanCount", _kobanCount);
    }
    private void LoadData()
    {
        _kokudaka = PlayerPrefs.GetInt("_kokudaka", 10000);
        _kobanCount = PlayerPrefs.GetInt("_kobanCount", 20000);
    }
public void UpdateKokudaka(int kokudaka)
    {
        if(_kokudaka < kokudaka)
        {
            _kokudaka = kokudaka;
        }
        if(KokudakaEvent != null)
        {
            KokudakaEvent.Invoke(new KokudakaEventArgs(_kokudaka));
        }
    }
    public void AddKoban(int income)
    {
        _kobanCount += income;
    }
    public bool UseKoban(int cost)
    {
        if(_kobanCount < cost)
        {
            return false;
        }
        _kobanCount -= cost;
        return true;
    }
}
