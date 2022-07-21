using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Spell
{
    //Blueprint for a spell.
    /* Projectile launches a gameobject in a direction
     * Plaeable Instatiates object in a position, wall, ally etc.
     * Special requires specific code.
     */
    public enum SpellType { Projectile, Placeable, Special };

    private string spellID;
    private float manaCost;
    private GameObject spellObject;
    private SpellType spellType;
    public Spell(string spellID, float manaCost, GameObject spellObject, SpellType spellType)
    {
        this.spellID = spellID;
        this.manaCost = manaCost;
        this.spellObject = spellObject;
        this.spellType = spellType;
    }
    #region Properties
    public float ManaCost
    {
        get
        {
            return manaCost;
        }
    }
    public string SpellID
    {
        get
        {
            return spellID;
        }
    }
    public GameObject SpellObject
    {
        get
        {
            return spellObject;
        }
    }
    public SpellType SpellTypeValue
    {
        get
        {
            return spellType;
        }
    }
    #endregion
}

