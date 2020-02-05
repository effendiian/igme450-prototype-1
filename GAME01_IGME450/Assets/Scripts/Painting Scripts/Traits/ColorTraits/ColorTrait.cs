using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ColorTraitSO", order = 1)]
public class ColorTrait : Trait
{
    public Color color;

    public ColorTrait(string name, Color color)
    {
        this.name = name;
        this.color = color;
    }

    public string GetName()
    {
        return name;
    }

    public Color GetColor()
    {
        return color;
    }

}
