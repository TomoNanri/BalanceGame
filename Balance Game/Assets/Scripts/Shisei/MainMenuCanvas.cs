using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MainMenuCanvas : MonoBehaviour
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
    public void OnNenguButton()
    {
        if(OnButton!= null)
        {
            Debug.Log($"[{name}] Nengu Button On!");
            OnButton(this, new ButtonEventArgs("Nengu"));
        }
        else
        {
            Debug.LogError($"[{name}] OnButton is null!");
        }
    }
    public void OnHiritsuButton()
    {
        if (OnButton != null)
        {
            Debug.Log($"[{name}] Hiritsu Button On!");
            OnButton(this, new ButtonEventArgs("TahataHiritsu"));
        }
        else
        {
            Debug.LogError($"[{name}] OnButton is null!");
        }
    }
    public void OnSuidenGijutsuButtom()
    {
        if (OnButton != null)
        {
            OnButton(this, new ButtonEventArgs("SuidenGijutsu"));
        }
        else
        {
            Debug.LogError($"[{name}] OnButton is null!");
        }
    }
    public void OnHatakeGijutsuButton()
    {
        if (OnButton != null)
        {
            OnButton(this, new ButtonEventArgs("HatasakuGijutsu"));
        }
        else
        {
            Debug.LogError($"[{name}] OnButton is null!");
        }
    }
    public void OnNouguButtom()
    {
        if (OnButton != null)
        {
            OnButton(this, new ButtonEventArgs("NouguKounyuu"));
        }
        else
        {
            Debug.LogError($"[{name}] OnButton is null!");
        }
    }
    public void OnShisatsuButton()
    {
        if (OnButton != null)
        {
            OnButton(this, new ButtonEventArgs("Shisatsu"));
        }
        else
        {
            Debug.LogError($"[{name}] OnButton is null!");
        }
    }
    public void OnMatsuriButton()
    {
        if (OnButton != null)
        {
            OnButton(this, new ButtonEventArgs("Matsuri"));
        }
        else
        {
            Debug.LogError($"[{name}] OnButton is null!");
        }
    }
    public void OnKyusaiButton()
    {
        if (OnButton != null)
        {
            OnButton(this, new ButtonEventArgs("Kyusai"));
        }
        else
        {
            Debug.LogError($"[{name}] OnButton is null!");
        }
    }
    public void OnPassButton()
    {
        if (OnButton != null)
        {
            OnButton(this, new ButtonEventArgs("Pass"));
        }
        else
        {
            Debug.LogError($"[{name}] OnButton is null!");
        }
    }
}
