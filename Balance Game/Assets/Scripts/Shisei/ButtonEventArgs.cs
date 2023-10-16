using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ButtonEventArgs : EventArgs
{
    public string ButtonName { get; }
    public ButtonEventArgs(string buttonName)
    {
        ButtonName = buttonName;
    }
}
