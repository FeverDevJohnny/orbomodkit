using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;

public class Shell_GearPart : MonoBehaviour
{
    public string gearPartOverrideID;
    public bool activeByDefault = true;
    [ConditionalField("activeByDefault", true)] public bool poofInWhenActivated = true;
    public JTrigger onCollectionCutsceneEnd;
    public JTrigger onCollectionCutsceneEndFirstTime;

    public void SetGearPartState(bool state){}

    public void OnDrawGizmos()
    {
        Matrix4x4 m = Gizmos.matrix;

        Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.lossyScale);

        Gizmos.color = new Color(1f, 0f, 1f, activeByDefault ? 0.6f : 0.3f);
        Gizmos.DrawSphere(Vector3.zero, 2f);
        Gizmos.color = new Color(1f, 0f, 0.5f, activeByDefault ? 0.6f : 0.3f);
        Gizmos.DrawCube(Vector3.zero, Vector3.one);

        Gizmos.matrix = m;
    }
}
