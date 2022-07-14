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
        rb = this.GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
       rb.MovePosition(this.transform.position + shootDir * moveSpeed * Time.fixedDeltaTime); 
    }
    /*
      Receives direction to travel
     */
    public void ReceiveDirection(Vector3 shootDir)
    {
        this.shootDir = shootDir;
    }
    private void OnTriggerEnter(Collider other)
    {
        //Do something later
    }
}
