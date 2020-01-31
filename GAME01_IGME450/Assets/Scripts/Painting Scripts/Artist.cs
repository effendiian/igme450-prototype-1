using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Artist
{
    private GameObject paintingPrefab;

    public Artist(GameObject paintingPrefab)
    {
        this.paintingPrefab = paintingPrefab;
    }

    public GameObject GeneratePainting(ColorTrait color, FormatTrait format)
    {
        //Create and disable the painting
        GameObject painting = Object.Instantiate(paintingPrefab);
        painting.transform.position = painting.transform.position + new Vector3(0, 1, 0);

        Painting script = painting.GetComponent(typeof(Painting)) as Painting;
        script.SetTraits(color, format);

        //Get the sprite renerer so we can use it
        SpriteRenderer rend = painting.GetComponent(typeof(SpriteRenderer)) as SpriteRenderer;

        //Add some randomization to the range of shades, but always be fully transparent
        Color randomizedColor = color.GetColor() * Random.Range(0.39f, 1);
        randomizedColor.a = 1;
        rend.color = randomizedColor;


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
