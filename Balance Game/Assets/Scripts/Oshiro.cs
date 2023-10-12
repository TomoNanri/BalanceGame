using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Oshiro : MonoBehaviour
{
    public bool IsSelectable { get; set; }

    private GameManager _gm;
    private GameObject _mainMenuCanvas;
    private RaycastHit _hitObj;

    // Start is called before the first frame update
    void Start()
    {
        _gm = GameObject.FindAnyObjectByType<GameManager>();
        IsSelectable = false;
        _mainMenuCanvas = transform.Find("MainMenuCanvas").gameObject;
        _mainMenuCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsSelectable)
        {
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {
            Ray rey = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(rey, out _hitObj))
            {
                Debug.Log($"[{this.name}] HitObject Tag = {_hitObj.transform.tag}");

                if (_hitObj.transform.tag == "Oshiro")
                {
                    Debug.Log($"[{this.name}] Oshiro is selected!");
                    _gm.StateByButton = GameManager.StateType.InMainMenu;
                }
            }
        }
    }
    public void ShowMainMenu(bool isActivate)
    {
        _mainMenuCanvas.SetActive(isActivate);
    }
    public void OnNenguButton()
    {
        _gm.StateByButton = GameManager.StateType.InNengu;
        _mainMenuCanvas.SetActive(false);
    }
    public void OnHiritsuButton()
    {
        _gm.StateByButton = GameManager.StateType.InTahataHiritsu;
        _mainMenuCanvas.SetActive(false);
    }
    public void OnSuidenGijutsuButtom()
    {
        _gm.StateByButton = GameManager.StateType.InSuidenGijutsu;
        _mainMenuCanvas.SetActive(false);
    }
    public void OnHatakeGijutsuButton()
    {
        _gm.StateByButton = GameManager.StateType.InHatasakuGijutsu;
        _mainMenuCanvas.SetActive(false);
    }
    public void OnNouguButtom()
    {
        _gm.StateByButton = GameManager.StateType.InNouguKounyuu;
        _mainMenuCanvas.SetActive(false);
    }
    public void OnShisatsuButton()
    {
        _gm.StateByButton = GameManager.StateType.InShisatsu;
        _mainMenuCanvas.SetActive(false);
    }
    public void OnMatsuriButton()
    {
        _gm.StateByButton = GameManager.StateType.InMatsuri;
        _mainMenuCanvas.SetActive(false);
    }
    public void OnKyusaiButton()
    {
        _gm.StateByButton = GameManager.StateType.InKyusai;
        _mainMenuCanvas.SetActive(false);
    }
    public void OnPassButton()
    {
        _gm.StateByButton = GameManager.StateType.InPass;
        _mainMenuCanvas.SetActive(false);
    }
}
