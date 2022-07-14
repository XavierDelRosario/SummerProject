using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{   //Controls Camera settings.
    #region Fields
    [SerializeField] private Vector3 CamOffset = new Vector3(0f, 1.2f, -2.6f);

    private PlayerCast playerCast;
    private CinemachineFreeLook virtualCam;
    private bool isCasting;
    #endregion
    void Start()
    {
        playerCast = GameObject.Find("Player").GetComponent<PlayerCast>();
        virtualCam = GameObject.Find("Virtual_Camera").GetComponent<CinemachineFreeLook>();
    }
    private void Update()
    {
        isCasting = playerCast.IsCasting;
    }
    void LateUpdate()
    {
        CinemachineCore.GetInputAxis = GetAxisCustom;
        if (isCasting)
        {
            virtualCam.m_RecenterToTargetHeading.m_enabled = true;
            virtualCam.m_YAxisRecentering.m_enabled = true;           
        }
        else
        {
            virtualCam.m_RecenterToTargetHeading.m_enabled = false;
            virtualCam.m_YAxisRecentering.m_enabled = false;
        } 
    }

    /* 
     Disables freelook with mouse if player is casting a spell.
    */ 
    public float GetAxisCustom(string axisName)
    {   
         if (axisName == "Mouse X")
         {
              if (!isCasting)
              {
                 return UnityEngine.Input.GetAxis("Mouse X");
              }
              else
              {
                 return 0;
              }
         }       
         else if (axisName == "Mouse Y")
         {
              if (!isCasting)
              {
                 return UnityEngine.Input.GetAxis("Mouse Y");
              }
              else
              {
                 return 0;
              }
         }
         return UnityEngine.Input.GetAxis(axisName);
    }
}
