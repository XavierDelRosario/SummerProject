using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameplayUI : MonoBehaviour
{
    //Manages gameplayUI
    #region Fields
    [SerializeField] private Texture2D cursorImage;
    [SerializeField] private GameObject healthBarObject;
    [SerializeField] private GameObject manaBarObject;
    [SerializeField] private GameObject manaCostBarObject;
    [SerializeField] private GameObject castingUI;

    private PlayerState playerState;
    private PlayerStats playerStats;
    private PlayerCast playerCast;

    private Image healthBarImage;
    private Image manaBarImage;
    private Image manaCostBarImage;

    #endregion
    void Start()
    {
        playerState = GameObject.Find("Player").GetComponent<PlayerState>();
        playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
        playerCast = GameObject.Find("Player").GetComponent<PlayerCast>();

        healthBarImage = healthBarObject.GetComponent<Image>();
        manaBarImage = manaBarObject.GetComponent<Image>();
        manaCostBarImage = manaCostBarObject.GetComponent<Image>();
        Cursor.SetCursor(cursorImage, Vector2.zero, CursorMode.ForceSoftware);
    }
    void Update()
    {
        FillUIBars();
        ShowUI(playerState.IsCasting);
    }
    #region Methods
    /// <summary>
    /// Shows the casting UI depending on given bool.
    /// </summary>
    /// <param name="shouldShowUI"></param>
    private void ShowUI(bool shouldShowUI)
    {
        if (shouldShowUI)
        {
            Cursor.lockState = CursorLockMode.None;
            castingUI.SetActive(true);
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            castingUI.SetActive(false);
        }
    }
    /// <summary>
    /// Change how much UI bars are filled
    /// </summary>
    private void FillUIBars()
    {
        float healthPercentage = playerStats.CurrentHealth / playerStats.MaxHealth;
        float manaPercentage = playerStats.CurrentMana / playerStats.MaxMana;
        float manaCostPercentage = playerCast.manaCost / playerStats.MaxMana;

        healthBarImage.fillAmount = healthPercentage;
        manaBarImage.fillAmount = manaPercentage;
        manaCostBarImage.fillAmount = manaCostPercentage;     
    }
    #endregion
}
