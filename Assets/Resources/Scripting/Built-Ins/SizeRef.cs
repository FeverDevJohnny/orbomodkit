using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;

public class SizeRef : MonoBehaviour
{
    [SerializeField] [ReadOnly]
    Vector3 sizeReference = new Vector3(0.73f,1.4f,0.73f);

    public void OnDrawGizmos()
    {
        Gizmos.color = new Color(1f,1f,0f,0.6f);
        Gizmos.DrawCube(transform.position + Vector3.up * sizeReference.y * 0.5f, sizeReference);
    }
}
