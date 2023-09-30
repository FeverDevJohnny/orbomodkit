using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell_TimeTrialRing : MonoBehaviour
{
    public bool overrideStartingRotation = false;

    public void OnDrawGizmos()
    {
        Matrix4x4 m = Gizmos.matrix;

        Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.lossyScale);

        Gizmos.color = new Color(1f, 0.3f, 0f, 0.6f);
        Gizmos.DrawCube(Vector3.zero, new Vector3(8f,8f,2f));

        Gizmos.color = Color.yellow;
        //Gizmos.DrawSphere(Vector3.zero, 1f);
        //Gizmos.DrawMesh(Primitive);

        float ringMag = 4f;
        for (int i = 0; i < 10; i++)
        {
            float t = (i / 10f) * Mathf.PI * 2f, tb = ((i + 1f) / 10f) * Mathf.PI * 2f;
            Vector3 x = new Vector3(Mathf.Cos(t), Mathf.Sin(t), 0f) * ringMag, y = new Vector3(Mathf.Cos(tb), Mathf.Sin(tb), 0f) * ringMag;
            Gizmos.DrawLine(x,y);
            Gizmos.DrawLine(x, x + Vector3.forward);
            Gizmos.DrawLine(x + Vector3.forward, y + Vector3.forward);
        }

        Vector3 a = Vector3.forward, b = a + Vector3.forward * 1f;

        Gizmos.DrawLine(a, b);
        Gizmos.DrawLine(b, b - Vector3.forward * 0.5f + Vector3.Cross(Vector3.forward, Vector3.up) * 0.2f);
        Gizmos.DrawLine(b, b - Vector3.forward * 0.5f - Vector3.Cross(Vector3.forward, Vector3.up) * 0.2f);

        Gizmos.matrix = m;
    }
}
