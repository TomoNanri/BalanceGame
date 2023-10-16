using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KanshaNoOdori : MonoBehaviour
{
    [SerializeField]
    private float _interval = 0.3f;
    private float _timer;
    private int _count;

    // Start is called before the first frame update
    void Start()
    {
        _timer = 0f;
        _count = 0;
    }

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;
        if (_timer > _interval)
        {
            var pos = transform.position;

            _timer = 0f;

            if(++_count%2 == 0)
            {
                pos.x += 0.3f;
            }
            else
            {
                pos.x -= 0.3f;
            }
            transform.position = pos;
        }
    }
}
