using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "FormatTraitSO", order = 2)]
public class FormatTrait : Trait
{
    public Vector2 ratio;

    //Will be repetitive, could just do at start?
    public Vector2 GetRatio()
    {
        return ratio / Mathf.Max(ratio.x, ratio.y);
    }

}
