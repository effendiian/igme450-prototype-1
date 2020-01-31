using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Trait : ScriptableObject
{
    //Rank is essentially the popularity of each Trait
    //Values should range from 0-100
    public int rank = 0;

    public string name;
    private bool isTrending = false;

    /**
     * Updates the rank of a trait based on the change in popularity for a corresponding painting
     * 
     * This is a temporary way for now. In the future I think something else should calculate the change
     * and then send it to the Trait. Which can decide diminishing returns?
     */
    public void UpdateFromPaintingChange(int changeInPopularity)
    {
        float rankChange = changeInPopularity * 0.25f;
    }

    //Method to be called at the start of the game to set the initial rank
    public void SetBaseRank(int rank)
    {
        this.rank = rank;
    }

    public int GetRank()
    {
        return rank;
    }
}
