using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellLibrary : MonoBehaviour
{
    //Information for all possible player spells. Like prefabs;
    [Header("Projectiles")]
    [SerializeField] private GameObject fireBall;
    [SerializeField] private GameObject rockBall;
    [SerializeField] private GameObject giantFireBall;
    [SerializeField] private GameObject giantRockBall;
    [SerializeField] private GameObject waterBall;

    private Spell[] spellArray; 
    private void Awake()
    {
        spellArray = new Spell[6] 
        {
            new Spell("0", 0f, null, Spell.SpellType.Projectile),
            new Spell("1", 10f, fireBall, Spell.SpellType.Projectile),
            new Spell("2", 10f, rockBall, Spell.SpellType.Projectile),
            new Spell("3", 10f, waterBall, Spell.SpellType.Projectile),
            new Spell("123", 75f, giantFireBall, Spell.SpellType.Projectile),
            new Spell("213", 75f, giantRockBall, Spell.SpellType.Projectile)
        };
    }
    /// <summary>
    /// Finds the matching Spell from spellArray
    /// </summary>
    /// <param name="spellID"> ID to search for</param>
    /// <returns></returns>
    public Spell FindSpell(string spellID)
    {
        Spell foundSpell = spellArray[0];
        for (int i = 0; i < spellArray.Length; i++)
        {   
            if(spellID == spellArray[i].SpellID)
            {
                foundSpell = spellArray[i];
                break; 
            }        
        }
        return foundSpell;
    }
    /// <summary>
    ///  Returns a placeholder spell, this spell does nothing;
    /// </summary>
    /// <returns></returns>
    public Spell DefaultSpell()
    {
        //spellArray[0] is the default spell if a spell is missing. It has no functionality except to be a placeholder.
        return spellArray[0];
    }
}
