using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlainStyle : MonoBehaviour, Style
{
    public Sprite squareSprite;

    public PlainStyle(Sprite squareSprite) {
        this.squareSprite = squareSprite;
    }

    public GameObject CreatePainting(float xScale, float yScale)
    {
        GameObject painting = new GameObject();
        painting.SetActive(false);

        SpriteRenderer rend = painting.AddComponent(typeof(SpriteRenderer)) as SpriteRenderer;
        rend.sprite = squareSprite;
        rend.color = Random.ColorHSV();
        painting.transform.position = painting.transform.position + new Vector3(0, 3, 0);
        painting.transform.localScale = new Vector3(xScale, yScale, 1);

        return painting;
    }
}
