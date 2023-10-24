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

        //Debug.Log($"[{name}] _nouchi�̓c�̐�={_nouchi.TaNoKazu}�@���̐�={_nouchi.HatakeNoKazu}");


        //// �c���䗦�ύX�C�x���g�̃n���h���o�^
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

        Debug.Log($"[{name}] _nouchi�̓c�̐�={_nouchi.TaNoKazu}�@���̐�={_nouchi.HatakeNoKazu}");

    }
    public void SetSliderValue()
    {
        Debug.Log($"[{_muramei}] �X���C�_�[�ݒ肪�Ă΂ꂽ�I{_nouchi.TaNoKazu}�F{_slider.value}�F{_nouchi.HatakeNoKazu}");

        // Slider �����l�ݒ�
        _slider.value = (float)_nouchi.HatakeNoKazu / (float)_nouchi.NouchiMaxCount;

        Debug.Log($"[{_muramei}] �X���C�_�[�l���Z�b�g���ꂽ�I{_nouchi.TaNoKazu}�F{_slider.value}�F{_nouchi.HatakeNoKazu}");

        // Slider �����l���C���W�P�[�^�ɐݒ�
        _taText.SetText($"�c{_nouchi.TaNoKazu}");
        _hatakeText.SetText($"��{_nouchi.HatakeNoKazu}");
        // �c���䗦�ύX�C�x���g�̃n���h���o�^
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
        Debug.Log($"[{_muramei}] Slider �̒l���ς�����I{_slider.value}");

        // Slider �̒l�ɍ��킹�ăC���W�P�[�^���b��l�ɕύX
        _newHatakeNoKazu = (int)(_slider.value * _nouchi.NouchiMaxCount);
        _newTaNoKazu = _nouchi.NouchiMaxCount - _newHatakeNoKazu;
        _taText.SetText($"�c{_newTaNoKazu}");
        _hatakeText.SetText($"��{_newHatakeNoKazu}");

        _isSliderChanged = true;
    }
    private void Execute()
    {
        Debug.Log($"[{name}] OK�{�^���������ꂽ�I�I");
        // Ok �{�^���̉����Ŏb��l������l�ɔ��f
        if (_isSliderChanged)
        {
            _isSliderChanged = false;
            _nouchi.TaNoKazu = _newTaNoKazu;
            _nouchi.HatakeNoKazu = _newHatakeNoKazu;
            _nouchi.SetUpdateFlag();
        }
    }
}
