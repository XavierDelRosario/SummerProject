using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookForwardOffset : MonoBehaviour
{
    //Moves empty Gameobject that follows the parent. This gameobject has a child which is always looking ahead where the camera is facing.
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
        this.transform.rotation = Quaternion.Euler(0f, angle, 0f);
        this.transform.position = target.position;
    }
}
