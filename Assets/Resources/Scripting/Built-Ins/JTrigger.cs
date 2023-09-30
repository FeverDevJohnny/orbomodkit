using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using MyBox;

public class JTrigger : MonoBehaviour, ITriggerPassthrough
{
    [Tooltip("If true, this trigger will only fire once until the scene is reloaded. If false, it can fire an infinite amount of times.")] public bool oneShot = false;
    public EventTriggerContext context;
    [ConditionalField("context", false, EventTriggerContext.OnTrigger, EventTriggerContext.OnTriggerExit)] [Tooltip("Determines what tag this trigger should detect before firing off an event.\n\nThe other object MUST have a rigidbody for trigger detection to work.")] [Tag] public string tagToSearchFor = "Player";
    [ConditionalField("context", false, EventTriggerContext.OnTimer)] [Tooltip("This range defines a random value for a trigger event to fire. You can set both values to the same amount if you want a reliable timer.")] public Vector2 triggerTimerRange;
    public TriggerLogic triggerRequirement;
    public UnityEvent events;
    public UnityEvent eventsOnFail;

    public static Dictionary<string, int> triggers = new Dictionary<string, int>(0);

    public void Start(){}

    public void Update(){}

    public void OnDemand(){}

    public void TriggerSet(string setCommand){}
    public void TriggerAdd(string addCommand){}
    public void TriggerSubtract(string subCommand) { }
    public void TriggerMultiply(string mulCommand) { }
    public void TriggerDivide(string divCommand) { }
    public void PlaySound(AudioClip sound){}
    public void PlaySoundNormalPitch(AudioClip sound){}
    public void SetCameraShake(float amount){}
    public void PlaySoundAtPosition(AudioClip sound){}
    public void PushTutorialMessage(string command){}
    public static void ModifyTrigger(string address, TriggerModification operation, int value){}
    public void SetPlayerCameraPosition(Transform position){}
    public void ReturnPlayerCamera(){}
    public void SetPlayerMood(int mood){}
    public void SetPlayerViewTarget(Transform target){}
    public void SetPlayerControlState(bool state){}
    public void SetPlayerRunTarget(Transform target){}
    public void UnsetPlayerRunTarget(){}
    public void HealPlayer(int amount){}
    public void HurtPlayer(int amount){}

    public void SetPlayerArtRootLookOverride(Transform target){ }
    public void UnsetPlayerArtRootOverride(){}
    public void SetOneShotState(bool state) { }
    public void OnTriggerEnterPassthrough(Collider collider, string passthroughID){}
    public void OnTriggerExitPassthrough(Collider collider, string passthroughID){}
}

public enum EventTriggerContext
{ 
    OnStart = 0,
    OnTrigger = 1,
    OnTimer = 2,
    OnTriggerExit = 3,
    OnDemand = 99
}

[System.Serializable]
public struct TriggerComparison
{
    public string triggerAddress;
    public ComparisonOperation operation;
    public int referenceValue;
}

public enum TriggerModification
{
    Set = 0,
    Add = 1,
}



[System.Serializable]
public struct TriggerLogic
{
    public TriggerComparison[] comparison;
    public LogicOperation[] logic;
    public bool[] foldouts;
}


public enum ComparisonOperation
{ 
    Equals = 0,
    NotEquals = 1,
    Less = 2,
    Greater = 3,
    LessEqual = 4,
    GreaterEqual = 5
}

public enum LogicOperation
{
    AND = 0,
    NAND = 1,
    OR = 2,
    EXOR = 3,
    NOR = 4,
    EXNOR = 5
}

