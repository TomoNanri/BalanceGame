using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Tokuten : MonoBehaviour
{
    // ���̐΍�
    public int Kokudaka => _kokudaka;

    // �����̗�
    public int KobanCount => _kobanCount;

    // �K�^�l
    public int Luck { get; set; }

    [SerializeField]
    private int _kokudaka = 10000;
    private int _previousKokudaka;

    [SerializeField]
    private int _kobanCount = 10000;
    private int _previousKoban;

    // �΍��ω����̃C�x���g�n���h��
    public Action<KokudakaEventArgs> KokudakaEvent;

    // �������ϓ����̃C�x���g�n���h��

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
        // �΍����������m
        if(_kokudaka > _previousKokudaka)
        {
            _previousKokudaka = _kokudaka;

            // �΍��p�l���̍X�V
            _kokudakaText.SetText($"{Kokudaka}");
            if (KokudakaEvent != null)
            {
                KokudakaEvent.Invoke(new KokudakaEventArgs(_kokudaka));
            }
        }

        // �������ϓ������m
        if(_kobanCount != _previousKoban)
        {
            _previousKoban = _kobanCount;

            // �������p�l���̍X�V
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
