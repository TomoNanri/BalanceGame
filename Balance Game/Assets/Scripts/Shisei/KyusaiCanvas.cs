using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class KyusaiCanvas : MonoBehaviour
{
    private GameManager _gm;
    private TextMeshProUGUI _restrictionMSG;
    private TextMeshProUGUI _costText;
    
    // Start is called before the first frame update
    void Start()
    {
        _gm = FindAnyObjectByType<GameManager>();
        _restrictionMSG = transform.Find("Panel/RestrictionText").GetComponent<TextMeshProUGUI>();
        _costText = transform.Find("Panel/CostText").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Activate(string msg)
    {
        _costText.SetText(msg);
    }
    public void OnOkButton()
    {
        _gm.StateByButton = GameManager.StateType.Progress;
    }
    public void OnCancelButton()
    {
        _gm.StateByButton = GameManager.StateType.InMainMenu;
    }
}
