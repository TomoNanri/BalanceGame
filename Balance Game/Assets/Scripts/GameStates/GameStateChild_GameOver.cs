using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateChild_GameOver : AbstractStateChild
{
    private GameManager _gm;
    private Oshiro _oshiro;
    private GameObject _gameOverCanvas;
    private GameOver _gameOver;

    private AudioChanger _audioChanger;

    private bool _isButtonOn = false;
    private string _buttonName;
    public override void Initialize(int stateType)
    {
        _gm = GetComponent<GameManager>();
        _oshiro = GameObject.Find("Oshiro").GetComponent<Oshiro>();
        _gameOverCanvas = GameObject.Find("GameOverCanvas");
        _gameOver = _gameOverCanvas.GetComponent<GameOver>();
        _gameOver.OnButton += OnButtonEventHandler;

        _audioChanger = FindAnyObjectByType<AudioChanger>();

        _gameOverCanvas.SetActive(false);

        base.Initialize(stateType);
    }
    public override void OnEnter()
    {
        // �{�^���C�x���g��M�ϐ���������
        _isButtonOn = false;
        _buttonName = default;

        // Game Over �L�����o�X��\������
        Debug.Log($"[{name}] Enter GameOver State!");
        _oshiro.IsSelectable = false;
        _gameOverCanvas.SetActive(true);

        // Game Over �W���O���𗬂�
        _audioChanger.StopBGM();
        _audioChanger.PlayBGM(AudioChanger.BGMType.GameOver);
    }
    public override void OnExit()
    {
        // Game Over �L�����o�X���\���ɂ���
        _gameOverCanvas.SetActive(false);

        // Game Over �W���O�����~�߂�
        _audioChanger.StopBGM();
        _audioChanger.PlayBGM(AudioChanger.BGMType.Normal);
    }

    public override int StateUpdate()
    {
        if (_isButtonOn)
        {
            return (int)GameManager.StateType.Initialize;
        }
        return (int)StateType;
    }
    private void OnButtonEventHandler(object sender, ButtonEventArgs args)
    {
        _isButtonOn = true;
        _buttonName = args.ButtonName;
    }
}
