using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonthStateChild_December : AbstractMonthStateChild
{
    private Oshiro _oshiro;
    private List<Nouchi> _nouchi = new List<Nouchi>();
    public override void Initialize(int stateType)
    {
        _oshiro = GameObject.FindAnyObjectByType<Oshiro>();
        _nouchi.Add(GameObject.Find("UshitoraMura/Nouchi").GetComponent<Nouchi>());
        _nouchi.Add(GameObject.Find("InuiMura/Nouchi").GetComponent<Nouchi>());
        _nouchi.Add(GameObject.Find("HitsujisaruMura/Nouchi").GetComponent<Nouchi>());
        _nouchi.Add(GameObject.Find("TatsumiMura/Nouchi").GetComponent<Nouchi>());
        base.Initialize(stateType);
    }
    public override void OnExit()
    {
        if (_koyomi.IsTaLevelUpOrdered)
        {
            _koyomi.IsTaLevelUpOrdered = false;
            _oshiro.TaLevel += 1;
            foreach(Nouchi n in _nouchi)
            {
                n.SetUpdateFlag();
            }
        }
    }
    public override int StateUpdate()
    {
        if (_koyomi.IsNewMonth)
        {
            _koyomi.IsNewMonth = false;
            return (int)MonthStateController.StateType.January;
        }
        return (int)StateType;
    }
}
