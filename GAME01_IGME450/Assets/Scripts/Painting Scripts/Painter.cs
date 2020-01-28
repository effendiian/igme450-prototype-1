using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using System.Linq;

public class Painter : MonoBehaviour
{
    GameObject painting;
    public GameObject paintingPrefab;
    public Sprite squareSprite;
    
    public List<ColorTrait> colorTraits = new List<ColorTrait>();
    public List<FormatTrait> formatTraits = new List<FormatTrait>();

    Artist artist;

    public int minX = 2;
    public int maxX = 18;
    public int minY = 2;
    public int maxY = 7;
    
    List<int> probabilityChecks = new List<int>();

    // Start is called before the first frame update
    //Grab all the painters assigned to the Painting to create probabilities
    void Start()
    {
        artist = new Artist(paintingPrefab);

        InvokeRepeating("ShowNextPainting", 0, 3);
    }

    //Nothing to update right now
    // Update is called once per frame
    //void Update()
    //{
        
    //}

    //May not use in the future if we want to generate and hold a list of paintings, see methods below
    //Destroy the current painting and generate a new one to view
    public void ShowNextPainting()
    {
        Destroy(painting);
        painting = GeneratePainting();
        painting.SetActive(true);
    }

    //Generate and return a list of paintings - not active
    public IList<GameObject> GeneratePaintings(int num)
    {
        List<GameObject> paintings = new List<GameObject>();
        for (int i = 0; i < num; i++)
        {
            paintings.Add(GeneratePainting());
        }
        return paintings;
    }

    //Generate and return a single painting - not active
    private GameObject GeneratePainting()
    {
        return artist.GeneratePainting(colorTraits[Random.Range(0, colorTraits.Count)], formatTraits[Random.Range(0, formatTraits.Count)]);
    }
}
