using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;

public class Damageable_Events : Damageable
{
    [Foldout("Damageable Events - Triggers", true)]
    public JTrigger onDamageableStart;
    public JTrigger onDamageableTakeDamage;
    public JTrigger onDamageableDeath;
}
