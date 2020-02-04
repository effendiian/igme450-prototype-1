using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LikeAllGoal : Goal
{
    private bool isLike;

    public LikeAllGoal(bool isLike, Trait trait, int bonus) : base(trait, bonus)
    {
        this.isLike = isLike;
    }

    public override string GetGoalText()
    {
        string text = "Like";
        if (!isLike)
            text = "Dislike";

        text += " all paintings that are " + trait.name;
        return text;
    }

    //Check if all paintings with the trait have had some increase/decrease depending on goal
    public override bool MetGoal(List<Painting> paintings)
    {
        foreach(Painting painting in paintings)
        {
            if (painting.HasTrait(trait))
            {
                if ((isLike && painting.GetChangeInPopularity() <= 0) || (!isLike && painting.GetChangeInPopularity() >= 0))
                {
                    return false;
                }
            }
        }

        return true;
    }
}
