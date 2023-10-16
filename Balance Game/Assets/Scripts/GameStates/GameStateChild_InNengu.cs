using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateChild_InNengu : AbstractStateChild
{
    private GameManager _gm;
    private Oshiro _oshiro;
    private GameObject _commandCanvas;
    private NenguCanvas _nenguCanvas;
    private Tokuten _tokutenPanel;

    private int _nengu;

    public override void Initialize(int stateType)
    {
        _gm = GetComponent<GameManager>();
        _oshiro = GameObject.Find("Oshiro").GetComponent<Oshiro>();
        _commandCanvas = _oshiro.transform.Find("NenguCanvas").gameObject;
        _nenguCanvas = _commandCanvas.GetComponent<NenguCanvas>();
        _tokutenPanel = GameObject.FindAnyObjectByType<Tokuten>();

        // �N�v�L�����o�X��s���ɂ���
        _commandCanvas.SetActive(false);
        base.Initialize(stateType);
    }
    public override void OnEnter()
    {
        Debug.Log($"[{this.name}] Enter InNengu_State!");

        // �R�}���h�L�����o�X��\������
        _commandCanvas.SetActive(true);
        _nenguCanvas.Setup("");
    }
    public override void OnExit()
    {
        Debug.Log($"[{name}] Exit InNengu!");

        // �R�}���h�L�����o�X����������
        _commandCanvas.SetActive(false);
    }

    public override int StateUpdate()
    {
        if (_nenguCanvas.OnButtonName != null)
        {
            switch (_nenguCanvas.OnButtonName)
            {
                case "Ok":
                    // ���P�ʂɔN�v�𒥎��i���������ƂɃC�x���g���N�����\��j
                    _nengu = GameObject.Find("UshitoraMura").GetComponent<Mura>().PayNengu();
                    _nengu += GameObject.Find("InuiMura").GetComponent<Mura>().PayNengu();
                    _nengu += GameObject.Find("HitsujisaruMura").GetComponent<Mura>().PayNengu();
                    _nengu += GameObject.Find("TatsumiMura").GetComponent<Mura>().PayNengu();

                    // ���_�p�l���̍X�V
                    _tokutenPanel.UpdateKokudaka(_nengu);
                    _tokutenPanel.AddKoban(_nengu);

                    return (int)GameManager.StateType.Progress;

                case "Cancel":
                    return (int)GameManager.StateType.InMainMenu;

                default:
                    Debug.LogError($"[{name}] Undefind button found.");
                    return (int)StateType;
            }
        }
        return (int)StateType;
    }
}
