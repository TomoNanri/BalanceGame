using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodOfLuck : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public bool DiceCheck(int Limit, int count)
    {
        for(int i = 0; i < count; i++)
        {
            int ret = Random.Range((int)1, (int)101);
            if(ret <= Limit)
            {
                Debug.Log($"[{name}] Dice{i + 1} Limit = {Limit}, ret = {ret} : Success!");
                return true;
            }
            else
            {
                Debug.Log($"[{name}] Dice{i + 1} Limit = {Limit}, ret = {ret} : Fail!");
            }
        }
        return false;
    }
}
