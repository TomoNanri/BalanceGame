using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NenguCanvas : MonoBehaviour
{
    public string OnButtonName => _onButtonName;
    private string _onButtonName;
    private TextMeshProUGUI _restrictionMSG;
    private TextMeshProUGUI _costText;
    private GameObject _okButton;

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
        _costText = transform.Find("Panel/CostText").GetComponent<TextMeshProUGUI>();
        var isNenguAvailable = GameObject.Find("Koyomi").GetComponent<Koyomi>().IsNenguAvailable;
        Debug.Log($"[{name}] isNenguAvailable = {isNenguAvailable}");
        _okButton = transform.Find("Panel/OkButton").gameObject;

        if (isNenguAvailable)
        {
            _okButton.SetActive(true);
        }
        else
        {
            _okButton.SetActive(false);
        }
        _onButtonName = null;
        _costText.SetText(msg);
    }
    public void OnOkButton()
    {
        _onButtonName = "Ok";
    }
    public void OnCancelButton()
    {
        _onButtonName = "Cancel";
    }
}
