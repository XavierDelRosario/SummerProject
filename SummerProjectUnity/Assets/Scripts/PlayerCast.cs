using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCast : MonoBehaviour
{
    //Determines when player is casting a spell. Keeps track of what that spell is.
    #region Fields
    [SerializeField] private GameObject castingUI;
    [SerializeField] private GameObject castingPoint;
    [SerializeField] private GameObject fireBall;
    [SerializeField] private GameObject rockBall;
    [SerializeField] private GameObject waterBall;

    [SerializeField]
    private LayerMask aimColliderLayerMask;

    [SerializeField]
    private Transform debugTransform;

    private string spellID = "";
    private string savedSpellID = "";
    private bool isCasting;
    private Vector2 screenCenter = new Vector2(Screen.width / 2f, Screen.height / 2f);
    private Camera cam;

    #endregion
    private void Start()
    {
        cam = GameObject.Find("Main_Camera").GetComponent<Camera>();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {           
            isCasting = true;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            castingUI.SetActive(true);
        } 
        else if (Input.GetMouseButtonUp(1))
        {
            isCasting = false;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;         
            castingUI.SetActive(false);

            if(spellID != "")
            {
                savedSpellID = spellID;
                castingPoint.SetActive(true);
            }
            spellID = "";
        }

        if(Input.GetMouseButtonDown(0))
        {
            Cast(savedSpellID);
        }
    }
    #region Methods
    /*
     Allows other scripts to access isCasting.
     */
    public bool IsCasting
    {
        get { return isCasting; }
    }
    /*
     Allows other scripts to access spellID.
     */
    public string SpellID
    {
        get { return spellID; }
        set { spellID = value; }
    }
    /*
     Casts a spell depending on the given ID.
     */
    private void Cast(string spellID)
    {
        switch (spellID)
        {
            case "1": 
                    SpawnProjectile(fireBall);
                 break;
            case "2":
                SpawnProjectile(rockBall);
                break;
            case "3":
                SpawnProjectile(waterBall);
                break;

            default:
                Debug.Log("That spell doesn't exist");
                break;
        }
        Debug.Log("Cast spell " + spellID);
        savedSpellID = "";
        castingPoint.SetActive(false);
    }
    /*
     Returns the position of what the player is lookin at.
     */
    private Vector3 AimProjectile()
    {
        Ray ray = cam.ScreenPointToRay(screenCenter);
        if(Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimColliderLayerMask))
        {
            return raycastHit.point;
        }
        else
        {
            Debug.Log("Raycast didn't hit anything");
            return new Vector3(0f, 0f, 0f);
        }
    }
    /*
     Instatiates a projectile and launches it.
     */
    private void SpawnProjectile(GameObject pfProjectile)
    {
        GameObject projectile = Instantiate(pfProjectile, castingPoint.transform.position, Quaternion.identity);
        Vector3 shootDir = (AimProjectile() - castingPoint.transform.position).normalized;
        projectile.GetComponent<ProjectileBehaviour>().ReceiveDirection(shootDir);
    }
    #endregion
}
