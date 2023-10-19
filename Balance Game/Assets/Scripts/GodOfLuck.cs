using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodOfLuck : MonoBehaviour
{
    private AudioSource _diceSE;
    private int _soundEffectCount;

    // Start is called before the first frame update
    void Start()
    {
        _diceSE = GetComponent<AudioSource>();
        _soundEffectCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (_soundEffectCount > 0)
        {
            _diceSE.Play();
            _soundEffectCount--;
        }
    }
    //IEnumerator PlaySound()
    //{
    //    _diceSE.Play();
    //    yield return new WaitWhile();
    //}
    public bool DiceCheckD100(int Limit, int count)
    {
        _soundEffectCount = 1;

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
        _soundEffectCount = 1;

        for (int i = 0; i < count; i++)
        {
            int ret = Random.Range(1, 7);
            result += ret;
            Debug.Log($"[{name}] Dice{i + 1} ret = {ret} : result = {result}");
        }
        return result;
    }
}
