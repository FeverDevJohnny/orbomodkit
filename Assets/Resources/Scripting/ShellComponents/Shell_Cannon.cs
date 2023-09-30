using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell_Cannon : MonoBehaviour, ITriggerPassthrough
{
    public bool overridePlayerControl = false;
    public bool enableSpotlight = true;
    public Transform cannonHead;
    public float launchForce = 100;
    [Space]
    public JTrigger onCannonEnter;
    public JTrigger onCannonExit;

    public void OnDrawGizmos()
    {
        Matrix4x4 m = Gizmos.matrix;

        Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.lossyScale);

        Gizmos.color = new Color(1f, 0f, 1f, 0.6f);
        Gizmos.DrawCube(Vector3.up*0.125f, new Vector3(1.5f,0.25f,1.5f));

        if (cannonHead != null)
        {
            Gizmos.matrix = Matrix4x4.TRS(cannonHead.position, cannonHead.rotation, Vector3.one);
            Gizmos.DrawCube(Vector3.zero, Vector3.one * 0.75f);
            Gizmos.DrawCube(Vector3.forward * 0.25f, new Vector3(0.5f, 0.5f, 1f));
        }

        Gizmos.color = Color.red;
        Gizmos.DrawLine(Vector3.forward * 0.25f, Vector3.forward * 0.25f + Vector3.forward * launchForce/5f);

        Gizmos.matrix = m;
    }

    public void LaunchPlayer() { }

    public void OnTriggerEnterPassthrough(Collider collider, string passthroughID){}

    public void OnTriggerExitPassthrough(Collider collider, string passthroughID){}
}
