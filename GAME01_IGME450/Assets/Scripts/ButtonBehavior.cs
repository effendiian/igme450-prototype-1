using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonBehavior : MonoBehaviour
{

    public GameObject popularityCounterObject;  //UI object that counnts the painting popularity
    private Text popularityCounter;     //Reference to the text on the UI object for counting the painting popularity
    private int popularityScore;

    // Start is called before the first frame update
    void Start()
    {
        popularityCounter = popularityCounterObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        popularityCounter.text = "Popularity: " + popularityScore;
    }


    //function that runs when a like happens
    public void Like()
    {
        popularityScore++;
    }

    //function that runs when a dislike happens
    public void Dislike()
    {
        if (popularityScore > 0)
        {
            popularityScore--;
        }
    }
}
