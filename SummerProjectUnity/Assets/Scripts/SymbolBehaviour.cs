using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class SymbolBehaviour : MonoBehaviour
{
    //Manages the UI of symbol in Casting UI. 
    #region Fields
    [SerializeField] private string symbolID;
    [SerializeField] private Color defaultBlue = new Color(0f, 0f, 1f, 1f);
    [SerializeField] private Color highlightBlue = new Color(0f, 0.76f, 0.87f, 1f);

    private PlayerCast playerCast;
    private RawImage image;
    private bool isSelected = false;
    #endregion
    void Start()
    {
        playerCast = GameObject.Find("Player").GetComponent<PlayerCast>();
        image = GetComponent<RawImage>();
    }
    private void OnEnable()
    {
        isSelected = false;
    }
    private void Update()
    {
        highlight(image, isSelected);
    }
    /// <summary>
    /// Sends the symbol value to PlayerCast spellID
    /// </summary>
    private void SendValue()
    {
        if (!isSelected)
        {
            playerCast.spellID += symbolID;
            isSelected = true;
        } 
    }
    /// <summary>
    ///  Highlights an image based on given bool
    /// </summary>
    /// <param name="image"></param>
    /// <param name="shouldHighlight"></param>
    private void highlight(RawImage image, bool shouldHighlight)
    {
        if (shouldHighlight)
        {
            image.color = highlightBlue;
        }
        else
        {
            image.color = defaultBlue;
        }
    }
}
