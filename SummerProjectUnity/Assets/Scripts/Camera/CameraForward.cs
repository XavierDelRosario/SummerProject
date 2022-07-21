using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CameraForward : MonoBehaviour
{
    //Moves empty Gameobject that will always be the direction the camera is facing.
    private Transform target;
    private Transform cam;
    void Start()
    {
        cam = GameObject.Find("Main_Camera").transform;
        target = GameObject.Find("Player").transform;
    }
    void Update()
    {
        float angle = cam.eulerAngles.y;
        transform.rotation = Quaternion.Euler(0f, angle, 0f);
        transform.position = target.position;
    }
}
