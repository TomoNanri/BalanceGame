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
    private string[] _tsukimei = {"����", "��", "�O��", "�l��", "�܌�", "�Z��", "����", "����", "�㌎", "�\��", "�\�ꌎ", "�\��" };

    // Start is called before the first frame update
    void Start()
    {
        _tsukisuu = 0;
        IsNewMonth = false;
        _gm = FindAnyObjectByType<GameManager>();
        _gm.LoadGameProc += LoadGame;

        // ��L�����o�X�̏�����
        _nengouText = transform.Find("Canvas/Panel/NengouText").gameObject.GetComponent<TextMeshProUGUI>();
        _tsukiText = transform.Find("Canvas/Panel/TsukiText").gameObject.GetComponent<TextMeshProUGUI>();

        // state controller �X�^�[�g
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
            _nengouText.SetText($"�߁Z ���N");
        }
        else
        {
            _nengouText.SetText($"�߁Z {nen}�N");
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
