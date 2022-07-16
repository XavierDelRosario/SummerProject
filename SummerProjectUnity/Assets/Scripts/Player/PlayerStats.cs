using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    [SerializeField] private float maxMana;
    [SerializeField] private float manaRegenRate;
    [SerializeField] private float moveSpeed;
    private float currentHealth;
    private float currentMana;
    private int currency;
    #region Properties
    public float MaxHealth
    {
        get
        {
            return maxHealth;
        }
    }
    public float MaxMana
    {
        get
        {
            return maxMana;
        }
    }
    public float CurrentHealth 
    { 
        get 
        {
            return currentHealth;
        } 
    }
    public float CurrentMana 
    {
        get
        {
            return currentMana;
        }
    }
    public float MoveSpeed
    {
        get
        {
            return moveSpeed;
        }
    }
   
    #endregion
    void Start()
    {
        currentHealth = maxHealth;
        currentMana = maxMana;
    }
    private void Update()
    {
        GainManaOverTime();
    }

    #region Methods
    /// <summary>
    /// Increases player's currentHealth by healAmount upto maxHealth
    /// </summary>
    /// <param name="healAmount"></param>
    public void Heal(float healAmount)
    {
       currentHealth = Mathf.Min(maxHealth, currentHealth += healAmount);
    }
    /// <summary>
    /// Decreases the player's currentHealth by damage
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
    }
    /// <summary>
    /// Checks if player has more currentMana than manaCost
    /// </summary>
    /// <param name="manaCost"></param>
    /// <returns></returns>
    public bool HasMana(float manaCost)
    {
        return (currentMana - manaCost) > 0;
    }
    /// <summary>
    /// Decreases player's currentMana by manaCost
    /// </summary>
    /// <param name="manaCost"></param>
    public void UseMana(float manaCost)
    {
        currentMana -= manaCost;
    }
    /// <summary>
    /// Restores mana overtime
    /// </summary>
    public void GainManaOverTime()
    {
        currentMana = Mathf.Min(maxMana, currentMana += manaRegenRate * Time.deltaTime);
    }
    #endregion
}
