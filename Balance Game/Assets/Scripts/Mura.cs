using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class Mura : MonoBehaviour
{
    public bool IsIkkiJoutai
    {
        get { return _ikkiNoumin != null ? true : false; }
    }

    [SerializeField]
    private int _satisfaction;
    [SerializeField]
    private int _MaxSatisfaction = 99;
    [SerializeField]
    private int _MinSatisfaction = 1;
    [SerializeField]
    private int _WarningSatisfactionLevel = 32;
    [SerializeField]
    private int _GoodSatisfactionLevel = 72;
    [SerializeField]
    private float alpha = 0.2f;
    [SerializeField]
    private int _thresholdOfIkki = 20; 

    [SerializeField]
    private GameObject[] _prefab;

    // �_���C���X�^���X
    private GameObject _normalNomin;
    private GameObject _ikkiNoumin;

    private GameObject _popUp;

    private GameManager _gm;
    private GodOfLuck _dice;
    private Oshiro _oshiro;

    private Koyomi _koyomi;
    private Nouchi _nouchi;

    private bool _isKyusaiRequested;
    private float _timeOfKyusai;
    private float _timer;

    // Start is called before the first frame update
    void Start()
    {
        _gm = FindAnyObjectByType<GameManager>();
        _gm.InitializeHandler += ResetMura;

        _oshiro = FindAnyObjectByType<Oshiro>();
        _dice = FindAnyObjectByType<GodOfLuck>();
        _koyomi = FindAnyObjectByType<Koyomi>();
        _nouchi = transform.Find("Nouchi").GetComponent<Nouchi>();

        // �_���i���ʁj�����J�n
        _normalNomin = Instantiate(_prefab[0], this.transform);

        // PopUp�ݒ�
        _isKyusaiRequested = false;

        // �����x������
        _satisfaction = 50;
    }

    // Update is called once per frame
    void Update()
    {
        _timer -= Time.deltaTime;
        if (_isKyusaiRequested)
        {
            _isKyusaiRequested = false;
            _timer = _timeOfKyusai;
            _popUp = Instantiate(_prefab[1], _oshiro.transform);
            var _koukaText = _popUp.transform.Find("Canvas2/Panel/Text").GetComponent<TextMeshProUGUI>();
            _koukaText.SetText($"�_���̖����x��\n{_oshiro.KyusaiKouka} ���������I");
        }
        if(_popUp != null && _timer < 0)
        {
            // PopUp�C�x���g���I�����AEvent�I����ʒm����
            Destroy(_popUp);
            _popUp = null;
            _oshiro.RaiseShisakuEnd(this, EventArgs.Empty);
        }
    }
    private void ResetMura()
    {
        if(_ikkiNoumin != null)
        {
            Destroy(_ikkiNoumin);
            _ikkiNoumin = null;
        }
        if(_normalNomin != null)
        {
            Destroy(_normalNomin);
        }

        // �_���i���ʁj�����J�n
        _normalNomin = Instantiate(_prefab[0], this.transform);

        // PopUp�ݒ�
        _isKyusaiRequested = false;
        if(_popUp != null)
        {
            Destroy(_popUp);
            _popUp = null;
        }

        // �����x������
        _satisfaction = 50;
    }
    private void StartIkki()
    {
        if (_normalNomin != null)
        {
            Destroy(_normalNomin);
            _normalNomin = null;
        }
        if (_ikkiNoumin == null)
        {
            _ikkiNoumin = Instantiate(_prefab[2], this.transform);
        }
    }
    private void StopIkki()
    {
        if (_ikkiNoumin != null)
        {
            Destroy(_ikkiNoumin);
            _ikkiNoumin = null;
        }
        if (_normalNomin == null)
        {
            _normalNomin = Instantiate(_prefab[0], this.transform);
        }
    }
    public bool CheckSatisfaction()
    {
        var _HatakePerformance = _nouchi.HatakeNoKazu * _oshiro.LevelList[_oshiro.HatakeLevel] * (100 + _koyomi.HatakeHosei) / 100;
        var _TaPerformance = _nouchi.TaNoKazu * _oshiro.LevelList[_oshiro.TaLevel] * (100 + _koyomi.TaHosei) / 100;
        float delta = (float)Math.Tanh((double)(alpha * (float)(_HatakePerformance - _TaPerformance) / 1000 / 2.0f)) * 6.0f;

        Debug.Log($"[{name}] _HatakePerformance={_HatakePerformance}, _TaPerformance={_TaPerformance}, tanh={Math.Tanh((double)(alpha * (float)(_HatakePerformance - _TaPerformance) / 1000 / 2.0f))}, delta={delta}");

        _satisfaction += Mathf.RoundToInt(delta);
        _satisfaction = Mathf.Min(_satisfaction, _MaxSatisfaction);
        _satisfaction = Mathf.Max(_satisfaction, _MinSatisfaction);

        Debug.Log($"[{name}] Satisfaction Checked! CS = {_satisfaction}");
        if (_satisfaction < _thresholdOfIkki)
        {
            StartIkki();
            return true;
        }
        else
        {
            StopIkki();
        }
        return false;
    }
    public string Response()
    {
        string responseText = "�������a���˂�"; 
        if (_dice.DiceCheckD100(_oshiro.Luck, 1))
        {
            if( _ikkiNoumin != null)
            {
                responseText = "���Ƃ̂��܂��A�z������ˁD�D�D";
            }
            else if(_satisfaction < _WarningSatisfactionLevel)
            {
                responseText = "�͂炪����������";
            }
            else if (_satisfaction > _GoodSatisfactionLevel)
            {
                responseText = "���a�l�Ɋ��ӂ��Ƃ��";
            }
            else
            {
                responseText = "�����������ˁH";
            }
        }
        return responseText;
    }
    public void StartMatsuri()
    {
        Debug.Log($"[{name}] �Ղ���n�߂܂��I");
        if(_normalNomin != null)
        {
            Destroy(_normalNomin);
        }
        _normalNomin = Instantiate(_prefab[3], this.transform);
        _koyomi.AddCalendarEvent((_koyomi.Tsuki + 2) % 12, StopMatsuri);
        _satisfaction += _oshiro.MatsuriKouka;    
    }
    public void StopMatsuri()
    {
        if (_normalNomin != null)
        {
            Destroy(_normalNomin);
        }
        _normalNomin = Instantiate(_prefab[0], this.transform);
    }

    public int PayNengu()
    {
        if(_koyomi.Tsuki != 9)
        {
            Debug.LogError($"[{name}] �P�O���ȊO�Ƀ{�^���������ꂽ�I");
        }
        return _nouchi.TaNoKazu * _oshiro.LevelList[_oshiro.TaLevel] * (100 + _koyomi.TaHosei) / 100;
    }
    public void DoKyusai(float sec)
    {
        _isKyusaiRequested = true;
        _timeOfKyusai = sec;
        _satisfaction += _oshiro.KyusaiKouka;
        _satisfaction = Mathf.Min(_satisfaction, _MaxSatisfaction);
    }
}
