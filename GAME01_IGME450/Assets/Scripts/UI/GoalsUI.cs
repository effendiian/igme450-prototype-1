using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoalsUI : MonoBehaviour
{
    public Gallery gallery;
    public GameObject textPrefab;

    public List<GameObject> goalTexts;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Show()
    {
        this.gameObject.SetActive(true);
    }

    public void Hide()
    {
        this.gameObject.SetActive(false);
    }

    public void CreateText(List<Goal> goals)
    {
        foreach (var text in goalTexts)
        {
            Destroy(text);
        }

        goalTexts = new List<GameObject>();

        foreach (var goal in goals)
        {
            GameObject text = Instantiate(textPrefab);
            text.transform.parent = this.transform;
            text.transform.position = new Vector3(0, 0);
            text.GetComponent<Text>().text = goal.GetGoalText();
            goalTexts.Add(text);
        }
    }
}
