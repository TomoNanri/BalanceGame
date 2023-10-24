using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Intro : MonoBehaviour
{
    private GameManager _gm;
    private Toggle _BGMToggle;
    private Toggle _SEToggle;
    private Toggle _VoiceToggle;
    private Slider _soundValume;
    private AudioChanger _audioChanger;

    // Start is called before the first frame update
    void Start()
    {
        _gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        _soundValume = transform.Find("Panel/Volume/Slider").GetComponent<Slider>();
        _BGMToggle = transform.Find("Panel/BGMControl/ToggleOn").GetComponent<Toggle>();
        _SEToggle = transform.Find("Panel/SEControl/ToggleOn").GetComponent<Toggle>();
        _VoiceToggle = transform.Find("Panel/VoiceControl/ToggleOn").GetComponent<Toggle>();
        _audioChanger = FindAnyObjectByType<AudioChanger>();

        _gm.IsOnBGM = _BGMToggle.isOn;
        _gm.IsOnSE = _SEToggle.isOn;
        _gm.IsOnVoice = _VoiceToggle.isOn;

        // âπó ïœçXÇÕÉnÉìÉhÉâìoò^å„
        _soundValume.value = _gm.SoundLevel;
        if (_gm.IsOnBGM)
        {
            _audioChanger.PlayBGM(AudioChanger.BGMType.Normal);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnChangeBGMToggle()
    {
        if (_BGMToggle.isOn)
        {
            _gm.IsOnBGM = true;
            _audioChanger.PlayBGM(AudioChanger.BGMType.Normal);
        }
        else
        {
            _gm.IsOnBGM = false;
            _audioChanger.StopBGM();
        }
    }
    public void OnchangeSEToggle()
    {
        if (_SEToggle.isOn)
        {
            _gm.IsOnSE = true;
        }
        else
        {
            _gm.IsOnSE = false;
        }
    }
    public void OnchangeVoiceToggle()
    {
        if (_VoiceToggle.isOn)
        {
            _gm.IsOnVoice = true;
        }
        else
        {
            _gm.IsOnVoice = false;
        }
    }
    public void OnChangeVolume()
    {
        _gm.SoundLevel = _soundValume.value;
        Debug.Log($"[{this.name}] SoundLevel={_gm.SoundLevel}");
    }
}
