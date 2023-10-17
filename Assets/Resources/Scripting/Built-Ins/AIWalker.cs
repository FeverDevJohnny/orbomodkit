using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AIWalker : MonoBehaviour
{

    public Transform walkTarget;
    public void SetNavigationState(bool state){}
    public void SetNavigationTarget(Transform target){}
}
