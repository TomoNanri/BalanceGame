using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class TahataHiritsuCanvas : MonoBehaviour
{
    // �䗦�ύX����̃C�x���g���e���̃X���C�_�[�ɒʒm����
    public Action OkEvent;

    // ���j���[�{�^���̃N���b�N�C�x���g���X�e�[�g�}�V���ɒʒm����iArgs �͎��̃X�e�[�g�j
    public delegate void OnButtonEventHandler(object sender, ButtonEventArgs args);
    public event OnButtonEventHandler OnButton;

    private Oshiro _oshiro;

    private TextMeshProUGUI _restrictionMSG;

    // Start is called before the first frame update
    void Start()
    {
        _oshiro = FindAnyObjectByType<Oshiro>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Setup(string msg)
    {
        OkEvent = null;
        _restrictionMSG = transform.Find("Panel/RestrictionText").GetComponent<TextMeshProUGUI>();
        _restrictionMSG.SetText(msg);
        foreach(Mura m in _oshiro.MuraList)
        {
            var slider = transform.Find($"Panel/{m.name}").GetComponent<TahataHiritsuSlider>();
            if (slider != null)
            {
                slider.SetSliderValue();
            }
            else
            {
                Debug.LogError($"[{name}] Slider {m.name} Not found!");
            }
        }
    }
    public void OnOkButton()
    {
        if (OkEvent != null)
        {
            OkEvent.Invoke();
        }

        if (OnButton != null)
        {
            OnButton(this, new ButtonEventArgs("Ok"));
        }
        else
        {
            Debug.LogError($"[{name}] OnButton is null!");
        }
    }
    public void OnCancelButton()
    {
        if (OnButton != null)
        {
            OnButton(this, new ButtonEventArgs("Cancel"));
        }
        else
        {
            Debug.LogError($"[{name}] OnButton is null!");
        }
    }
}
