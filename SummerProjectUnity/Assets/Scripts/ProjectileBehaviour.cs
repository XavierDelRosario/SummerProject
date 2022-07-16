using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    [SerializeField] private float moveSpeed; 
    [SerializeField] private float lifeSpan;
    [SerializeField] private float damage;

    private Vector3 shootDir;
    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Destroy(gameObject, lifeSpan);
    }
    void FixedUpdate()
    {
       rb.MovePosition(transform.position + shootDir * moveSpeed * Time.fixedDeltaTime); 
    }
    /// <summary>
    /// Sets the direction the projectile will travel
    /// </summary>
    /// <param name="shootDir"></param>
    public void SetDirection(Vector3 shootDir)
    {
        this.shootDir = shootDir;
    }
    private void OnTriggerEnter(Collider other)
    {
        //Do something later
    }
}
