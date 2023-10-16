using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MatsuriCanvas : MonoBehaviour
{
    // ���j���[�{�^���̃N���b�N�C�x���g���X�e�[�g�}�V���ɒʒm����iArgs �͎��̃X�e�[�g�j
    public delegate void OnButtonEventHandler(object sender, ButtonEventArgs args);
    public event OnButtonEventHandler OnButton;

    private GameObject _okButton;
    private TextMeshProUGUI _restrictionMSG;
    private TextMeshProUGUI _costText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Setup(string msg, bool isShowOkButton)
    {
        _restrictionMSG = transform.Find("Panel/RestrictionText").GetComponent<TextMeshProUGUI>();
        _costText = transform.Find("Panel/CostText").GetComponent<TextMeshProUGUI>();
        _okButton = transform.Find("Panel/OkButton").gameObject;

        _okButton.SetActive(isShowOkButton);
        _costText.SetText(msg);
    }
    public void OnOkButton()
    {
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
