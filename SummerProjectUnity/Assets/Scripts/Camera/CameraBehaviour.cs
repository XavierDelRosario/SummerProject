using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
public class CameraBehaviour : MonoBehaviour
{   //Controls Camera settings.
    #region Fields
    private PlayerState playerState;
    private bool isCasting;
    #endregion
    void Start()
    {
        playerState = GameObject.Find("Player").GetComponent<PlayerState>();
    }
    private void Update()
    {
        isCasting = playerState.IsCasting;
    }
    void LateUpdate()
    {
        CinemachineCore.GetInputAxis = GetAxisCustom;
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
}
