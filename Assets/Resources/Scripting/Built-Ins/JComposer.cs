using JTools;
using MyBox;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class JComposer : MonoBehaviour
{
    public bool loop = false;
    [Space]
    public JSequencePart[] sequence;

    public GameObject[] speakerTargets;

    public void SetComposerState(bool state)
    {
    }

    public void SetPlayerLockState(bool state)
    {

    }

    public void SetPlayerTransform(Transform target)
    {

    }

    public void SetPlayerControlState(bool state)
    {

    }

    public void SetPlayerArtRootLookOverride(Transform target)
    {

    }

    public void SetPlayerCinematicBarState(bool state)
    {

    }

    public void SetPlayerCameraPosition(Transform position)
    {

    }

    public void SetPlayerMood(int mood)
    {

    }

    public void SetPlayerViewTarget(Transform target)
    {
        

    }

    public void PlaySoundEffect(AudioClip sound)
    {

    }

    public void ReturnPlayerCamera()
    {

    }

    public void SetPlayerCameraShake(float level)
    {

    }

    private void OnDestroy()
    {

    }

    public void SetPlayerRunTarget(Transform target)
    {
    }

    public void UnsetPlayerRunTarget()
    {
    }


    public void UnsetPlayerArtRootOverride()
    {
    }
}

[System.Serializable]
public class JSequencePart
{
    public SequenceProgressType progressType;
    [ConditionalField("progressType", false, SequenceProgressType.Delay)]
    public float delayTime = 0f;
    [ConditionalField("progressType", false, SequenceProgressType.Dialog)]
    public string speakerName;
    [ConditionalField("progressType", false, SequenceProgressType.Dialog)]
    [TextArea(3,5)]
    public string speakerDialog;
    [ConditionalField("progressType", false, SequenceProgressType.Dialog)]
    public AudioClipCollection speakerTalkSound;
    [ConditionalField("progressType", false, SequenceProgressType.Dialog)]
    public ConversationSpeakerTarget speakerTarget;
    [Space]
    public bool cameraChangesAreSnap = true;
    public UnityEvent sequenceEvent;
}

public enum SequenceProgressType
{ 
    Delay = 0,
    AnyKey = 1,
    Dialog = 2,
}


public enum ConversationSpeakerTarget
{
    Narration = 0,
    Player = 1,
    NPC0 = 2,
    NPC1 = 3,
    NPC2 = 4,
    NPC3 = 5,
    NPC4 = 6,
    NPC5 = 7,
    NPC6 = 8,
    NPC7 = 9,
    NPC8 = 10,
    NPC9 = 11,
}

[System.Serializable] public class AudioClipCollection : CollectionWrapper<AudioClip> { }
