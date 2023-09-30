using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeakerTarget_Animator : MonoBehaviour, ISpeaker
{
    public void OnSpeakBegin()
    {
        targetAnimator.SetBool(isTalkingProperty, true);
    }

    public void OnSpeakEnd()
    {
        targetAnimator.SetBool(isTalkingProperty, false);
    }

    public Animator targetAnimator;
    [Space]
    [Tooltip("Your animator should incorporate this as a boolean value.")] public string isTalkingProperty;
    [Tooltip("To change moods, use SetCharacterMood on this speaker object.\nYour animator should incorporate this as an integer value.\n\nREF SHEET:\nNormal = 0\nHappy = 1\nAngry = 2\nDisturbed = 3\n")] public string moodProperty;

    public void SetCharacterMood(int mood)
    {
        targetAnimator.SetInteger(moodProperty, mood);
    }
}
