using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PassCanvas : MonoBehaviour
{
    public string OnButtonName => _onButtonName;
    private string _onButtonName;

    private GameManager _gm;
    private TextMeshProUGUI _restrictionMSG;

    // Start is called before the first frame update
    void Start()
    {
        _gm = FindAnyObjectByType<GameManager>();
        _restrictionMSG = transform.Find("Panel/RestrictionText").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Setup(string msg)
    {
        _onButtonName = null;
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
