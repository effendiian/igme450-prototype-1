using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Trait : ScriptableObject
{
    //Rank is essentially the popularity of each Trait
    //Values should range from 0-100
    public int rank = 0;

    public string name;

    public abstract void Upvote();
    public abstract void Downvote();
}
