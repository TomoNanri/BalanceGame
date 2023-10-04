using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kukaku : MonoBehaviour
{
    public GameObject tanbo;
    public GameObject hatake;
    private int count = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            count++;
            if(transform.childCount != 0)
            {
                var child = transform.GetChild(0);
                Destroy(child.gameObject);
            }
            if(count%2 == 1)
            {
                var child = Instantiate(tanbo, this.transform, false);
                child.transform.position = transform.position;
            }
            else
            {
                var child = Instantiate(hatake, this.transform, false);
                child.transform.position = transform.position;
            }
        }
    }
}
