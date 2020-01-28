using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Painting : MonoBehaviour
{
    private ColorTrait color;
    private FormatTrait format;

    private int popularity;

    public void Upvote()
    {

    }

    public void Downvote()
    {

    }

    public int GetPopularity()
    {
        return popularity;
    }

    // Start is called before the first frame update
    //void Start()
    //{
        
    //}

    //// Update is called once per frame
    //void Update()
    //{
        
    //}

    public void SetTraits(ColorTrait color, FormatTrait format)
    {
        this.color = color;
        this.format = format;
    }
}
