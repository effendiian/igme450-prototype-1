using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoalsUI : MonoBehaviour
{
    public Gallery gallery;
    public GameObject textPrefab;

    public GameObject hideButton;
    public GameObject nextNightButton;
    public Text moneyText;

    private List<Goal> goals;
    private List<GameObject> goalTexts = new List<GameObject>();
    private List<GoalTextUI> goalTextScripts = new List<GoalTextUI>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            if (this.gameObject.transform.localScale.x == 0)
                Show();
            else
                Hide();
        }
    }

    public void Show()
    {
        this.gameObject.transform.localScale = new Vector3(1,1,1);
    }

    public void Hide()
    {
        this.gameObject.transform.localScale = new Vector3(0, 0, 0);
    }

    public void CreateText(List<Goal> goals, int money)
    {
        moneyText.text = "Bank: $" + money;

        this.goals = goals;

        foreach (var goalText in goalTexts)
        {
            Destroy(goalText);
        }

        goalTexts = new List<GameObject>();
        goalTextScripts = new List<GoalTextUI>();

        foreach (var goal in goals)
        {
            GameObject text = Instantiate(textPrefab);
            text.transform.parent = this.transform;
            text.transform.position = new Vector3(0, 0);
            GoalTextUI goalText = text.GetComponent<GoalTextUI>();
            goalText.SetText(goal.GetGoalText(), "Bonus: $" + goal.GetBonus());

            goalTexts.Add(text);
            goalTextScripts.Add(goalText);
        }

        hideButton.SetActive(true);
        nextNightButton.SetActive(false);
    }

    public void EndNight(List<Painting> paintings, int money)
    {
        hideButton.SetActive(false);
        nextNightButton.SetActive(true);

        moneyText.text = "Bank: $" + money;

        for (int i = 0; i < goals.Count; i++)
        {
            if (goals[i].MetGoal(paintings))
            {
                goalTextScripts[i].SetText("<color=green>" + goalTextScripts[i].mainText.text + "</color>", "<color=green>" + goalTextScripts[i].bonusText.text + "</color>");
            } else
            {
                goalTextScripts[i].SetText("<color=red>" + goalTextScripts[i].mainText.text + "</color>", "<color=red>" + goalTextScripts[i].bonusText.text + "</color>");
            }
        }

        Show();
    }
}
