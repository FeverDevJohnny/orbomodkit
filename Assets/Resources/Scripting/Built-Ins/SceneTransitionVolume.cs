using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransitionVolume : MonoBehaviour
{
    [Tooltip("This is the name of the target scene to load. This scene MUST be included in your mod scenes list (found inside the mod asset while the mod toolkit window is open).")] public string destinationScene;
    [Tooltip("You can customize the color of your transition with this!")] public Color fadeColor = Color.black;
    [Tooltip("This is how long (in seconds) it takes for the game to fade out.")] public float fadeTime = 1f;
    public Vector3 destinationPosition;
    public float destinationRotationYaw;
    [Space]
    [Tooltip("This will make Orbo explode when he makes contact with this volume! It's the same effect as when you enter portals in the main campaign.")] public bool popOrbo = false;

    public void BeginSceneTransition()
    {

    }
}
