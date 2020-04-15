using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreUI : MonoBehaviour
{
    public Gallery galleryScript;   //getting the gallery script to be used later
    List<string> itemsBought;   //List to hold the names of the items bought
    private int[] costs;
    private string[] itemNames;


    // Start is called before the first frame update
    void Start()
    {
        itemsBought = new List<string>();


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //function to buy the item
    public void BuyItem(int index)
    {
        if(CheckFunds(costs[index]))
        {
            itemsBought.Add(itemNames[index]);
        }
    }

    //function to check if the player has enough money
    private bool CheckFunds(int cost)
    {
        if(galleryScript.Money < cost)
        {
            return false;
        }

        return true;
    }
}
