using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Painting : MonoBehaviour
{
    private ColorTrait color;
    private FormatTrait format;


    private int timer = 0;      //Timer for keeping track of how often the like button is being used
    private int frames = 60;    //Frame count used for incrmenting the timer
    private int successfulClicks;
    private int badClicks;
    private bool tooManyLikes;   //Bool for holding whether or not the player has liked or disliked the painting too often

    public static int SECONDS_BETWEEN_CLICKS = 2;

    //Keep track of initial and end popularity over the night
    private int initialPopularity;
    private int popularity;

    // Start is called before the first frame update
    //void Start()
    //{
        
    //}

    private void FixedUpdate()
    {
        if (frames <= 0)
        {
            timer++;
            Debug.Log(timer);
            frames = 60;
        }
        else
        {
            frames--;
        }
    }

    public void Upvote(float multiplier)
    {
        HandleClick(multiplier, 1);

        if (popularity > 100)
            popularity = 100;
    }

    public void Downvote(float multiplier)
    {
        HandleClick(multiplier, -1);

        if (popularity < 0)
            popularity = 0;
    }

    private void HandleClick(float multiplier, int sign)
    {
        if (SuccessfulClick())
        {
            int change = Mathf.FloorToInt(sign * multiplier * GetSuccessfullClickFactor());
            if (change != 0)
            {
                successfulClicks++;
                popularity += Mathf.FloorToInt(sign * multiplier * GetSuccessfullClickFactor());
            }
        }
        else
        {
            DoBadClick(-sign);
        }
    }

    private float GetSuccessfullClickFactor()
    {
        if (successfulClicks < 2)
        {
            return 1;
        }
        else if (successfulClicks < 5)
        {
            return 0.8f - (0.2f * (successfulClicks - 2));
        }
        else if (successfulClicks < 12)
        {
            return 0.4f;
        }
        else if (successfulClicks < 18) //Was 7 and 0.1, jumped straight from previous
        {
            return 0.2f;
        }
        else
        {
            return 0.025f;
        }
    }

    private void DoBadClick(int sign)
    {
        badClicks++;

        //If this is the first bad click or there has been no net change in popularity don't do anything
        if (badClicks == 1 || GetChangeInPopularity() <= 0)
        {
            return;
        }
        
        if (badClicks <= 3)
        {
            popularity += 1 * sign;
        }
        else if (badClicks <= 5)
        {
            popularity += 3 * sign;
        }
        else if (badClicks <= 8)
        {
            popularity += 6 * sign;
        }
    }

    private bool SuccessfulClick()
    {
        if (successfulClicks == 0)
        {
            return true;
        }
        else
        {
            if (timer > SECONDS_BETWEEN_CLICKS)
            {
                timer = 0;
                return true;
            } else
            {
                return false;
            }
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

        int randomFactor = Random.Range(0, (totalTraitCapacity - totalTraitPopularity) / 2);
        float percentage = (float)(totalTraitPopularity + randomFactor) / totalTraitCapacity;

        return (int)(percentage * 100);
    }
}
