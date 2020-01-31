using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gallery : MonoBehaviour
{
    private int currentIndex = 0;
    private List<GameObject> paintings;
    private List<Painting> paintingScripts = new List<Painting>();
    public List<Trait> allTraits;
    
    public GameObject curatorPrefab;
    public Influence influence;
    private GameObject curator;
    public Curator curatorScript;

    public Text popularityText;

    // Start is called before the first frame update
    void Start()
    {
        //TODO: Curator should be able to be constant throughout scenes
        curatorScript.EnsureArtist();

        //TODO: This should be somewhere else as well
        foreach(Trait trait in allTraits)
        {
            trait.SetBaseRank(Random.Range(25, 60));
        }

        //This will have to happen every scene
        paintings = curatorScript.GeneratePaintings(Random.Range(7, 10));
        foreach (GameObject painting in paintings)
        {
            paintingScripts.Add(painting.GetComponent(typeof(Painting)) as Painting);
        }
    
        paintings[currentIndex].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        popularityText.text = "Popularity: " + paintingScripts[currentIndex].GetPopularity();
    }

    public void UpvoteCurrent()
    {
        float multipier = influence.ResetInfluence();
        paintingScripts[currentIndex].Upvote(multipier);
    }

    public void DownvoteCurrent()
    {
        float multipier = influence.ResetInfluence();
        paintingScripts[currentIndex].Downvote(multipier);
    }

    public void NextPainting()
    {
        if (currentIndex < paintings.Count - 1)
        {
            paintings[currentIndex++].SetActive(false);
            paintings[currentIndex].SetActive(true);
        }
    }

    public void PreviousPainting()
    {
        if (currentIndex > 0)
        {
            paintings[currentIndex--].SetActive(false);
            paintings[currentIndex].SetActive(true);
        }
    }



}
