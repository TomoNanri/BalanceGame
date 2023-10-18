using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Intro : MonoBehaviour
{
    private GameManager _gm;

    // Start is called before the first frame update
    void Start()
    {
        _gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnChangeVolume()
    {
        _gm.SoundLevel = transform.Find("Panel/Volume/Slider").GetComponent<Slider>().value;
        Debug.Log($"[{this.name}] SoundLevel={_gm.SoundLevel}");
    }
}
