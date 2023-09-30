using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;

public class ScreenShaker : MonoBehaviour
{
    public bool active = true;
    [Space]
    public bool global = false;
    [ConditionalField("global", true)] [Space] [MinValue(0f)] public float minimumRange;
    [ConditionalField("global", true)] [MinValue(0f)] public float maximumRange;
    [Space]
    public float intensity = 0.1f;
    public void OnDrawGizmosSelected()
    {
        if (!global)
        {
            Gizmos.color = new Color(0f,0.5f,1f,0.5f);
            Gizmos.DrawWireSphere(transform.position, minimumRange);
            Gizmos.color = new Color(0f, 1f, 0.5f, 1f);
            Gizmos.DrawWireSphere(transform.position, maximumRange);
        }
    }

    public void SetShakerState(bool state){}
}
