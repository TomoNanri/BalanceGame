using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShisatsuCanvas : MonoBehaviour
{
    // メニューボタンのクリックイベントをステートマシンに通知する（Args は次のステート）
    public delegate void OnButtonEventHandler(object sender, ButtonEventArgs args);
    public event OnButtonEventHandler OnButton;

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

        transform.Find("Panel/GoNorthButton").gameObject.SetActive(isShowOkButton);
        transform.Find("Panel/GoEastButton").gameObject.SetActive(isShowOkButton);
        transform.Find("Panel/GoSouthButton").gameObject.SetActive(isShowOkButton);
        transform.Find("Panel/GoWestButton").gameObject.SetActive(isShowOkButton);

        _costText.SetText(msg);
    }
    public void OnGoNorthButton()
    {
        if (OnButton != null)
        {
            OnButton(this, new ButtonEventArgs("GoNorth"));
        }
        else
        {
            Debug.LogError($"[{name}] OnButton is null!");
        }
    }
    public void OnGoEastButton()
    {
        if (OnButton != null)
        {
            OnButton(this, new ButtonEventArgs("GoEast"));
        }
        else
        {
            Debug.LogError($"[{name}] OnButton is null!");
        }
    }
    public void OnGoSouthButton()
    {
        if (OnButton != null)
        {
            OnButton(this, new ButtonEventArgs("GoSouth"));
        }
        else
        {
            Debug.LogError($"[{name}] OnButton is null!");
        }
    }
    public void OnGoWestButton()
    {
        if (OnButton != null)
        {
            OnButton(this, new ButtonEventArgs("GoWest"));
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
