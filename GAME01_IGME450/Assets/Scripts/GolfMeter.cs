using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GolfMeter : MonoBehaviour
{
    public GameObject meterTicker;
    public Vector3 leftEdge;
    public Vector3 rightEdge;
    float lerpPercent = 0.0f;
    float timeToActive = 0;
    float timeForMultiplier = 0;
    public int count = 1;
    public bool goingRight = true;
    public GameObject ticker;

    public Text multiplierText;

    private const float FREEZE_TIME = 0.25f;
    private const float MULTIPLIER_TIME = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        leftEdge = new Vector3(transform.position.x - 2.5f, transform.position.y, transform.position.z);
        rightEdge = new Vector3(transform.position.x + 2.5f, transform.position.y, transform.position.z);

        ticker = Object.Instantiate(meterTicker);
        ticker.transform.position = leftEdge;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeForMultiplier > 0)
        {
            timeForMultiplier -= Time.deltaTime;

            if (timeForMultiplier <= 0)
            {
                multiplierText.text = "";
            }
        }

        if (timeToActive > 0)
        {
            timeToActive -= Time.deltaTime;
            return;
        } 

        if (goingRight)
        {
            lerpPercent += 0.005f + (0.002f * count);

            if (lerpPercent >= 1.0f)
            {
                goingRight = false;
            }
        }
        else
        {
            lerpPercent -= 0.005f + (0.002f * count);

            if (lerpPercent <= 0.0f)
            {
                goingRight = true;
            }
        }

        ticker.transform.position = Vector3.Lerp(leftEdge, rightEdge, lerpPercent);

    }

    public int GetBonus(int count)
    {
        this.count = count+1;
        if (count >= 6) { count = 6; }

        int multiplier = 1;

        if (lerpPercent >= 0.45f && lerpPercent <= 0.55f)
        {
            multiplier = 2;
        }
        else if (lerpPercent >= 0.48f && lerpPercent <= 0.52f)
        {
            multiplier = 4;
        }

        HitEffects(multiplier);

        //Debug.Log(count);
        return multiplier;
    }

    public void HitEffects(int multiplier)
    {
        timeToActive = FREEZE_TIME;
        timeForMultiplier = MULTIPLIER_TIME;

        multiplierText.text = "x" + multiplier;
    }

    //function to handle the count change when the painting is changed
    public void ChangePainting(int count)
    {
        leftEdge = new Vector3(transform.position.x - 2.5f, transform.position.y, transform.position.z);
        rightEdge = new Vector3(transform.position.x + 2.5f, transform.position.y, transform.position.z);
        ticker.transform.position = leftEdge;
        this.count = count+1;
    }
}
