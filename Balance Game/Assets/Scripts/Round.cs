using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Round : MonoBehaviour
{
    public int hatake = 0;
    public int manzoku = 50;
    public float alpha = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            float delta = (float)Math.Tanh((double)(alpha * (float)(hatake - 16) / 2.0f)) * 6.0f;
            manzoku +=  Mathf.RoundToInt(delta);
            Debug.Log($"hatake = {hatake}, delta = {delta}, manzoku = {manzoku}");
        }
    }
}
