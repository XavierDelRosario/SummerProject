using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{   //Controls Camera settings.
    #region Fields
    private PlayerState playerState;
    private CinemachineFreeLook virtualCam;
    private bool isCasting;
    #endregion
    void Start()
    {
        playerState = GameObject.Find("Player").GetComponent<PlayerState>();
        virtualCam = GameObject.Find("Virtual_Camera").GetComponent<CinemachineFreeLook>();
    }
    private void Update()
    {
        isCasting = playerState.IsCasting;
    }
    void LateUpdate()
    {
        CinemachineCore.GetInputAxis = GetAxisCustom;
        CenterCam(isCasting);
    }
    /// <summary>
    /// Disables freelook with mouse if player is casting a spell.
    /// </summary>
    /// <param name="axisName"></param>
    /// <returns></returns>
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
    /// <summary>
    /// Recenters the camera to face forward depending on given bool.
    /// </summary>
    /// <param name="shouldRecenter"></param>
    private void CenterCam(bool shouldRecenter)
    {
        virtualCam.m_RecenterToTargetHeading.m_enabled = shouldRecenter;
        virtualCam.m_YAxisRecentering.m_enabled = shouldRecenter;
    }
}
