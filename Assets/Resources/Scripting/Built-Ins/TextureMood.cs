using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Mood", menuName = "Create New Mood", order = 0)]
public class TextureMood : ScriptableObject
{
    public Mood state;
    [Space]
    public Texture2D idleFace;
    public Texture2D blinkFace;
    [Space]
    public float talkAnimSpeed = 1f;
    public Texture2D[] talkCycleFace;

    public TextureMood()
    {
        state = Mood.Normal;
    }
}

public enum Mood
{
    Normal = 0,
    Happy = 1,
    Angry = 2,
    Disturbed = 3,
    MiscA = 4,
    MiscB = 5,
    MiscC = 6,
    MiscD = 7
}

