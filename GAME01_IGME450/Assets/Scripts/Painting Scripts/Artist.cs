using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Artist
{
    private Sprite squareSprite;

    public Artist(Sprite squareSprite)
    {
        this.squareSprite = squareSprite;
    }

    public GameObject GeneratePainting(ColorTrait color, FormatTrait format)
    {
        //Create and disable the painting
        GameObject painting = new GameObject();
        painting.transform.position = painting.transform.position + new Vector3(0, 3, 0);
        painting.SetActive(false);

        //Create the sprite renderer and assign it
        SpriteRenderer rend = painting.AddComponent(typeof(SpriteRenderer)) as SpriteRenderer;
        rend.sprite = squareSprite;

        
        rend.color = color.GetColor();
        
        Vector2 ratio = format.GetRatio();
        Vector3 size = new Vector3(ratio.x, ratio.y) * Random.Range(4, 7);

        painting.transform.localScale = size;

        //Now that the base sprite is set up, we can allow the traits to be handled
        //Doing lots of conversions after the fact, might just be better to pass in and if null not use?
        //foreach (Trait trait in traits)
        //{
        //    if (trait is ColorTrait colorTrait)
        //    {
        //        rend.color = colorTrait.GetColor();
        //    } else if (trait is FormatTrait formatTrait)
        //    {
        //        Vector2 ratio = formatTrait.GetRatio();
        //        Vector3 size = new Vector3(ratio.x, ratio.y) * Random.Range(4, 7);

        //        painting.transform.localScale = size;
        //    }
        //}

        return painting;
    }
}
