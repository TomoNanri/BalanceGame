using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class KokudakaEventArgs : EventArgs
{
    public int Kokudaka { get; }
    public KokudakaEventArgs(int kokudaka)
    {
        Kokudaka = kokudaka;
    }
}
