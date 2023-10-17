using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MonthStateController : AbstractStateController
{
    public enum StateType
    {
        January,
        February,
        March,
        April,
        May,
        June,
        July,
        August,
        September,
        October,
        November,
        December
    }
    public override void Initialize(int initializeStateType)
    {
        stateDic[(int)StateType.January] = gameObject.AddComponent<MonthStateChild_January>();
        stateDic[(int)StateType.January].Initialize((int)StateType.January);

        stateDic[(int)StateType.February] = gameObject.AddComponent<MonthStateChild_February>();
        stateDic[(int)StateType.February].Initialize((int)StateType.February);

        stateDic[(int)StateType.March] = gameObject.AddComponent<MonthStateChild_March>();
        stateDic[(int)StateType.March].Initialize((int)StateType.March);

        stateDic[(int)StateType.April] = gameObject.AddComponent<MonthStateChild_April>();
        stateDic[(int)StateType.April].Initialize((int)StateType.April);

        stateDic[(int)StateType.May] = gameObject.AddComponent<MonthStateChild_May>();
        stateDic[(int)StateType.May].Initialize((int)StateType.May);

        stateDic[(int)StateType.June] = gameObject.AddComponent<MonthStateChild_June>();
        stateDic[(int)StateType.June].Initialize((int)StateType.June);

        stateDic[(int)StateType.July] = gameObject.AddComponent<MonthStateChild_July>();
        stateDic[(int)StateType.July].Initialize((int)StateType.July);

        stateDic[(int)StateType.August] = gameObject.AddComponent<MonthStateChild_August>();
        stateDic[(int)StateType.August].Initialize((int)StateType.August);

        stateDic[(int)StateType.September] = gameObject.AddComponent<MonthStateChild_September>();
        stateDic[(int)StateType.September].Initialize((int)StateType.September);

        stateDic[(int)StateType.October] = gameObject.AddComponent<MonthStateChild_October>();
        stateDic[(int)StateType.October].Initialize((int)StateType.October);

        stateDic[(int)StateType.November] = gameObject.AddComponent<MonthStateChild_November>();
        stateDic[(int)StateType.November].Initialize((int)StateType.November);

        stateDic[(int)StateType.December] = gameObject.AddComponent<MonthStateChild_December>();
        stateDic[(int)StateType.December].Initialize((int)StateType.December);

        CurrentState = initializeStateType;
        stateDic[CurrentState].OnEnter();
    }
    public void AddCalendarEvent(int tsuki, Action action)
    {
        var element = (MonthStateChild)stateDic[tsuki];
        element.AddSingleShotEventOnEnter(action);
    }
}
