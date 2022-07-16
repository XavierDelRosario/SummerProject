using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Symbol
{
    public string symbolID;

    private GameObject player;
    private PlayerCast playerCast;

    public Symbol(string symbolID) 
    {

        player = GameObject.Find("Player");
        playerCast = player.GetComponent<PlayerCast>();


        this.symbolID = symbolID;
    }

    /*
     Attaches the symbolID to the existing spellID. Changes value in PlayerCast
     */
    public void SendValue()
    {
       // playerCast.spellID += symbolID;
    }
}
