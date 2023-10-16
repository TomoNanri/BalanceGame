using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kyusai : MonoBehaviour
{
    private GameObject _canvas2;
    private float _timer;

    // Start is called before the first frame update
    void Start()
    {
        _canvas2 = transform.Find("Canvas2").gameObject;
        _canvas2.SetActive(false);
        _timer = 2.0f;
    }

    // Update is called once per frame
    void Update()
    {
        _timer -= Time.deltaTime;
        if(_timer < 0)
        {
            _canvas2.SetActive(true);
        }
    }
}
