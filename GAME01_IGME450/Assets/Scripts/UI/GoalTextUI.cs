using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoalTextUI : MonoBehaviour
{
    public Text mainText;
    public Text bonusText;

    public void SetText(string main, string bonus)
    {
        mainText.text = main;
        bonusText.text = bonus;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
