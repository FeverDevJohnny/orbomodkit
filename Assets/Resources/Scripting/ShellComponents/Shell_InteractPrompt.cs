using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell_InteractPrompt : MonoBehaviour
{
    public Interactable target;
    [Space]
    public Vector3 promptActiveDisplacement = Vector3.up * 1f;

    public void OnDrawGizmos()
    {
        Matrix4x4 m = Gizmos.matrix;

        Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.lossyScale);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(Vector3.zero, 0.3f);
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(Vector3.zero, promptActiveDisplacement);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(promptActiveDisplacement, 0.3f);

        Gizmos.matrix = m;
    }
}
