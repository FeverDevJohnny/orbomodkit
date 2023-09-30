using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;

public class ConstantMover : MonoBehaviour
{
    public bool isActive = true;
    [Space]
    public Transform overrideTarget;
    [Space]
    [ConditionalField("overrideTarget", false)]
    [MinValue(0f)] public float stoppingDistance = 1f;
    [ConditionalField("overrideTarget", true)]
    public Vector3 direction = Vector3.forward;
    [ConditionalField("overrideTarget", true)]
    public bool relative = false;
    [Space]
    public bool lookAtMovementDirection = false;
    [ConditionalField("lookAtMovementDirection", false)] [Range(0.01f, 1f)] public float lookAtSpeed = 0.1f;

    [ConditionalField("overrideTarget", false)]
    public bool lerpToTarget = false;
    bool DisplayLerpSpeed() => overrideTarget && lerpToTarget;
    [ConditionalField(true, nameof(DisplayLerpSpeed))]
    [Range(0f, 1f)] public float lerpSpeed = 0.1f;

    bool DisplayMoveSpeed() => (!lerpToTarget || !overrideTarget) && overrideTarget != null;
    [ConditionalField(true, nameof(DisplayMoveSpeed))]
    public float moveSpeed = 1f;

    public void SetMoverOverrideTarget(Transform target) { }

    public void UnsetMoverOverrideTarget() { }

    public void SetMoverSpeed(float speed) { }

    public void SetStoppingDistance(float distance){}

    public void SetDirectionTowards(Transform target) { }
    public void SetDirectionAlignedWith(Transform target){ }
    public void SetLerpSpeed(float lerpSpeed) { }
    public void SetMoverState(bool active) { }
}
