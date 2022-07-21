using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour, IShootable
{
    [SerializeField] private ProjectileScriptableObject projectileScriptableObject;

    //Fields to get from scriptable object
    private float moveSpeed;  
    private float damage;
    private float lifeSpanSeconds;
    private GameObject nextObject;

    //list of tags to deal damage to
    private List<string> collisionTags;
    private Vector3 shootDir;
    private Rigidbody rb;
    #region LifeCycle Methods
    void Start()
    {
        moveSpeed = projectileScriptableObject.moveSpeed;
        damage = projectileScriptableObject.damage;
        lifeSpanSeconds = projectileScriptableObject.lifeSpanSeconds;
        nextObject = projectileScriptableObject.nextObject;

        rb = GetComponent<Rigidbody>();
        DestroyObject(lifeSpanSeconds);
    }
    void FixedUpdate()
    {       
            MoveProjectile();        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(TagManager.CheckTag(other, collisionTags))
        {
            IDamageable damageable = other.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(damage);
                DestroyObject(0f);
            }
        }     
    }
#endregion
    #region Methods
    /// <summary>
    /// Sets the direction the projectile will travel and gets which tags to collide with
    /// </summary>
    /// <param name="shootDir"></param>
    public void InitializeProjectile(Vector3 shootDir, string objectTag)
    {
        this.shootDir = shootDir;
        collisionTags = TagManager.GetEnemyTags(objectTag);
    }
    /// <summary>
    /// Apply continous force in initialized direction
    /// </summary>
    public void MoveProjectile()
    {
        rb.MovePosition(transform.position + shootDir * moveSpeed * Time.fixedDeltaTime);
    }
    private void DestroyObject(float destroyDelaySeconds)
    {
        if(nextObject != null)
        {
            Instantiate(nextObject, transform.position, Quaternion.identity);
        }
        Destroy(gameObject, destroyDelaySeconds);
    }
    #endregion
}
