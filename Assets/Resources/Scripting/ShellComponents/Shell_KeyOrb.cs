using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell_KeyOrb : MonoBehaviour, ITriggerPassthrough
{
    public KeyOrbType type = KeyOrbType.Blue;



    public void OnDrawGizmos()
    {
        switch (type)
        {
            case KeyOrbType.Blue:
                Gizmos.color = new Color(0f,0f,1f,0.5f);
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

        Gizmos.DrawSphere(transform.position, 1f);

    }


    public void OnTriggerEnterPassthrough(Collider collider, string passthroughID)
    {
    }

    public void OnTriggerExitPassthrough(Collider collider, string passthroughID)
    {
    }
}
public enum KeyOrbType
{
    Blue = 0,
    Green = 1,
    Pink = 2,
    Red = 3
}