using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Nouchi : MonoBehaviour
{
    public GameObject Tanbo;
    public GameObject Hatake;
    public int NouchiMaxCount = 32;

    public int TaNoKazu;
    public int HatakeNoKazu;

    [SerializeField]
    private string _muramei;
    [SerializeField]
    private Vector2[] _kukaku;

    private Oshiro _oshiro;

    private bool _isUpdate = false;

    // Start is called before the first frame update
    void Start()
    {
        _oshiro = GameObject.FindAnyObjectByType<Oshiro>();

        // 村名取得
        _muramei = transform.parent.name;
        Debug.Log($"[{this.name}]Muramei = {_muramei}");

        // 田畑の比率初期設定
        TaNoKazu = NouchiMaxCount / 2;
        HatakeNoKazu = TaNoKazu;
        _isUpdate = true;

        // 田畑の配置場所決定（農地に対する相対座標）
        _kukaku = new Vector2[NouchiMaxCount];
        for(int i=0; i < NouchiMaxCount; i++)
        {
            _kukaku[i] = new Vector2(transform.position.x+(float)(i%8)*0.95f-3.3f, transform.position.y+(float)(i/8)*0.9f-1.4f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_isUpdate)
        {
            _isUpdate = false;

            if (transform.childCount != 0)
            {
                for(int c = 0; c < NouchiMaxCount; c++)
                {
                    var child = transform.GetChild(c);
                    Destroy(child.gameObject);
                }
            }
            for (int c = 0; c < TaNoKazu; c++)
            {
                var child = Instantiate(Tanbo, this.transform, false);
                child.transform.position = _kukaku[c];
                TextMeshProUGUI levelText =child.transform.Find("Canvas/LevelText").GetComponent<TextMeshProUGUI>();
                levelText.SetText($"Lv{_oshiro.TaLevel}");
            }
            for (int c = TaNoKazu; c < NouchiMaxCount; c++)
            {
                var child = Instantiate(Hatake, this.transform, false);
                child.transform.position = _kukaku[c];
                TextMeshProUGUI levelText = child.transform.Find("Canvas/LevelText").GetComponent<TextMeshProUGUI>();
                levelText.SetText($"Lv{_oshiro.HatakeLevel}");
            }
        }
    }
    public void SetUpdate()
    {
        _isUpdate = true;
    }
}
