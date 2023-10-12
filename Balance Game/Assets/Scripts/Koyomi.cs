using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Koyomi : MonoBehaviour
{
    public bool IsNewMonth { get; set; }
    [SerializeField]
    SeasonStateController stateController = default;
    [SerializeField]
    private int _tsukisuu;

    private GameManager _gm;
    private TextMeshProUGUI _nengouText;
    private TextMeshProUGUI _tsukiText;
    private string[] _tsukimei = {"正月", "二月", "三月", "四月", "五月", "六月", "七月", "八月", "九月", "十月", "十一月", "十二月" };

    // Start is called before the first frame update
    void Start()
    {
        _tsukisuu = 0;
        IsNewMonth = false;
        _gm = FindAnyObjectByType<GameManager>();
        _gm.LoadGameProc += LoadGame;

        // 暦キャンバスの初期化
        _nengouText = transform.Find("Canvas/Panel/NengouText").gameObject.GetComponent<TextMeshProUGUI>();
        _tsukiText = transform.Find("Canvas/Panel/TsukiText").gameObject.GetComponent<TextMeshProUGUI>();

        // state controller スタート
        stateController.Initialize((int)SeasonStateController.StateType.January);
    }

    // Update is called once per frame
    void Update()
    {
        stateController.UpdateSequence();
    }
    public void ShowKoyomi()
    {
        IsNewMonth = false;

        var nen = _tsukisuu / 12 + 1;
        var tsuki = _tsukisuu % 12;
        if (nen == 0)
        {
            _nengouText.SetText($"令〇 元年");
        }
        else
        {
            _nengouText.SetText($"令〇 {nen}年");
        }
        _tsukiText.SetText($"{_tsukimei[tsuki]}");
    }
    public void GoNextMonth()
    {
        _tsukisuu++;
        IsNewMonth = true;
    }
    public void LoadGame()
    {

    }
}
