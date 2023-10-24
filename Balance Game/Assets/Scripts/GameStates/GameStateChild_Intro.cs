using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameStateChild_Intro : AbstractStateChild
{
    private const float _eventShowTime = 5.0f;

    private GameManager _gm;
    private GameObject _introCanvas;
    private Tokuten _tokuten;
    private Oshiro _oshiro;
    private GodOfLuck _kami;
    private bool _needOpeningEvent;

    public override void Initialize(int stateType)
    {
        _gm = GetComponent<GameManager>();
        _introCanvas = GameObject.Find("IntroCanvas");
        _tokuten = GameObject.FindAnyObjectByType<Tokuten>();
        _oshiro = GameObject.FindAnyObjectByType<Oshiro>();
        _kami = GameObject.FindAnyObjectByType<GodOfLuck>();

        base.Initialize(stateType);
    }
    public override void OnEnter()
    {
        Debug.Log($"[{this.name}] Enter Intro State!");

        // �C���g���L�����o�X�̕\��
        _introCanvas.SetActive(true);
        _needOpeningEvent = false;
    }
    public override void OnExit()
    {
        if (_needOpeningEvent)
        {
            // �V�K�Q�[���|�b�v�A�b�v�\��

            _needOpeningEvent = false;
            StartCoroutine(OpeningEvent(_eventShowTime));

            // �K�^�l����

            _oshiro.Luck = _kami.GetDiceD6(4) * 4;
            if (_introCanvas.transform.Find("Panel/LevelSelect/ToggleEasy").GetComponent<Toggle>().isOn)
            {
                _oshiro.Luck = Math.Min(96, _oshiro.Luck + 30);
            }
            if (_introCanvas.transform.Find("Panel/LevelSelect/ToggleHard").GetComponent<Toggle>().isOn)
            {
                _oshiro.Luck = Math.Max(16, _oshiro.Luck - 40);
            }
            Debug.Log($"*** Luck = {_oshiro.Luck}");
        }
        // �C���g���L�����o�X�̏���
        _introCanvas.SetActive(false);
    }

    public override int StateUpdate()
    {
        if (_gm.OnNewGame)
        {
            _gm.OnNewGame = false;
            _gm.InitializeHandler.Invoke();
            _needOpeningEvent = true;
            return (int)GameManager.StateType.WaitInput;
        }
        if (_gm.OnLoadGame)
        {
            _gm.OnLoadGame = false;
            return (int)GameManager.StateType.Loading;
        }
        return (int)StateType;
    }
    IEnumerator OpeningEvent(float sec)
    {
        yield return new WaitForEndOfFrame();
        var _houbiPanel = Instantiate(_gm.HoubiPrefab, Vector3.zero, Quaternion.identity);
        var _text = _houbiPanel.transform.Find("Canvas/Panel/Text").gameObject.GetComponent<TextMeshProUGUI>();
        _text.SetText($"�������炻�����喼����\n\n�j�V�̏�����^����\n�S���Α喼��ڎw���̂���");


        yield return new WaitForSeconds(sec);
        _tokuten.UpdateKokudaka(10000);
        _tokuten.AddKoban(10000);
        Destroy(_houbiPanel);
    }
}
