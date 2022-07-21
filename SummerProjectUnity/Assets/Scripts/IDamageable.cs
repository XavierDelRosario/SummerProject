using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    void TakeDamage(float damage);
}
public interface IShootable
{
    void InitializeProjectile(Vector3 shootDir, string objectTag);
    void MoveProjectile();
}

