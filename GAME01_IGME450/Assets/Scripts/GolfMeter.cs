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

    public float GetBonus(int count)
    {
        this.count = count+1;
        if (count >= 6) { count = 6; }

        float multiplier = 1f;

        if (lerpPercent >= 0.4669f && lerpPercent <= 0.5330f)
        {
            multiplier = 2.5f;
        }
        else if (lerpPercent >= 0.2913f && lerpPercent <= 0.7086f)
        {
            multiplier = 1f;
        }
        else if (lerpPercent >= 0.09504 && lerpPercent <= 0.90495f)
        {
            multiplier = 0.5f;
        } else
        {
            multiplier = -1f;
        }

        HitEffects(multiplier);

        //Debug.Log(count);
        return multiplier;
    }

    public void HitEffects(float multiplier)
    {
        timeToActive = FREEZE_TIME;
        timeForMultiplier = MULTIPLIER_TIME;

        if (multiplier > 0)
        {
            multiplierText.text = "x" + multiplier;
            multiplierText.color = Color.black;
        } else
        {
            multiplierText.text = "x" + (-1 * multiplier);
            multiplierText.color = Color.red;
        }
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
