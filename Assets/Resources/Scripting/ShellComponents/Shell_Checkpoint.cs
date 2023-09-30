using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell_Checkpoint : MonoBehaviour
{
    public Transform spawnPoint;
    [Space]
    public JTrigger onCheckpointActivate;
    public JTrigger onCheckpointStrike;

    public void OnDrawGizmos()
    {
        if (spawnPoint != null)
        {
            Matrix4x4 m = Gizmos.matrix;

            Gizmos.matrix = Matrix4x4.TRS(spawnPoint.position, spawnPoint.rotation, Vector3.one);

            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(Vector3.up * 0.86f, new Vector3(0.6f, 1.6f, 0.6f));

            Vector3 a = Vector3.up * 0.86f, b = a + Vector3.forward * 1f;

            Gizmos.DrawLine(a, b);
            Gizmos.DrawLine(b, b - Vector3.forward * 0.5f + Vector3.Cross(Vector3.forward, Vector3.up) * 0.2f);
            Gizmos.DrawLine(b, b - Vector3.forward * 0.5f - Vector3.Cross(Vector3.forward, Vector3.up) * 0.2f);

            Gizmos.matrix = m;
        }
        else
            Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(transform.position + Vector3.up * 3.73f, 1f);
    }

    public void SetCheckpointAsCurrent()
    {
    }
    public void SetCheckpointAsCurrentQuietly()
    {
    }
}
