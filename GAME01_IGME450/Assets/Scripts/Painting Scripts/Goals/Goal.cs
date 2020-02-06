using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Goal
{
    protected Trait trait;
    protected int bonus;

    public Goal(Trait trait, int bonus)
    {
        this.trait = trait;
        this.bonus = bonus;
    }

    public abstract bool MetGoal(List<Painting> paintings);

    public abstract string GetGoalText();

    public Trait GetTrait()
    {
        return trait;
    }

    public int GetBonus()
    {
        return bonus;
    }

}
