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

    public List<Mura> MuraList = new List<Mura>();

    [SerializeField]
    private GameObject _guideOtonosamaPrefab;
    [SerializeField]
    private GameObject _seppukuOtonosamaPrefab;

    private GameObject _otonosama;

    // Start is called before the first frame update
    void Start()
    {
        MuraList.Add(GameObject.Find("UshitoraMura").GetComponent<Mura>());
        MuraList.Add(GameObject.Find("InuiMura").GetComponent<Mura>());
        MuraList.Add(GameObject.Find("HitsujisaruMura").GetComponent<Mura>());
        MuraList.Add(GameObject.Find("TatsumiMura").GetComponent<Mura>());
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
    public void ShowOtonosama(bool isActive)
    {
        if (_otonosama == null)
        {
            _otonosama = Instantiate(_guideOtonosamaPrefab, this.transform);
            Vector2 pos = _otonosama.transform.position;
            pos.x += 0.5f;
            pos.y += 0.5f;
            _otonosama.transform.position = pos;
        }
        _otonosama.SetActive(isActive);
    }
}
