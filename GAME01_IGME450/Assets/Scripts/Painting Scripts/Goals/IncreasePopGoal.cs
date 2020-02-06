using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreasePopGoal : Goal
{
    private bool isIncrease;
    private int requiredChange;

    public IncreasePopGoal(bool isIncrease, Trait trait, int baseBonus) : base(trait, baseBonus)
    {
        this.isIncrease = isIncrease;

        this.requiredChange = Random.Range(10, 30);
        this.bonus *= Mathf.CeilToInt(requiredChange / 10);
    }

    public override string GetGoalText()
    {
        string text = "Increase";
        if (!isIncrease)
            text = "Decrease";

        text += " a " + trait.name + " painting's popularity by " + requiredChange;
        return text;
    }

    public override bool MetGoal(List<Painting> paintings)
    {
        foreach (Painting painting in paintings)
        {
            if (painting.HasTrait(trait))
            {
                if ((isIncrease && painting.GetChangeInPopularity() >= requiredChange) || (!isIncrease && painting.GetChangeInPopularity() <= requiredChange))
                {
                    return true;
                }
            }
        }

        return false;
    }
}
