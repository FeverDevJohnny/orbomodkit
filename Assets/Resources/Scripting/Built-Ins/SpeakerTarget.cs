using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeakerTarget : MonoBehaviour, ISpeaker
{
    public NPCMoodController moodController;

    public void OnSpeakBegin(){}

    public void OnSpeakEnd(){}

    public void SetCharacterMood(int mood){}
}


public interface ISpeaker
{
    public void OnSpeakBegin();
    public void OnSpeakEnd();
}
