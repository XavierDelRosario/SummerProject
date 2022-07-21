using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Projectile")]
public class ProjectileScriptableObject : ScriptableObject
{
    public float damage;
    public float moveSpeed;
    public float lifeSpanSeconds;
    public float applyForceTime;

    [Header("Optional")]
    public GameObject nextObject;
}
