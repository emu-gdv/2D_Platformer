﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ReadIn : MonoBehaviour
{

    public List<Item> items = new List<Item>();
    // Start is called before the first frame update
    public List<Sprite> images = new List<Sprite>();
    public List<Sprite> icons = new List<Sprite>();
    void Start()
    {
        string filepath = System.IO.Path.GetFullPath("Assets/Loot_Table.tsv");
        System.IO.StreamReader file = new System.IO.StreamReader(filepath);
        string line = file.ReadLine();
        Item item = new Item();
        items.Add(item);
        while ((line = file.ReadLine()) != null)
        {
            item = new Item();
            item.newItems(line);
            item.setIcon();
            item.setImage();
            items.Add(item);
            //print("Added Item" + item.getId());
        }

        file.Close();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public Sprite getSprite(int id)
    {
        return images[id];
    }
    public Sprite getIcon(int id)
    {
        return icons[id];
    }
    public Item getItem(int i) {
        //Debug.Log("Overlord ReadIn " + i);
        return items[i];
    }
}
