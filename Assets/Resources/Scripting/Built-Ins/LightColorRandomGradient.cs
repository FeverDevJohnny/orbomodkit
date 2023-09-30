using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;

public class LightColorRandomGradient : MonoBehaviour
{
    public Light target;
    public ColorMode mode;
    public Gradient colorRange;

    [ConditionalField("mode", true, ColorMode.Random)]
    public float speed = 1f;
    [Range(0f, 100f)]
    public float strobeDelay;


    public enum ColorMode
    {
        Random = 0,
        Sine = 1,
        Modulus = 2
    }

}
