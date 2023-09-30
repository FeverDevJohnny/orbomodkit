using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;

public class LevelBinder : MonoBehaviour
{
    [Foldout("Level Binder - Settings")]
    public float killPlaneHeight = -50f;
    [Foldout("Level Binder - Events", true)]
    public JTrigger onPlayerDeath;
    public JTrigger onPlayerGetGearPart;

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1f,0f,0f,0.5f);
        Gizmos.DrawCube(Vector3.up * killPlaneHeight, new Vector3(1000f,0f,1000f));
    }
}
