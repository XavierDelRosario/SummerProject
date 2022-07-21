using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCast : MonoBehaviour
{
    //Keeps track of what spell the player is trying to cast
    #region Fields
    [SerializeField] private GameObject castingPoint;
    [SerializeField] private GameObject prefabParent;
    [SerializeField] private LayerMask aimColliderLayerMask;
 
    private PlayerState playerState;
    private PlayerStats playerStats;
    private SpellLibrary spellLibrary;
    private Spell spell;

    private GameObject cameraObject;
    private Camera cameraComponent;

    private bool isCasting;
    private bool isFiring;
    private Vector2 screenCenter;

    public string spellID { get; set; }
    public float manaCost { get; set; }
    #endregion
    private void Start()
    {
        playerState = GameObject.Find("Player").GetComponent<PlayerState>();
        playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
        spellLibrary = GameObject.Find("Player").GetComponent<SpellLibrary>();

        cameraObject = GameObject.Find("Main_Camera");
        cameraComponent = cameraObject.GetComponent<Camera>();

        screenCenter = new Vector2(Screen.width / 2f, Screen.height / 2f);
    }
    void Update()
    {
        isCasting = playerState.IsCasting;        
        isFiring = playerState.IsFiring;

        if (!isCasting)
        {
            FindSpell();
        }
        if (isFiring)
        {
            CastSpell();
        }
    }
    #region Methods
    /// <summary>
    /// Find a spell to save and remember.
    /// </summary>
    private void FindSpell()
    { 
            if (spellID != "")
            {
                spell = spellLibrary.FindSpell(spellID);
                manaCost = spell.ManaCost;

                castingPoint.SetActive(true);
                spellID = "";
            }
    }
    /// <summary>
    /// Casts a spell if player has enough mana.
    /// </summary>
    /// <param name="spellID"></param>
    private void CastSpell()
    {        
        if (playerStats.HasMana(manaCost))
        {
            switch (spell.SpellTypeValue)
            {
                case Spell.SpellType.Projectile:
                    SpawnProjectile(spell.SpellObject);
                    break;
                case Spell.SpellType.Placeable:
                    Debug.Log("Place object");
                    break;
                case Spell.SpellType.Special:
                    Debug.Log("Do something special");
                    break;
            }
            playerStats.UseMana(manaCost);
            manaCost = 0;
            spell = spellLibrary.DefaultSpell();

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
            projectile.GetComponent<IShootable>().InitializeProjectile(shootDir, "Player");
            projectile.transform.SetParent(prefabParent.transform);
        }
    }
    /// <summary>
    /// Returns the position of what the player is looking at.
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
