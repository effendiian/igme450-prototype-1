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
    public GameObject paintingPrefab;
    public Sprite squareSprite;
    
    public GameObject painting;
    public GameObject museumWall;
    public GameObject golfmeter;
    
    public List<ColorTrait> colorTraits = new List<ColorTrait>();
    public List<FormatTrait> formatTraits = new List<FormatTrait>();

    public List<GameObject> golfMeters = new List<GameObject>();

    Artist artist;

    public int minX = 2;
    public int maxX = 18;
    public int minY = 2;
    public int maxY = 7;

    public float xOffset = 19.2f;

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

    //Generate and return a list of paintings - not active
    public List<GameObject> GeneratePaintings(int num)
    {
        List<GameObject> paintings = new List<GameObject>();
        for (int i = 0; i < num; i++)
        {
            GameObject wallSpace = Object.Instantiate(museumWall);
            GameObject golfbar = Object.Instantiate(golfmeter);
            golfMeters.Add(golfbar);
            Vector3 newXPos = new Vector3(i * xOffset, 1.0f, 0);
            wallSpace.transform.position = newXPos;
            newXPos = new Vector3(i * xOffset, -3.53f, 0);
            golfbar.transform.position = newXPos;
            paintings.Add(GeneratePainting(i * xOffset, golfbar));
        }
        return paintings;
    }

    //Generate and return a single painting - not active
    private GameObject GeneratePainting(float _xPos, GameObject _golfbar)
    {
        return artist.GeneratePainting(colorTraits[Random.Range(0, colorTraits.Count)], formatTraits[Random.Range(0, formatTraits.Count)], _xPos, _golfbar);
    }

    public List<Trait> GetAllTraits()
    {
        List<Trait> traits = new List<Trait>();
        traits.AddRange(colorTraits);
        traits.AddRange(formatTraits);
        return traits;
    }

    //Temporary method just to get things working
    public void EnsureArtist()
    {
        if (artist == null)
            artist = new Artist(paintingPrefab);
    }
}
