using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidbodyMovement : MonoBehaviour
{
    //Movement based on Rigidbody. Move direction depends on camera.  Used with 'Locked On Behaviour'.

    public float MoveSpeed = 10f;
    public float RotateSpeed = 75f;

    private float _hInput;
    private float _vInput;

    private Rigidbody _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        _vInput = Input.GetAxis("Vertical") * MoveSpeed;

        _hInput = Input.GetAxis("Horizontal") * RotateSpeed;

        //Debug.Log("hInput:" + _hInput);
        //Debug.Log("vInput:" + _vInput);
    }

    private void FixedUpdate()
    {
        Vector3 rotation = Vector3.up * _hInput;

        Quaternion angleRot = Quaternion.Euler(rotation * Time.fixedDeltaTime);

        _rb.MovePosition(this.transform.position + this.transform.forward * _vInput * Time.fixedDeltaTime);

        _rb.MoveRotation(_rb.rotation * angleRot);
    }
}
