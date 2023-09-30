using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;

public class HurtVolume : MonoBehaviour
{
    public bool active = true;
    public Alignment volumeAlignment = Alignment.Neutral;
    public int damageAmount = 1;
    [MinValue(0.0000001f)] public float damageTimer = 0.1f;

    public void SetHurtVolumeState(bool state) { }
    public void SetDamageTimer(float time) { }
    public void SetDamageAmount(int amount) { }
}