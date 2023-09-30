using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
public class LimitString : PropertyAttribute
{
    public int maxLength { get; }
    public LimitString(int MaxLength)
    {
        maxLength = MaxLength;
    }
}

[AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
public class LimitStringTextArea : PropertyAttribute
{
    public float height { get; }
    public int maxLength { get; }
    public LimitStringTextArea(int MaxLength, float Height)
    {
        height = Height;
        maxLength = MaxLength;
    }
}
