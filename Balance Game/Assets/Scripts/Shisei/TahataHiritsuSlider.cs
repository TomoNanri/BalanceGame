using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TahataHiritsuSlider : MonoBehaviour
{
    private int _newTaNoKazu;
    private int _newHatakeNoKazu;
    private string _muramei;
    private GameManager _gm;
    private Nouchi _nouchi;
    private Slider _slider;
    private TextMeshProUGUI _taText;
    private TextMeshProUGUI _hatakeText;
    private bool _isSliderChanged = false;

    // Start is called before the first frame update
    void Start()
    {
        //_muramei = this.name;
        //_nouchi = GameObject.Find($"{_muramei}/Nouchi").GetComponent<Nouchi>();

        //_taText = transform.Find("TanokazuText").GetComponent<TextMeshProUGUI>();
        //_hatakeText = transform.Find("HatakenokazuText").GetComponent<TextMeshProUGUI>();

        //_slider = transform.Find("Hiritsu_Slider").gameObject.GetComponent<Slider>();

        //Debug.Log($"[{name}] _nouchiの田の数={_nouchi.TaNoKazu}　畑の数={_nouchi.HatakeNoKazu}");


        //// 田畑比率変更イベントのハンドラ登録
        //GameObject.Find("HiritsuCanvas").GetComponent<TahataHiritsuCanvas>().OkEvent += Execute;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnEnable()
    {
        _muramei = this.name;
        _nouchi = GameObject.Find($"{_muramei}/Nouchi").GetComponent<Nouchi>();

        _taText = transform.Find("TanokazuText").GetComponent<TextMeshProUGUI>();
        _hatakeText = transform.Find("HatakenokazuText").GetComponent<TextMeshProUGUI>();

        _slider = transform.Find("Hiritsu_Slider").gameObject.GetComponent<Slider>();

        Debug.Log($"[{name}] _nouchiの田の数={_nouchi.TaNoKazu}　畑の数={_nouchi.HatakeNoKazu}");

    }
    public void SetSliderValue()
    {
        Debug.Log($"[{_muramei}] スライダー設定が呼ばれた！{_nouchi.TaNoKazu}：{_slider.value}：{_nouchi.HatakeNoKazu}");

        // Slider 初期値設定
        _slider.value = (float)_nouchi.HatakeNoKazu / (float)_nouchi.NouchiMaxCount;

        Debug.Log($"[{_muramei}] スライダー値がセットされた！{_nouchi.TaNoKazu}：{_slider.value}：{_nouchi.HatakeNoKazu}");

        // Slider 初期値をインジケータに設定
        _taText.SetText($"田{_nouchi.TaNoKazu}");
        _hatakeText.SetText($"畑{_nouchi.HatakeNoKazu}");
        // 田畑比率変更イベントのハンドラ登録
        GameObject.Find("HiritsuCanvas").GetComponent<TahataHiritsuCanvas>().OkEvent += Execute;
    }
    public void LockMinValue()
    {
        _slider.minValue = _slider.value;
    }
    public void UnlockMinValue()
    {
        _slider.minValue = 0;
    }
    public void OnChangeValue()
    {
        Debug.Log($"[{_muramei}] Slider の値が変わった！{_slider.value}");

        // Slider の値に合わせてインジケータを暫定値に変更
        _newHatakeNoKazu = (int)(_slider.value * _nouchi.NouchiMaxCount);
        _newTaNoKazu = _nouchi.NouchiMaxCount - _newHatakeNoKazu;
        _taText.SetText($"田{_newTaNoKazu}");
        _hatakeText.SetText($"畑{_newHatakeNoKazu}");

        _isSliderChanged = true;
    }
    private void Execute()
    {
        Debug.Log($"[{name}] OKボタンが押された！！");
        // Ok ボタンの押下で暫定値を決定値に反映
        if (_isSliderChanged)
        {
            _isSliderChanged = false;
            _nouchi.TaNoKazu = _newTaNoKazu;
            _nouchi.HatakeNoKazu = _newHatakeNoKazu;
            _nouchi.SetUpdateFlag();
        }
    }
}
