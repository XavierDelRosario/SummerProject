using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCast : MonoBehaviour
{
    //Determines when player is casting a spell. Keeps track of what that spell is.
    #region Fields
    [SerializeField] private GameObject castingPoint;
    [SerializeField] private GameObject prefabParent;
    [SerializeField] private LayerMask aimColliderLayerMask;
    public string spellID { get; set; }
    private string savedSpellID = "";
   
    private Vector2 screenCenter = new Vector2(Screen.width / 2f, Screen.height / 2f);
    private GameObject cameraObject;
    private Camera cameraComponent;
    private PlayerState playerState;
    private PlayerSpells playerSpells;
    private PlayerStats playerStats;
    private bool isCasting;
    private bool isFiring;
    #endregion
    private void Start()
    {
        cameraObject = GameObject.Find("Main_Camera");
        cameraComponent = cameraObject.GetComponent<Camera>();

        playerState = GameObject.Find("Player").GetComponent<PlayerState>();
        playerSpells = GameObject.Find("Player").GetComponent<PlayerSpells>();
        playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
    }
    void Update()
    {
        isCasting = playerState.IsCasting;        
        isFiring = playerState.IsFiring;

        if (!isCasting)
        {
            if (spellID != "")
            {
                savedSpellID = spellID;
                castingPoint.SetActive(true);
            }
            spellID = "";
        }
        if (isFiring)
        {
            Cast(savedSpellID);
        }
    }
    #region Methods
    /// <summary>
    /// Casts a spell depending on the given ID.
    /// </summary>
    /// <param name="spellID"></param>
    private void Cast(string spellID)
    {
        Spell spell = playerSpells.FindSpell(spellID);
        float manaCost = spell.ManaCost;
        if (playerStats.HasMana(manaCost))
        {
            if (spell.SpellTypeValue == 0)
            {
                SpawnProjectile(spell.SpellObject);
            }
            playerStats.UseMana(manaCost);
            Debug.Log("Cast spell " + spellID);
            savedSpellID = "";
            castingPoint.SetActive(false);
        }
    }
    /// <summary>
    /// Instatiate and launch projectile towards where camera is pointed.
    /// </summary>
    /// <param name="pfProjectile">Object to instatiate</param>
    private void SpawnProjectile(GameObject pfProjectile)
    {
        if(pfProjectile != null)
        {
            Quaternion quaternion = Quaternion.Euler(cameraObject.transform.eulerAngles.x, cameraObject.transform.eulerAngles.y, cameraObject.transform.eulerAngles.z);
            GameObject projectile = Instantiate(pfProjectile, castingPoint.transform.position, quaternion);
            Vector3 shootDir = (AimProjectile() - castingPoint.transform.position).normalized;
            projectile.GetComponent<ProjectileBehaviour>().SetDirection(shootDir);
            projectile.transform.SetParent(prefabParent.transform);
        }
    }
    /// <summary>
    /// Returns the position of what the player is lookin at.
    /// </summary>
    /// <returns></returns>
    private Vector3 AimProjectile()
    {
        Ray ray = cameraComponent.ScreenPointToRay(screenCenter);
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
    #endregion
}
