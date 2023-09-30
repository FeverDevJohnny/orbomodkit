using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell_GustPlate : MonoBehaviour
{
    public float launchForce = 50f;
    [Space]
    public JTrigger onGustPlateUse;

    public void OnDrawGizmos()
    {
        Matrix4x4 m = Gizmos.matrix;

        Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.lossyScale);

        Gizmos.color = new Color(1f, 0f, 1f, 0.6f);
        Gizmos.DrawCube(Vector3.up * 0.22f, new Vector3(4f, 0.7f, 4f));

        Gizmos.color = Color.red;
        Gizmos.DrawLine(Vector3.up * 0.22f, Vector3.up * 0.25f + (launchForce * launchForce / 60f * Vector3.up));

        Gizmos.matrix = m;
    }
}
