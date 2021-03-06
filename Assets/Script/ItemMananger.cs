﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMananger : MonoBehaviour
{
    public bool itemOverride;
    public int overrideID;
    private int id;
    private Item item;
    private GameObject overLord;
    private ReadIn read;
    private bool gold = false;
    private int goldAmount;
    public int goldMax = 100;
    private bool bossLoot;

    private bool canPickup;

    // Start is called before the first frame update
    void Start()
    {
        overLord = GameObject.Find("OverLord");
        
        read = overLord.GetComponent<ReadIn>();
        StartCoroutine(LateStart(.1f));
    }

    IEnumerator LateStart(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        //Your Function You Want to Call
        if (!itemOverride){
            item = calcItem();
        }else{
            
            item = read.getItem(overrideID);
            id = overrideID;
        }
        this.GetComponent<SpriteRenderer>().sprite = read.getSprite(id);
        canPickup = true;
    }
    public Item getItem()
    {
        return item;
    }

    public void bossOverride()
    {
        bossLoot = true;
        if(item == null)
        {

        }
    }

    private Item calcItem()
    {
        if (!bossLoot)
        {
            //More Likely to get gold
            if (Random.value > .7)
            {
                //Choose between weapons and armor; lean towards weapons
                int type = Random.Range(0, 10);
                if (type < 6)//ie 0,1,2,3,4,5
                {
                    //weapon
                    //Choose between the 5 weapon subtypes; Weighted evenly
                    int subtype = Random.Range(0, 5);
                    //Choose one of 3 rarities
                    int rarity = Random.Range(0, 10);
                    if (rarity < 6)//ie 0,1,2,3,4,5
                    {
                        rarity = 1;
                    }
                    else if (rarity < 9)//ie 6,7,8
                    {
                        rarity = 2;
                    }
                    else//ie 9
                    {
                        rarity = 3;
                    }
                    id = rarity + (subtype * 3);
                    return read.getItem(id);
                }
                else//ie 6,7,8,9
                {
                    //Armor
                    //Choose between the 3 armor subtypes; Weighted evenly
                    int subtype = Random.Range(0, 3);
                    //Choose one of 3 rarities
                    int rarity = Random.Range(0, 10);
                    if (rarity < 6)//ie 0,1,2,3,4,5
                    {
                        rarity = 1;
                    }
                    else if (rarity < 9)//ie 6,7,8
                    {
                        rarity = 2;
                    }
                    else//ie 9
                    {
                        rarity = 3;
                    }
                    //15 is constant offset to get to the armor in loot table
                    id = 15 + rarity + (subtype * 3);
                    return read.getItem(id);
                }
            }
            else
            {
                gold = true;
                goldAmount = Random.Range(10, goldMax);
                id = 0;
                return null;
            }
        }
        else//Higher Chance or rare gear with boss loot
        {
            //Choose between weapons and armor; lean towards weapons
            int type = Random.Range(0, 10);
            if (type < 6)//ie 0,1,2,3,4,5
            {
                //weapon
                //Choose between the 5 weapon subtypes; Weighted evenly
                int subtype = Random.Range(0, 5);
                //Choose one of 3 rarities
                int rarity = Random.Range(0, 10);
                if (rarity < 6)//ie 0,1,2,3,4,5
                {
                    rarity = 2;
                }
                else if (rarity < 9)//ie 6,7,8
                {
                    rarity = 3;
                }
                else//ie 9
                {
                    rarity = 1;
                }
                id = rarity + (subtype * 3);
                return read.getItem(id);
            }
            else//ie 6,7,8,9
            {
                //Armor
                //Choose between the 3 armor subtypes; Weighted evenly
                int subtype = Random.Range(0, 3);
                //Choose one of 3 rarities
                int rarity = Random.Range(0, 10);
                if (rarity < 6)//ie 0,1,2,3,4,5
                {
                    rarity = 2;
                }
                else if (rarity < 9)//ie 6,7,8
                {
                    rarity = 3;
                }
                else//ie 9
                {
                    rarity = 1;
                }
                //15 is constant offset to get to the armor in loot table
                id = 15 + rarity + (subtype * 3);
                return read.getItem(id);
            }
        }
    }
    public void OnTriggerStay2D(Collider2D other)
    {
        
        if(other.tag == "Inv" && canPickup)
        {
            
            if (gold)
            {
                other.SendMessage("AddGold", goldAmount);
                Destroy(this.gameObject);
            }
            else
            {
                other.SendMessage("AddItem", id);
                Destroy(this.gameObject);
            }
        }
    }

}
