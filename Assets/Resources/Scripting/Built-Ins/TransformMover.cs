using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;

public class TransformMover : MonoBehaviour
{
    [MinValue(0.00001f)] public float positionTime = 5f;
    [MinValue(0.00001f)] public float rotationTime = 5f;

    public Transform goalTransform;
    public AnimationCurve positionLerp = AnimationCurve.Linear(0,0,1,1);
    public AnimationCurve rotationLerp = AnimationCurve.Linear(0,0,1,1);

    public void SetGoalPositionTime(float time){}
    public void SetGoalRotationTime(float time){}
    public void SetTransformGoal(Transform Target){}
    public void UnsetTransformGoal() { }

}