using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Oshiro : MonoBehaviour
{
    public bool IsSelectable { get; set; }

    private GameManager _gm;
    private GameObject _menuCanvas;
    private RaycastHit _hitObj;

    // Start is called before the first frame update
    void Start()
    {
        _gm = GameObject.FindAnyObjectByType<GameManager>();
        IsSelectable = false;
        _menuCanvas = transform.Find("MenuCanvas").gameObject;
        _menuCanvas.SetActive(false);
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
                    _menuCanvas.SetActive(true);
                }
            }
        }
    }
    public void OnNenguButton()
    {
        _gm.StateByButton = GameManager.StateType.InNengu;
        _menuCanvas.SetActive(false);
    }
    public void OnHiritsuButton()
    {
        _gm.StateByButton = GameManager.StateType.InTahataHiritsu;
        _menuCanvas.SetActive(false);
    }
    public void OnSuidenGijutsuButtom()
    {
        _menuCanvas.SetActive(false);

    }
    public void OnHatakeGijutsuButton()
    {
        _menuCanvas.SetActive(false);

    }
    public void OnNouguButtom()
    {
        _menuCanvas.SetActive(false);

    }
    public void OnShisatsuButton()
    {
        _menuCanvas.SetActive(false);

    }
    public void OnMatsuriButton()
    {
        _menuCanvas.SetActive(false);

    }
    public void OnKyusaiButton()
    {
        _menuCanvas.SetActive(false);

    }
    public void OnPassButton()
    {
        _menuCanvas.SetActive(false);

    }
}
