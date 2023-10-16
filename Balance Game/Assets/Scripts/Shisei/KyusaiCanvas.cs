using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class KyusaiCanvas : MonoBehaviour
{
    // メニューボタンのクリックイベントをステートマシンに通知する（Args は次のステート）
    public delegate void OnButtonEventHandler(object sender, ButtonEventArgs args);
    public event OnButtonEventHandler OnButton;

    private GameObject _OkButtonUshitora;
    private GameObject _OkButtonInui;
    private GameObject _OkButtonHitsujisaru;
    private GameObject _OkButtonTatsumi;
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
        _OkButtonUshitora = transform.Find("Panel/OkButtonUshitora").gameObject;
        _OkButtonInui = transform.Find("Panel/OkButtonInui").gameObject;
        _OkButtonHitsujisaru = transform.Find("Panel/OkButtonHitsujisaru").gameObject;
        _OkButtonTatsumi = transform.Find("Panel/OkButtonTatsumi").gameObject;

        Debug.Log($"[{name}] _OkButtonUshitora = {_OkButtonUshitora}");
        Debug.Log($"[{name}] _OkButtonInui = {_OkButtonInui}");

        _OkButtonUshitora.SetActive(isShowOkButton);
        _OkButtonInui.SetActive(isShowOkButton);
        _OkButtonHitsujisaru.SetActive(isShowOkButton);
        _OkButtonTatsumi.SetActive(isShowOkButton);
        _costText.SetText(msg);
    }
    public void OnOkUshitora()
    {
        if (OnButton != null)
        {
            OnButton(this, new ButtonEventArgs("OkUshitora"));
        }
        else
        {
            Debug.LogError($"[{name}] OnButton is null!");
        }
    }
    public void OnOkInui()
    {
        if (OnButton != null)
        {
            OnButton(this, new ButtonEventArgs("OkInui"));
        }
        else
        {
            Debug.LogError($"[{name}] OnButton is null!");
        }
    }
    public void OnOkHitsujisaru()
    {
        if (OnButton != null)
        {
            OnButton(this, new ButtonEventArgs("OkHitsujisaru"));
        }
        else
        {
            Debug.LogError($"[{name}] OnButton is null!");
        }
    }
    public void OnOkTatsumi()
    {
        if (OnButton != null)
        {
            OnButton(this, new ButtonEventArgs("OkTatsumi"));
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
