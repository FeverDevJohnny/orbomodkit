using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;

public class Shell_PipManager : MonoBehaviour
{
    [MinValue(0)] public int totalPips = 200;
    [Space]
    [ConditionalField("onCollectAllPips", true)] public Transform gearPartCameraPosition;
    [ConditionalField("onCollectAllPips", true)] public Shell_GearPart gearPartToReveal;
    [ConditionalField("onCollectAllPips", true)] public AudioClip gearPartRevealSoundOverride;
    [Space]

    public JTrigger onCollectAllPips;
}
