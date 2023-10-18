using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    // ���j���[�{�^���̃N���b�N�C�x���g���X�e�[�g�}�V���ɒʒm����iArgs �͎��̃X�e�[�g�j
    public delegate void OnButtonEventHandler(object sender, ButtonEventArgs args);
    public event OnButtonEventHandler OnButton;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnReturnButton()
    {
        Debug.Log($"[{name}] �X�^�[�g�ɂ��ǂ�{�^���������ꂽ�I");
        if (OnButton != null)
        {
            OnButton(this, new ButtonEventArgs("Return"));
        }
        else
        {
            Debug.LogError($"[{name}] OnButton is null!");
        }
    }
}
