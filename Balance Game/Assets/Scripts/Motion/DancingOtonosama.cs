using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DancingOtonosama : MonoBehaviour
{
    [SerializeField]
    private float _interval = 0.3f;
    private float _timer;

    // Start is called before the first frame update
    void Start()
    {
        _timer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;
        if(_timer > _interval)
        {
            _timer = 0f;
            var scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }
}
