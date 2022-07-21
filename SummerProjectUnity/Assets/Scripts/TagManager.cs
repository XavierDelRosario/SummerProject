using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TagManager
{   
    /// <summary>
    /// Returns the enemy tags 
    /// </summary>
    /// <param name="objectTag">The tag of the gameobject to find enemies for</param>
    /// <returns></returns>
    public static List<string> GetEnemyTags(string objectTag)
    {
        switch (objectTag)
        {
            case "Player":
                return  new List<string>() { "Enemy", "Passive", "Obstacle" };

            case "Enemy":
                return  new List<string>() { "Player", "Passive", "Friendly", "Obstacle" };

            default:
                return new List<string>() { "Player", "Passive", "Friendly", "Enemy", "Obstacle" };
        }
    }
    /// <summary>
    /// Checks if collided object is an enemy
    /// </summary>
    /// <param name="other"></param>
    /// <param name="listOfTags"></param>
    /// <returns></returns>
    public static bool CheckTag(Collider other, List<string> listOfTags)
    {
        for (int i = 0; i < listOfTags.Count; i++)
        {
            if (other.CompareTag(listOfTags[i]))
            {
                return true;
            }
        }
        return false;
    }
}
