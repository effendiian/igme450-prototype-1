using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gallery : MonoBehaviour
{
    private int currentIndex = 0;
    private List<GameObject> paintings = new List<GameObject>();
    private GameObject golfMeter;    //GameObject to hold the golf meter
    private List<Painting> paintingScripts = new List<Painting>();
    private List<Trait> allTraits;

    private List<Goal> goals;
    
    public GameObject curatorPrefab;
    public Influence influence;
    private GameObject curator;
    public Curator curatorScript;
    public GoalsUI goalsUI;

    public AudioSource hmmSource;
    public AudioSource ehhSource;

    public int numGoals = 4;

    public Text popularityText;
    public Button backButton;
    public Button nextButton;
    public Button upvoteButton;
    public Button downvoteButton;
    public GameObject goalScreen;

    private GameObject[] walls; //array to hold the walls
    private bool wallToMove = false;    //int to hold which of the two walls to move

    public GameObject store;

    public GameObject cupMngr;


    private float buttonDisabledTime = 0f;
    private const float BUTTON_DISABLE_LENGTH = 0.5f;


    private int money = 0;

    // Start is called before the first frame update
    void Start()
    {
        //setting the wall to the wall in the scene
        walls = new GameObject[] { GameObject.Find("Walls").transform.GetChild(0).gameObject, GameObject.Find("Walls").transform.GetChild(1).gameObject };
        golfMeter = GameObject.Find("golfbar");

        //TODO: Curator should be able to be constant throughout scenes
        curatorScript.EnsureArtist();

        //TODO: This should be somewhere else as well
        allTraits = curatorScript.GetAllTraits();
        foreach(Trait trait in allTraits)
        {
            trait.SetBaseRank(Random.Range(25, 60));
        }

        //This will have to happen every scene
        StartNight();
    }

    // Update is called once per frame
    void Update()
    {
        popularityText.text = "Popularity: " + paintingScripts[currentIndex].GetPopularity();

        int change = paintingScripts[currentIndex].GetChangeInPopularity();
        if (change > 0)
        {
            popularityText.text = popularityText.text + " <color=green>+" + change + "</color>";
        } else if (change < 0)
        {
            popularityText.text = popularityText.text + " <color=red>" + change + "</color>";
        }

        if (buttonDisabledTime > 0)
        {
            buttonDisabledTime -= Time.deltaTime;
            if (buttonDisabledTime <= 0)
            {
                upvoteButton.interactable = true;
                downvoteButton.interactable = true;
            }
        }
    }

    private void FixedUpdate()
    {

    }

    public void StartNight()
    {

        
        int cupCount = cupMngr.transform.childCount;

        //setting all the other cups to inactive
        for(int i = 0; i < cupCount; i++)
        {
            cupMngr.transform.GetChild(i).gameObject.SetActive(false);
        }


        if (store.GetComponent<StoreUI>().ItemsBought.Count > 0)
        {
            cupMngr.transform.Find(store.GetComponent<StoreUI>().ItemsBought[store.GetComponent<StoreUI>().ItemsBought.Count-1]).gameObject.SetActive(true);
        }

        store.SetActive(false);
        goalScreen.SetActive(true);

        foreach(GameObject painting in paintings)
        {
            Destroy(painting);
        }
        
        paintings = curatorScript.GeneratePaintings(Random.Range(10, 15));
        paintingScripts = new List<Painting>();
        foreach (GameObject painting in paintings)
        {
            paintingScripts.Add(painting.GetComponent(typeof(Painting)) as Painting);
        }

        influence.ResetInfluence();
        GenerateGoals();
        goalScreen.GetComponent<GoalsUI>().CreateText(goals, money);

        upvoteButton.interactable = true;
        downvoteButton.interactable = true;
        
        currentIndex = 1;
        PreviousPainting();
        CheckButtons();
    }

    public void EndNight()
    {
        foreach (var goal in goals)
        {
            if (goal.MetGoal(paintingScripts))
            {
                money += goal.GetBonus();
            }
        }

        goalsUI.EndNight(paintingScripts, money);
    }

    public List<Goal> GetGoals()
    {
        return goals;
    }

    private void GenerateGoals()
    {
        goals = new List<Goal>();

        //Only allow traits used in paintings for goals
        HashSet<Trait> usableTraits = new HashSet<Trait>();
        foreach (Painting painting in paintingScripts)
        {
            usableTraits.Add(painting.GetColorTrait());
            usableTraits.Add(painting.GetFormatTrait());
        }

        List<Trait> usable = new List<Trait>(usableTraits);
        for (int i = 0; i < numGoals; i++)
        {
            int traitIndex = Random.Range(0, usable.Count);
            Trait forGoal = usable[traitIndex];
            usable.RemoveAt(traitIndex);

            float num = Random.Range(0f, 1f);
            if (num < 0.33)
            {
                goals.Add(new LikeAllGoal(GetRandomBool(), forGoal, 100));
            } else if (num < 0.66)
            {
                goals.Add(new IncreasePopGoal(GetRandomBool(), forGoal, 150));
            } else
            {
                goals.Add(new ClickXTimesGoal(GetRandomBool(), forGoal, 120));
            }
        }
    }

    private bool GetRandomBool()
    {
        if (Random.Range(0f, 1f) <= 0.5)
            return true;
        else
            return false;
    }

    public void UpvoteCurrent()
    {
        if (buttonDisabledTime > 0)
            return;
        else
            DisableButtons();

        //float multipier = influence.ResetInfluence();
        float multiplier = 1f;
        paintingScripts[currentIndex].Upvote(multiplier, golfMeter);
        hmmSource.Play();

        influence.DecreaseInfluence();
        CheckInfluence();
    }

    public void DownvoteCurrent()
    {
        if (buttonDisabledTime > 0)
            return;
        else
            DisableButtons();

        //float multipier = influence.ResetInfluence();
        float multiplier = 1f;
        paintingScripts[currentIndex].Downvote(multiplier, golfMeter);
        ehhSource.Play();

        influence.DecreaseInfluence();
        CheckInfluence();
    }

    private void DisableButtons()
    {
        buttonDisabledTime = BUTTON_DISABLE_LENGTH;
        upvoteButton.interactable = false;
        downvoteButton.interactable = false;
    }

    private void CheckInfluence()
    {
        Debug.Log(influence.GetInfluence());
        if (influence.GetInfluence() <= 0)
        {
            upvoteButton.interactable = false;
            downvoteButton.interactable = false;

            Debug.Log("hello");
            EndNight();
        } 
    }

    public void NextPainting()
    {
        if (currentIndex < paintings.Count - 1)
        {
            currentIndex++;
            Vector3 newXPos = new Vector3(currentIndex * 19.2f, 1.0f, -9);
            Camera.main.GetComponent<CameraMovement>().MoveTo(newXPos);


            walls[ConvertToInt(wallToMove)].transform.position = new Vector3(newXPos.x, 1.0f);
            golfMeter.transform.position = new Vector3(newXPos.x, -3.5f);
            golfMeter.GetComponent<GolfMeter>().ChangePainting(paintings[currentIndex].GetComponent<Painting>().GetNumberOfClicks());
            wallToMove = !wallToMove;

            

            paintings[currentIndex].SetActive(true);
            CheckButtons();
        }
    }

    public void PreviousPainting()
    {
        if (currentIndex > 0)
        {
            currentIndex--;
            Vector3 newXPos = new Vector3(currentIndex * 19.2f, 1.0f, -9);
            Camera.main.GetComponent<CameraMovement>().MoveTo(newXPos);


            walls[ConvertToInt(wallToMove)].transform.position = new Vector3(newXPos.x, 1.0f);
            golfMeter.transform.position = new Vector3(newXPos.x, -3.5f);
            golfMeter.GetComponent<GolfMeter>().ChangePainting(paintings[currentIndex].GetComponent<Painting>().GetNumberOfClicks());
            wallToMove = !wallToMove;

            paintings[currentIndex].SetActive(true);
            CheckButtons();
        }
    }

    private void CheckButtons()
    {
        if (currentIndex == 0)
        {
            backButton.interactable = false;
        } else
        {
            backButton.interactable = true;
        }


        if (currentIndex == paintings.Count - 1)
        {
            nextButton.interactable = false;
        }
        else
        {
            nextButton.interactable = true;
        }
    }

    //helper method to convert the bool into an int for use in swapping the walls
    private int ConvertToInt(bool b) => b ? 1 : 0;

    public int Money
    {
        get { return money; }
        set { money = value; }
    }

}
