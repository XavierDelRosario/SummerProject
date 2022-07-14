using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformMovement : MonoBehaviour
{
    //Movement based on Transform. Move direction independent of Camera. Use with 'LockedOnCamera'

    public float MoveSpeed = 10f;
    public float RotateSpeed = 75f;

    private float _hInput;
    private float _vInput;
    // Update is called once per frame

    void Update()
    {
        _vInput = Input.GetAxis("Vertical") * MoveSpeed;

        _hInput = Input.GetAxis("Horizontal") * RotateSpeed;

        this.transform.Translate(Vector3.forward * _vInput * Time.deltaTime);

        this.transform.Rotate(Vector3.up * _hInput * Time.deltaTime);

        //Debug.Log("hInput:" + _hInput);
        //Debug.Log("vInput:" + _vInput);

    }
}
