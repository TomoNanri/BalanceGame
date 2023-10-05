using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nouchi : MonoBehaviour
{
    public GameObject Tanbo;
    public GameObject Hatake;
    public int TanoKazu;
    [SerializeField]
    private int _NouchiMaxCount = 32;
    [SerializeField]
    private Vector2[] _kukaku;

    [SerializeField]
    private int count = 0;

    // Start is called before the first frame update
    void Start()
    {
        _kukaku = new Vector2[_NouchiMaxCount];
        for(int i=0; i < _NouchiMaxCount; i++)
        {
            _kukaku[i] = new Vector2(transform.position.x+(float)(i%8)*0.95f-3.3f, transform.position.y+(float)(i/8)*0.9f-1.4f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            count++;
            if (transform.childCount != 0)
            {
                for(int c = 0; c < _NouchiMaxCount; c++)
                {
                    var child = transform.GetChild(c);
                    Destroy(child.gameObject);
                }
            }
            if (count % 2 == 1)
            {
                for (int c = 0; c < _NouchiMaxCount; c++)
                {
                    var child = Instantiate(Tanbo, this.transform, false);
                    child.transform.position = _kukaku[c];
                }
            }
            else
            {
                for (int c = 0; c < _NouchiMaxCount; c++)
                {
                    var child = Instantiate(Hatake, this.transform, false);
                    child.transform.position = _kukaku[c];
                }
            }
        }
    }
}
