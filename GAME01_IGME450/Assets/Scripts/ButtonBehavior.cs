using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonBehavior : MonoBehaviour
{

    public GameObject popularityCounterObject;  //UI object that counnts the painting popularity
    private Text popularityCounter;     //Reference to the text on the UI object for counting the painting popularity
    private int popularityScore;    //Int that holds the painting score
    private int timer;      //Timer for keeping track of how often the like button is being used
    private int frames = 60;    //Frame count used for incrmenting the timer
    private int multiplier;     //Int to hold a multiplier for increasing amount of points recieved as the player does better
    private bool tooManyLikes;   //Bool for holding whether or not the player has liked or disliked the painting too often  

    // Start is called before the first frame update
    void Start()
    {
        tooManyLikes = false;
        multiplier = 1;
        timer = 0;
        popularityCounter = popularityCounterObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        popularityCounter.text = "Popularity: " + popularityScore;
    }

    //Fised Update loop for runnning the timer
    private void FixedUpdate()
    {
        if (timer > 0)
        {
            if (frames <= 0)
            {
                timer--;
                frames = 60;
            }
            else
            {
                frames--;
            }
        }
        
    }


    //function that runs when a like happens
    public void Like()
    {

        if (multiplier > 6)
        {
            tooManyLikes = true;
        }

        if (tooManyLikes)
        {
            multiplier--;
            popularityScore += multiplier;
        }
        else
        {
            if (timer == 3)
            {
                multiplier = 0;
                popularityScore -= 2;
            }
            else if (timer == 2)
            {
                multiplier = 0;
                popularityScore -= 1;
            }
            else if (timer <= 0)
            {
                popularityScore += multiplier;
                multiplier++;
            }
            timer = 3;
        }
        
        if(popularityScore < 0)
        {
            popularityScore = 0;
        }
    }

    //function that runs when a dislike happens
    public void Dislike()
    {
        popularityScore--;
        if (popularityScore < 0)
        {
            popularityScore = 0;
        }
    }
}
