using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerPassthrough : MonoBehaviour
{
    public GameObject target;
    public string passthroughID = "general";
}

public interface ITriggerPassthrough
{
    public void OnTriggerEnterPassthrough(Collider collider, string passthroughID);
    public void OnTriggerExitPassthrough(Collider collider, string passthroughID);
}
