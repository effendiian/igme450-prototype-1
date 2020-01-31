using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using System.Linq;

/**
 * Currently the curator determines what kind of paintings to make randomly
 * Then sends those to the Artist to be made
 * 
 * Might be better to move this somewhere else
 */
public class Curator : MonoBehaviour
{
    public GameObject painting;
    public GameObject paintingPrefab;
    public Sprite squareSprite;
    public GameObject museumWall;

    public List<ColorTrait> colorTraits = new List<ColorTrait>();
    public List<FormatTrait> formatTraits = new List<FormatTrait>();

    Artist artist;

    public int minX = 2;
    public int maxX = 18;
    public int minY = 2;
    public int maxY = 7;

    public float xOffset = 19.2f;
    
    
    List<int> probabilityChecks = new List<int>();

    // Start is called before the first frame update
    //Grab all the painters assigned to the Painting to create probabilities
    void Start()
    {
        artist = new Artist(paintingPrefab);

        //InvokeRepeating("ShowNextPainting", 0, 3);
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
        painting = GeneratePainting(0);
        painting.SetActive(true);
    }

    //Generate and return a list of paintings - not active
    public List<GameObject> GeneratePaintings(int num)
    {
        

        List<GameObject> paintings = new List<GameObject>();
        for (int i = 0; i < num; i++)
        {
            GameObject wallSpace = Object.Instantiate(museumWall);
            Vector3 newXPos = new Vector3(i * xOffset, 1.0f, 0);
            wallSpace.transform.position = newXPos; 
            paintings.Add(GeneratePainting(i * xOffset));
        }
        return paintings;
    }

    //Generate and return a single painting - not active
    private GameObject GeneratePainting(float _xPos)
    {
        return artist.GeneratePainting(colorTraits[Random.Range(0, colorTraits.Count)], formatTraits[Random.Range(0, formatTraits.Count)], _xPos);
    }

    //Temporary method just to get things working
    public void EnsureArtist()
    {
        if (artist == null)
            artist = new Artist(paintingPrefab);
    }
}
