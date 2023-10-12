using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PassCanvas : MonoBehaviour
{
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
    public void Activate(string msg)
    {
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
