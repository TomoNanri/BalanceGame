using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Oshiro : MonoBehaviour
{
    // お城がクリックされた Event を GameState ステートマシン へ通知する
    public delegate void OshiroSelectedEventHandler(object sender, EventArgs args);
    public event OshiroSelectedEventHandler OshiroSelected;

    public readonly int[] LevelList = { 200, 400, 600, 1000, 1600, 2600, 4200, 6800, 11000, 17800 };
    public int LevelMax = 10;
    public bool IsSelectable { get; set; }
    public int TaLevel { get; set; } = 1;
    public int HatakeLevel { get; set; } = 1;
    public int Luck { get; set; } = 50;

    // Satisfaction に与える施策の効果値
    public int ShisatsuKouka = 1;
    public int MatsuriKouka = 9;
    public int KyusaiKouka = 3;

    private GameManager _gm;
    private GameObject _mainMenuCanvas;
    private RaycastHit _hitObj;
    [SerializeField]
    private GameObject _guidePrefab;
    private GameObject _guideObject;

    // Start is called before the first frame update
    void Start()
    {
        _gm = GameObject.FindAnyObjectByType<GameManager>();
        IsSelectable = false;
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
                    if (OshiroSelected != null)
                    {
                        OshiroSelected(this, EventArgs.Empty);
                    }
                }
            }
        }
    }
    public void ShowGuide(bool isActive)
    {
        if (_guideObject == null)
        {
            _guideObject = Instantiate(_guidePrefab, this.transform);
            Vector2 pos = _guideObject.transform.position;
            pos.x += 0.5f;
            pos.y += 0.5f;
            _guideObject.transform.position = pos;
        }
        _guideObject.SetActive(isActive);
    }
}
