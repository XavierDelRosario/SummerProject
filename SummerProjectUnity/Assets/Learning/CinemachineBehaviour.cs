using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CinemachineBehaviour : MonoBehaviour
{
    //Camera fixed on the character, no looking around

    public Vector3 CamOffset = new Vector3(0f, 1.2f, -2.6f);

    private GameObject player;

    private PlayerCast playerCast;

    private Transform target;
    private Transform lookTarget;

    private bool isCasting;
    private CinemachineBrain cinemachine;

    private GameObject cam;
    private CinemachineFreeLook virtualCam;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        playerCast = player.GetComponent<PlayerCast>();

        target = GameObject.Find("PlayerLookForward").transform;


        cam = GameObject.Find("VirtualCamera");
        virtualCam = cam.GetComponent<CinemachineFreeLook>();

        cinemachine = GetComponent<CinemachineBrain>();
        lookTarget = GameObject.Find("PlayerLookForward").transform;
    }
    private void Update()
    {
       
    }
    // Update is called once per frame
    void LateUpdate()
    {
        if (isCasting)
        {
            virtualCam.m_RecenterToTargetHeading.m_enabled = true;
            virtualCam.m_YAxisRecentering.m_enabled = true;
            virtualCam.LookAt = target;
            /*
            cinemachine.enabled = false;
            this.transform.position = target.TransformPoint(CamOffset);
            this.transform.LookAt(lookTarget);
            */

        }
        else
        {
            virtualCam.m_RecenterToTargetHeading.m_enabled = false;
            virtualCam.m_YAxisRecentering.m_enabled = false;
            virtualCam.LookAt = player.transform;
            //cinemachine.enabled = true;
        }

    }
}
