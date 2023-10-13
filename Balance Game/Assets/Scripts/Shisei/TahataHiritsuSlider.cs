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
    private Nouchi _nouchi;
    private Slider _slider;
    private TextMeshProUGUI _taText;
    private TextMeshProUGUI _hatakeText;

    // Start is called before the first frame update
    void Start()
    {
        _muramei = this.name;
        _nouchi = GameObject.Find($"{_muramei}/Nouchi").GetComponent<Nouchi>();
        _slider = transform.Find("Hiritsu_Slider").gameObject.GetComponent<Slider>();

        // Slider �����l�ǂݎ��
        _newHatakeNoKazu = (int)(_slider.value * (float)_nouchi.NouchiMaxCount);
        _newTaNoKazu = _nouchi.NouchiMaxCount - _newHatakeNoKazu;

        _taText = GameObject.Find($"{_muramei}/TanokazuText").GetComponent<TextMeshProUGUI>();
        _hatakeText = GameObject.Find($"{_muramei}/HatakenokazuText").GetComponent<TextMeshProUGUI>();

        // Slider �����l���C���W�P�[�^�ɐݒ�
        _taText.SetText($"�c{_newTaNoKazu}");
        _hatakeText.SetText($"��{_newHatakeNoKazu}");

        // �c���䗦�ύX�C�x���g�̃n���h���o�^
        GameObject.Find("HiritsuCanvas").GetComponent<TahataHiritsuCanvas>().OkEvent += Execute;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnChangeValue()
    {
        _newHatakeNoKazu = (int)(_slider.value * 32f);
        _newTaNoKazu = _nouchi.NouchiMaxCount - _newHatakeNoKazu;
        _taText.SetText($"�c{_newTaNoKazu}");
        _hatakeText.SetText($"��{_nouchi.HatakeNoKazu}");
    }
    private void Execute()
    {
        _nouchi.TaNoKazu = _newTaNoKazu;
        _nouchi.HatakeNoKazu = _newHatakeNoKazu;
        _nouchi.SetUpdate();
    }
}
