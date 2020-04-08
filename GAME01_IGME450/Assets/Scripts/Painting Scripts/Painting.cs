using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Painting : MonoBehaviour
{
    private ColorTrait color;
    private FormatTrait format;

    private int BASE_CLICK_INCREASE = 2;
    private int clicks;
    private int successfulClicks;

    //Keep track of initial and end popularity over the night
    private int initialPopularity;
    private int popularity;


    // Start is called before the first frame update
    //void Start()
    //{

    //}

    public void Upvote(float multiplier, GameObject golfbar)
    {
        clicks += 1;
        int golfMulti = golfbar.GetComponent<GolfMeter>().GetBonus(clicks);
        HandleClick(multiplier, 1, golfMulti);

        if (popularity > 100)
            popularity = 100;
    }

    public void Downvote(float multiplier, GameObject golfbar)
    {
        clicks += 1;
        int golfMulti = golfbar.GetComponent<GolfMeter>().GetBonus(clicks);
        HandleClick(multiplier, -1, golfMulti);

        if (popularity < 0)
            popularity = 0;
    }

    private void HandleClick(float multiplier, int sign, int _golfMulti)
    {

        if (multiplier > 0)
        {
            successfulClicks += 1;
            popularity += (int)(multiplier * BASE_CLICK_INCREASE * sign * _golfMulti);
        }
        else
        {
            //Click which has a negative impact, don't go lower than the start
            if ((sign > 0 && GetChangeInPopularity() >= 0) || GetChangeInPopularity() <= 0)
            {
                popularity -= (int)(multiplier * BASE_CLICK_INCREASE * sign * _golfMulti);
            }
        }
    }

    public int GetNumberOfSuccessfulClicks()
    {
        return successfulClicks;
    }

    public int GetNumberOfClicks()
    {
        return clicks;
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

    public void SetTraits(ColorTrait color, FormatTrait format)
    {
        this.color = color;
        this.format = format;

        initialPopularity = CaclulatePopularity();
        popularity = initialPopularity;

    }

    public bool HasTrait(Trait trait)
    {
        return color.Equals(trait) || format.Equals(trait);
    }

    public ColorTrait GetColorTrait()
    {
        return color;
    }

    public FormatTrait GetFormatTrait()
    {
        return format;
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
