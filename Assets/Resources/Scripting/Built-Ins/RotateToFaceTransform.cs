using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToFaceTransform : MonoBehaviour
{
    public Transform target;
    [Range(0f,1f)] public float rotationSpeed = 0.1f;
    public bool lockY = false;

    public void SetRotationTarget(Transform Target){}

    public void UnsetRotationTarget(){}

    public void SetRotationSpeed(float speed){}
}
