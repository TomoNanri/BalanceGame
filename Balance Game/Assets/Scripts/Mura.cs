using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Mura : MonoBehaviour
{
    public int Satisfaction;
    [SerializeField]
    private int _MaxSatisfaction = 99;
    [SerializeField]
    private int _MinSatisfaction = 1;
    [SerializeField]
    private float alpha = 0.0001f;
    [SerializeField]
    private int _thresholdOfIkki = 20; 

    [SerializeField]
    private GameObject[] _prefab;

    // 農民インスタンス
    private GameObject _normalNoumin;
    private GameObject _ikkiNoumin;

    private GameObject _popUp;

    private Oshiro _oshiro;
    private Koyomi _koyomi;
    private Nouchi _nouchi;

    private bool _isKyusaiRequested;
    private float _timeOgKyusai;
    private float _timer;

    private bool _isIkkiStarted;

    // Start is called before the first frame update
    void Start()
    {
        _oshiro = GameObject.FindAnyObjectByType<Oshiro>();
        _koyomi = GameObject.FindAnyObjectByType<Koyomi>();
        _nouchi = transform.Find("Nouchi").GetComponent<Nouchi>();

        // 農民（普通）活動開始
        _normalNoumin = Instantiate(_prefab[0], this.transform);

        // PopUp設定
        _isKyusaiRequested = false;

        // 満足度初期化
        Satisfaction = 50;
    }

    // Update is called once per frame
    void Update()
    {
        _timer -= Time.deltaTime;
        if (_isIkkiStarted && _normalNoumin != null)
        {
            Destroy(_normalNoumin);
            _normalNoumin = null;
            _ikkiNoumin = Instantiate(_prefab[2], this.transform);
        }
        if (_isKyusaiRequested)
        {
            _isKyusaiRequested = false;
            _timer = _timeOgKyusai;
            _popUp = Instantiate(_prefab[1], _oshiro.transform);
        }
        if(_popUp != null && _timer < 0)
        {
            Destroy(_popUp);
            _popUp = null;
        }
    }
    public bool CheckSatisfaction()
    {
        _isIkkiStarted = false;
        var _HatakePerformance = _nouchi.HatakeNoKazu * _oshiro.LevelList[_oshiro.HatakeLevel];
        var _TaPerformance = _nouchi.TaNoKazu * _oshiro.LevelList[_oshiro.TaLevel];
        float delta = (float)Math.Tanh((double)(alpha * (float)(_HatakePerformance - _TaPerformance) / 2.0f)) * 6.0f;

        Debug.Log($"[{name}] _HatakePerformance={_HatakePerformance}, _TaPerformance={_TaPerformance}, tanh={Math.Tanh((double)(alpha * (float)(_HatakePerformance - _TaPerformance) / 2.0f))}, delta={delta}");

        Satisfaction += Mathf.RoundToInt(delta);
        Satisfaction = Mathf.Min(Satisfaction, _MaxSatisfaction);
        Satisfaction = Mathf.Max(Satisfaction, _MinSatisfaction);

        Debug.Log($"[{name}] Satisfaction Checked! CS = {Satisfaction}");
        if(Satisfaction < _thresholdOfIkki)
        {
            _isIkkiStarted = true;
            return true;
        }
        return false;
    }
    public void StopIkki()
    {
        if(_normalNoumin == null)
        {
            if (_ikkiNoumin != null)
            {
                Destroy(_ikkiNoumin);
                _ikkiNoumin = null;
            }
            _normalNoumin = Instantiate(_prefab[0], this.transform);
        }
    }
    public int PayNengu()
    {
        if(_koyomi.Tsuki != 9)
        {
            Debug.LogError($"[{name}] １０月以外にボタンが押された！");
            //return 0;
        }
        return _nouchi.TaNoKazu * _oshiro.LevelList[_oshiro.TaLevel];
    }
    public void DoKyusai(float sec)
    {
        _isKyusaiRequested = true;
        _timeOgKyusai = sec;
        Satisfaction += _oshiro.KyusaiKouka;
        Satisfaction = Mathf.Min(Satisfaction, _MaxSatisfaction);
    }
}
