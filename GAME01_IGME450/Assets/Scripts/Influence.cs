using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Influence : MonoBehaviour
{

    //varaibles
    private float multiplier; //float to hold the current multiplier value
    public float reputationScoreMultiplier;    //float to decide how fast the bar will fill and multiplier will increase


    // Start is called before the first frame update
    void Start()
    {
        reputationScoreMultiplier = .02f;
        multiplier = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //fixed update to run the timer for the influence bar and the mulitplier
    private void FixedUpdate()
    {
        multiplier += .1f;

        if (this.transform.localScale.x < 18f)
        {
            this.transform.localScale = new Vector3(this.transform.localScale.x + reputationScoreMultiplier, this.transform.localScale.y);
        }
    }

    //function to return the current multiplier that will effect popularity
    public float Mulitplier
    {
        get { return multiplier; }
    }

    //function to reset the influence when it is used
    public void ResetInfluence()
    {
        this.transform.localScale = new Vector3(.03f, this.transform.localScale.y);
    }
}
