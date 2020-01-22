using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class Painter : MonoBehaviour
{
    public Sprite square;
    GameObject painting;

    List<Style> styles = new List<Style>();
    List<int> probabilities = new List<int>();
    List<int> probabilityChecks = new List<int>();

    // Start is called before the first frame update
    void Start()
    {
        styles.Add(new PlainStyle(square));
        probabilities.Add(1);

        GenerateProbabilityChecks();

        InvokeRepeating("GenerateNextPainting", 0, 3);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GenerateNextPainting()
    {
        Destroy(painting);
        float width = Random.Range(2, 18);
        float height = Random.Range(2, 8);

        int style = Random.Range(0, probabilityChecks[probabilityChecks.Count - 1]);
        for (int i = 0; i < probabilityChecks.Count; i++)
        {
            if (style <= probabilityChecks[i])
            {
                Debug.Log("drawing");
                painting = styles[i].CreatePainting(width, height);
                painting.SetActive(true);
            }
        }
    }

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
