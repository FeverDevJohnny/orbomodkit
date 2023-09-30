using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class BindAudioMixer : MonoBehaviour
{
    public MixerGrouping targetMixer = MixerGrouping.Master;
}

public enum MixerGrouping
{ 
    Master = 0,
    Music = 1,
    Effects = 2
}

