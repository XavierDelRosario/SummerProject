using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameplayUI : MonoBehaviour
{
    //Manages UI during gameplay
    #region Fields
    [SerializeField] private Texture2D cursorImage;
    [SerializeField] private GameObject healthBarObject;
    [SerializeField] private GameObject manaBarObject;
    [SerializeField] private GameObject castingUI;

    private Image healthBarImage;
    private Image manaBarImage;
    private PlayerState playerState;
    private PlayerStats playerStats;
    #endregion
    void Start()
    {
        playerState = GameObject.Find("Player").GetComponent<PlayerState>();
        playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
        healthBarImage = healthBarObject.GetComponent<Image>();
        manaBarImage = manaBarObject.GetComponent<Image>();
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
        healthBarImage.fillAmount = healthPercentage;
        manaBarImage.fillAmount = manaPercentage;
    }
    #endregion
}
