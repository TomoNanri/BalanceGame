using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class TahataHiritsuCanvas : MonoBehaviour
{
    // 比率変更決定のイベントを各村のスライダーに通知する
    public Action OkEvent;

    // メニューボタンのクリックイベントをステートマシンに通知する（Args は次のステート）
    public delegate void OnButtonEventHandler(object sender, ButtonEventArgs args);
    public event OnButtonEventHandler OnButton;

    private TextMeshProUGUI _restrictionMSG;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Setup(string msg)
    {
        _restrictionMSG = transform.Find("Panel/RestrictionText").GetComponent<TextMeshProUGUI>();
        _restrictionMSG.SetText(msg);
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
