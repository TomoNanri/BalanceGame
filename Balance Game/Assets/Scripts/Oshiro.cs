using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Oshiro : MonoBehaviour
{
    public bool IsSelectable { get; set; }

    private RaycastHit _hitObj;

    // Start is called before the first frame update
    void Start()
    {
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
                if(_hitObj.transform.tag == "Player")
                {

                }
            }
        }
    }
}
