using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreUI : MonoBehaviour
{
    public Gallery galleryScript;   //getting the gallery script to be used later
    private List<string> itemsBought = new List<string>();   //List to hold the names of the items bought
    public GameObject[] costs;  //array with the ui objects that have the cost text in it
    public GameObject[] buttons;    //array with the ui buttons

    public GameObject goals;    //goal screen game object

    public Text money;  //field that holds the money

    private GameObject equipped;    //holds currently equipped cup

    public Scrollbar scrollBar; //holds the scrollbar for the store
    public GameObject scrollScreen; //holds the canvas that scrolls with the scrollbar

    public AudioSource buySound;
    public AudioSource noSound;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //function to buy the item
    public void BuyItem(GameObject button)
    {
        int index = -1;

        for(int i = 0;i < buttons.Length; i++)
        {
            if(button == buttons[i])
            {
                index = i;
            }
        }

        //if the item is already bought, removing it and putting it at the end of the list so it becomes equipped
        if(itemsBought.Contains(costs[index].name))
        {
            itemsBought.Remove(costs[index].name);
            itemsBought.Add(costs[index].name);
            buttons[index].GetComponent<Button>().interactable = false;
            buttons[index].transform.GetChild(0).GetComponent<Text>().text = "EQUIPPED";
            equipped.GetComponent<Button>().interactable = true;
            equipped.transform.GetChild(0).GetComponent<Text>().text = "EQUIP";
            equipped = buttons[index];
            return;
        }

        //removing the dollar sign from the string
        string cost = costs[index].GetComponent<Text>().text.Remove(0, 1);

        //making sure that the player has enough money
        if (CheckFunds(int.Parse(cost)))
        {
            itemsBought.Add(costs[index].name);
            buttons[index].GetComponent<Button>().interactable = false;
            buttons[index].transform.GetChild(0).GetComponent<Text>().text = "EQUIPPED";
            if (equipped != null)
            {
                equipped.GetComponent<Button>().interactable = true;
                equipped.transform.GetChild(0).GetComponent<Text>().text = "EQUIP";
            }
            equipped = buttons[index];
            galleryScript.Money = galleryScript.Money - int.Parse(cost);
            money.text = "Bank: $" + galleryScript.Money;
        }
    }

    //function to open the store page
    public void OpenStore()
    {
        goals.SetActive(false);

        scrollBar.value = 0;
        scrollScreen.transform.position = new Vector3(scrollScreen.transform.position.x, ((Screen.height) / 6.7f), 0);

        money.text = "Bank: $" + galleryScript.Money;

        for(int i = 0; i < costs.Length; i++)
        {
            if(itemsBought.Contains(costs[i].name))
            {
                buttons[i].GetComponent<Button>().interactable = true;
                buttons[i].transform.GetChild(0).GetComponent<Text>().text = "EQUIP";
            }
        }
        if (equipped != null)
        {
            equipped.GetComponent<Button>().interactable = false;
            equipped.transform.GetChild(0).GetComponent<Text>().text = "EQUIPPED";
        }


        this.gameObject.SetActive(true);
    }

    //function to check if the player has enough money
    private bool CheckFunds(int cost)
    {
        if(galleryScript.Money < cost)
        {
            noSound.Play();
            return false;
            
        }
        buySound.Play();
        return true;
    }

    public List<string> ItemsBought
    {
        get { return itemsBought; }
    }
}
