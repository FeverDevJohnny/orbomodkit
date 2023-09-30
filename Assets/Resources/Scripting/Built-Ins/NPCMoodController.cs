using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using MyBox;

public class NPCMoodController : MonoBehaviour
{
    [Separator("-= Mood Configuration =-")]

    public Mood npcState = Mood.Normal;
    public TextureMood[] moodPool;

    [Separator("-= Rendering =-")]

    public Renderer targetRenderer;
    public int targetMaterialIndex = 0;
    public string[] targetProperties = new string[0];

    [Separator("-= Animation Drivers =-")]

    public float blinkDelay = 1f;
    public float blinkTime = 1f;

    public Dictionary<Mood, TextureMood> moods = new Dictionary<Mood, TextureMood>(0);

    public void ChangeMood(int targetMood){}
}
