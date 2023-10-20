using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodOfLuck : MonoBehaviour
{
    private GameManager _gm;
    private AudioSource _diceSE;
    private bool _isDoundEffectOn;

    // Start is called before the first frame update
    void Start()
    {
        _diceSE = GetComponent<AudioSource>();
        _gm = FindAnyObjectByType<GameManager>();
        _isDoundEffectOn = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isDoundEffectOn)
        {
            _isDoundEffectOn = false;
            _diceSE.Play();
        }
    }
    public bool DiceCheckD100(int Limit, int count)
    {
        if(_gm.IsOnSE)
            _isDoundEffectOn = true;

        for (int i = 0; i < count; i++)
        {
            int ret = Random.Range(1, 101);
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
    public int GetDiceD6(int count)
    {
        int result = 0;
        if (_gm.IsOnSE)
            _isDoundEffectOn = true;

        for (int i = 0; i < count; i++)
        {
            int ret = Random.Range(1, 7);
            result += ret;
            Debug.Log($"[{name}] Dice{i + 1} ret = {ret} : result = {result}");
        }
        return result;
    }
}
