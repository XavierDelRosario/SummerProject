using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class SymbolBehaviour : MonoBehaviour
{
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
        image = this.GetComponent<RawImage>();
    }
    private void OnEnable()
    {
        isSelected = false;
    }
    private void Update()
    {
        if (isSelected)
        {
            image.color = highlightBlue;
        }
        else
        {
            image.color = defaultBlue;
        }
    }
    /*
     Sends the symbols value to the current spellID
     */
    public void SendValue()
    {
        if (!isSelected)
        {
            playerCast.SpellID += symbolID;
            isSelected = true;
        } 
    }
}
