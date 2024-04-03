using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    private int index;
    private string name;
    private ItemType type;
    private Sprite image;

    public int Index
    {
        get { return index; }
        set { index = value; }
    }

    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    public ItemType Type
    {
        get { return type; }
        set { type = value; }
    }

    public Sprite Image
    {
        get { return image; }
        set { image = value; }
    }

    public Item(int index, string name, ItemType type)
    {
        this.index = index;
        this.name = name;
        this.type = type;
    }
}

public class Inventory
{
    private Item[] items = new Item[16];

    // 아이템 인덱서(Indexer)
    public Item this[int index]
    {
        get { return items[index]; }
        set { items[index] = value; }
    }

    public int ItemCount
    {
        get
        {
            int count = 0;
            foreach (Item item in items)
            {
                if (item != null)
                {
                    count++;
                }
            }

            return count;
        }
    }

    public bool AddItem(Item item)
    {
        for(int i = 0; i < items.Length; i++)
        {
            if (items[i] == null)
            {
                items[i] = item;
                return true;
            }
        }
        return false;
    }

    public void RemoveItem(Item item)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == item)
            {
                items[i] = null;
                break;
            }
        }
    }
}


public class ExGameSystem : MonoBehaviour
{
    private Inventory inventory = new Inventory();

    Item sword = new Item(0, "Sword", ItemType.Weapon);
    Item shield = new Item(1, "Shield", ItemType.Armor);

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            inventory.AddItem(sword);
            DebugInventory();
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            inventory.AddItem(shield);
            DebugInventory();
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            inventory.RemoveItem(sword);
            DebugInventory();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            inventory.RemoveItem(shield);
            DebugInventory();
        }
    }

    void DebugInventory()
    {
        Debug.Log("Player Inventory : " + GetInventoryAsString());
    }

    private string GetInventoryAsString()
    {
        string result = "";
        for(int i = 0; i < inventory.ItemCount; i++)
        {
            if (inventory[i] != null)
            {
                result += inventory[i].Name + ",";
            }
        }
        return result.TrimEnd(',');
    }

}


public enum ItemType
{
    Weapon,
    Armor,
    Potion,
    QuestItem
}