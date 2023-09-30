using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;

public class Shell_DoorSlab : MonoBehaviour
{

    public KeyOrbType type = KeyOrbType.Blue;
    public Vector3 displacementGoal = Vector3.up * -18f;
    public float displacementSpeed = 3f;
    public Vector3 dustOffset = Vector3.up * 4.2f;
    [Space]
    public JTrigger onUnlockDoorSlab;

    public void OnDrawGizmos()
    {
        Matrix4x4 m = Gizmos.matrix;

        Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.localScale);

        switch (type)
        {
            case KeyOrbType.Blue:
                Gizmos.color = new Color(0f, 0f, 1f, 0.5f);
                break;

            case KeyOrbType.Green:
                Gizmos.color = new Color(0f, 1f, 0f, 0.5f);
                break;

            case KeyOrbType.Pink:
                Gizmos.color = new Color(1f, 0f, 1f, 0.5f);
                break;

            case KeyOrbType.Red:
                Gizmos.color = new Color(1f, 0f, 0f, 0.5f);
                break;
        }

        Gizmos.DrawCube(Vector3.up * 10.6f, new Vector3(16.5f, 21.2f, 2.6f));

        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(dustOffset, new Vector3(17f, 0f, 3.1f));
        Vector3 a = new Vector3(0f, 10.6f, 1.55f), b = a + Vector3.forward * 2f;

        Gizmos.DrawLine(a, b);
        Gizmos.DrawLine(b, b - Vector3.forward * 1f + Vector3.Cross(Vector3.forward, Vector3.up) * 0.4f);
        Gizmos.DrawLine(b, b - Vector3.forward * 1f - Vector3.Cross(Vector3.forward, Vector3.up) * 0.4f);

        Gizmos.matrix = m;
    }
}
