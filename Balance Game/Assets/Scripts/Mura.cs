using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mura : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _prefab;
    public GameObject Noumin;

    // Start is called before the first frame update
    void Start()
    {
        Noumin = Instantiate(_prefab[0], this.transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
