using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Painting : MonoBehaviour
{
    private ColorTrait color;
    private FormatTrait format;


    private int timer;      //Timer for keeping track of how often the like button is being used
    private int frames = 60;    //Frame count used for incrmenting the timer
    private int multiplier;     //Int to hold a multiplier for increasing amount of points recieved as the player does better
    private bool tooManyLikes;   //Bool for holding whether or not the player has liked or disliked the painting too often

    //Keep track of initial and end popularity over the night
    private int initialPopularity;
    private int popularity;

    // Start is called before the first frame update
    //void Start()
    //{
        
    //}

    private void FixedUpdate()
    {
        if (timer > 0)
        {
            if (frames <= 0)
            {
                timer--;
                frames = 60;
            }
            else
            {
                frames--;
            }
        }
    }

    public void Upvote()
    {
        //Move logic for upvoting here
        //Switch this to something like diminishing returns?
        if (multiplier > 6)
        {
            tooManyLikes = true;
        }

        if (tooManyLikes)
        {
            multiplier--;
            popularity += multiplier;
        }
        else
        {
            if (timer == 3)
            {
                multiplier = 0;
                popularity -= 2;
            }
            else if (timer == 2)
            {
                multiplier = 0;
                popularity -= 1;
            }
            else if (timer <= 0)
            {
                popularity += multiplier;
                multiplier++;
            }
            timer = 3;
        }

        if (popularity < 0)
        {
            popularity = 0;
        }
    }

    public void Downvote()
    {
        popularity--;
        if (popularity < 0)
        {
            popularity = 0;
        }
    }

    //Accessible point for getting the popularity to display on the screen
    public int GetPopularity()
    {
        return popularity;
    }

    public int GetChangeInPopularity()
    {
        return popularity - initialPopularity;
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

        initialPopularity = CaclulatePopularity();
        popularity = initialPopularity;
    }

    private int CaclulatePopularity()
    {
        int totalTraitPopularity = color.GetRank() + format.GetRank();
        int totalTraitCapacity = 100 + 100; //Color + Format

        int randomFactor = Random.Range(0, totalTraitCapacity - totalTraitPopularity);
        float percentage = (float)(totalTraitPopularity + randomFactor) / totalTraitCapacity;

        return (int)(percentage * 100);
    }
}
