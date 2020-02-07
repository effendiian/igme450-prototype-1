using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickXTimesGoal : Goal
{
    private int requiredClickNum;

    public ClickXTimesGoal(bool isIncrease, Trait trait, int baseBonus) : base(trait, baseBonus)
    {

        this.requiredClickNum = Random.Range(4, 10);
        this.bonus *= Mathf.CeilToInt(requiredClickNum / 4);
    }

    public override string GetGoalText()
    {
        return "Succesfully change a " + trait.name + " painting's popularity " + requiredClickNum + " times.";
    }

    public override bool MetGoal(List<Painting> paintings)
    {
        foreach (Painting painting in paintings)
        {
            if (painting.HasTrait(trait) && painting.GetNumberOfSuccessfulClicks() >= requiredClickNum)
            {
                return true;
            }
        }

        return false;
    }
}
