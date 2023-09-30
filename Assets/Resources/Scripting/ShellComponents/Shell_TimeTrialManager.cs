using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JTools.Audio;
using MyBox;

public class Shell_TimeTrialManager : MonoBehaviour
{
    public float timeLimit = 30f;
    [MinValue(0f)] public float greenTimeScore = 0f;
    [MinValue(0f)] public float devTimeScore = 0f;
    [Space]
    public string timeTrialID;
    public Shell_TimeTrialRing[] rings;
    [Space]
    public JTrigger onTrialBegin;
    public JTrigger onTrialSuccess;
    public JTrigger onTrialFailure;
    [Space]
    [ConditionalField("onTrialSuccess", true)]
    public Transform gearPartCameraPosition;
    [ConditionalField("onTrialSuccess", true)]
    public Shell_GearPart gearPartToReveal;
    [ConditionalField("onTrialSuccess", true)]
    public AudioClip gearPartRevealSoundOverride;
    [Space]
    public AudioClip ringClearSoundOverride;
    public AudioClip cheerSoundOverride;
    public AudioClip booSoundOverride;

    public BGMVolume timeTrialThemeOverride;

    [ButtonMethod]
    public void ShowRingOrientations()
    {
        if (rings.Length != 0)
        {
            for(int i = 0; i < rings.Length; i++)
            {
                if(rings[i] != null)
                {
                    if(i != rings.Length-1)
                    {
                        if (rings[i + 1] != null)
                            if(!rings[i].overrideStartingRotation)
                            rings[i].transform.rotation = Quaternion.LookRotation((rings[i + 1].transform.position - rings[i].transform.position).normalized);
                    }
                    else
                    {
                        if(rings[i-1] != null)
                            if (!rings[i].overrideStartingRotation)
                                rings[i].transform.rotation = Quaternion.LookRotation((rings[i].transform.position - rings[i-1].transform.position).normalized);
                    }
                }
            }
        }
        else
            Debug.LogError("WARNING! This time trial doesn't have any rings assigned to it! Cannot display ring orientations.");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        if (rings.Length != 0)
            for (int i = 0; i < rings.Length; i++)
                if (rings[i] != null)
                    if (i != rings.Length - 1)
                        if (rings[i + 1] != null)
                            Gizmos.DrawLine(rings[i].transform.position, rings[i+1].transform.position);
    }
}
