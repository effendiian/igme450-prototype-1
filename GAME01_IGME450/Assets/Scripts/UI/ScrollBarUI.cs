using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollBarUI : MonoBehaviour
{
    public GameObject scrollScreen;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void scrollFunction(float value)
    {
        scrollScreen.transform.position = new Vector3(scrollScreen.transform.position.x, ((Screen.height)/6.7f) + (750 * value), 0);
    }
}
