using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolfMeter : MonoBehaviour
{
    public GameObject meterTicker;
    public Vector3 leftEdge;
    public Vector3 rightEdge;
    public float lerpPercent = 0.0f;
    public int count = 1;
    public bool goingRight = true;
    GameObject ticker;

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
        if (goingRight)
        {
            lerpPercent += 0.005f * count;

            if (lerpPercent >= 1.0f)
            {
                goingRight = false;
            }
        }
        else
        {
            lerpPercent -= 0.005f * count;

            if (lerpPercent <= 0.0f)
            {
                goingRight = true;
            }
        }

        ticker.transform.position = Vector3.Lerp(leftEdge, rightEdge, lerpPercent);

    }

    public int GetBonus()
    {
        count++;

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

        //Debug.Log(count);
        return multiplier;
    }
}
