using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;

public abstract class WorldObject : MonoBehaviour
{
    [Foldout("World Object - General Settings", true)]
    [Separator("- References -")]
    [SerializeField] protected Transform centerOverride;
    public Renderer primaryRenderer;

    [Separator("- Settings -")]
    public bool isInvincible = false;
    public bool hasRecoil = true;
    public float maxWorldObjectHealth = 100f;

    protected float m_currentWorldObjectHealth = 100f;

    public void ForceKill(){}
}

public struct DamageData
{
    public float damage;
    public Vector3 incomingDirection;
    public bool pierceThroughInvuln;
    public Alignment alignment;

    public DamageData(float Damage, Vector3 Incoming, Alignment Alignment = Alignment.Neutral, bool Pierce = false)
    {
        damage = Damage;
        incomingDirection = Incoming;
        pierceThroughInvuln = Pierce;
        alignment = Alignment;
    }
 
}
public enum Alignment
{
    Neutral = 0,
    Player = 1,
    Enemy = 2
}