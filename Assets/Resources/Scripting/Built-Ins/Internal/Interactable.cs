using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;
public abstract class Interactable : WorldObject, ITriggerPassthrough
{
    public bool isInteractable = true;

    public void SetInteractableState(bool state) {}

    public virtual void OnTriggerEnterPassthrough(Collider collider, string passthroughID){}

    public virtual void OnTriggerExitPassthrough(Collider collider, string passthroughID){}
}
