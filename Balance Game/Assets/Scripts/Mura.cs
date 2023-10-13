using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mura : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _prefab;

    private GameObject Noumin;

    private Oshiro _oshiro;
    private Koyomi _koyomi;
    private Nouchi _nouchi;

    // Start is called before the first frame update
    void Start()
    {
        _oshiro = GameObject.FindAnyObjectByType<Oshiro>();
        _koyomi = GameObject.FindAnyObjectByType<Koyomi>();
        _nouchi = transform.Find("Nouchi").GetComponent<Nouchi>();

        // 農民（普通）活動開始
        Noumin = Instantiate(_prefab[0], this.transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public int PayNengu()
    {
        if(_koyomi.Tsuki != 9)
        {
            Debug.LogError($"[{name}] １０月以外にボタンが押された！");
            //return 0;
        }
        return _nouchi.TaNoKazu * _oshiro.LevelList[_oshiro.TaLevel];
    }
}
