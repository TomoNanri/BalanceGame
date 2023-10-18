using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    // メニューボタンのクリックイベントをステートマシンに通知する（Args は次のステート）
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
        Debug.Log($"[{name}] スタートにもどるボタンが押された！");
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
