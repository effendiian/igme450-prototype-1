using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using System.Linq;

public class Painter : MonoBehaviour
{
    GameObject painting;
    
    public List<GameObject> painters = new List<GameObject>();
    public List<int> probabilities = new List<int>();
    List<Style> styles = new List<Style>();

    public int minX = 2;
    public int maxX = 18;
    public int minY = 2;
    public int maxY = 7;
    
    List<int> probabilityChecks = new List<int>();

    // Start is called before the first frame update
    //Grab all the painters assigned to the Painting to create probabilities
    void Start()
    {
        foreach (var painter in this.painters)
        {
            styles.Add((Style)painter.GetComponent(typeof(Style)));
        }

        GenerateProbabilityChecks();

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
        float width = Random.Range(minX, maxX);
        float height = Random.Range(minY, maxY);

        int style = Random.Range(0, probabilityChecks[probabilityChecks.Count - 1]);
        for (int i = 0; i < probabilityChecks.Count; i++)
        {
            if (style <= probabilityChecks[i])
            {
                return styles[i].CreatePainting(width, height);
            }
        }

        return null;
    }

    //Change painters with all having same probabilities
    public void SetPainters(List<GameObject> painters)
    {
        SetPainters(painters, Enumerable.Repeat(1, painters.Count).ToList());
    }

    //Allows us to change Painters/Styles on the fly
    public void SetPainters(List<GameObject> painters, List<int> probabilities)
    {
        //Might not want to do this if we keep centralized painters
        foreach (var painter in this.painters)
        {
            Destroy(painter);
        }

        this.painters = painters;
        this.probabilities = probabilities;

        GenerateProbabilityChecks();
    }

    //Generate numbers to check for style so it's easier to do it every time we make them
    private void GenerateProbabilityChecks()
    {
        probabilityChecks.Add(probabilities[0]);
        if (probabilities.Count == 1)
            return;

        for (int i = 1; i < probabilities.Count; i++)
        {
            probabilityChecks.Add(probabilityChecks[i - 1] + probabilities[i]);
        }
    }
}
