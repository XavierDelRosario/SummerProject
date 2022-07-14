using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerPlayerMovement : MonoBehaviour
{
    //Movement based on Camera using Character controller, can look around. Used with Cinemachine


    public float MoveSpeed = 100f;
    public float turnSmoothTime = 0.1f;


    private CharacterController controller;
    private Transform cam;

    private float _hInput;
    private float _vInput;
    private float turnSmoothVelocity;
    // Update is called once per frame

    private void Start()
    {
        controller = this.GetComponent<CharacterController>();
        cam = GameObject.Find("Main_Camera").transform;
    }
    void Update()
    {
        _vInput = Input.GetAxis("Vertical");
        _hInput = Input.GetAxis("Horizontal");
        Vector3 direction = new Vector3(_hInput, 0f, _vInput).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            controller.Move(moveDir * MoveSpeed * Time.deltaTime);
        }
    }
}
