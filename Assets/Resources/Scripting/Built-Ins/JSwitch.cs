using Steamworks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;

public class JSwitch : MonoBehaviour
{
    public JSwitchPiece[] branches;
    [Space]
    public JTrigger defaultEvent;

    public void Operate(){}
}

[System.Serializable]
public class JSwitchPiece
{
    public TriggerLogic conditions;
    [Space]
    [MinValue(0)] public int priority = 0;
    public JTrigger events;
}
