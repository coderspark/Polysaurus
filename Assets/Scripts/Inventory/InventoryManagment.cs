using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class InventoryManagment : MonoBehaviour
{
    public int Selectedslot = 1;
    public GameObject HBSlot;
    public GameObject MISlot;
    public GameObject CRSlot;
    public Color BaseColor;
    public Color SelectedColor;
    public Slot[] inventory;
    public Slot[] hotbar;
    public IT[] itemDataBase;
    public string[] craftableItems;
    public Texture2D nullt;
    public bool draggingitem = false;
    public int maxItems;
    void Start()
    {
        itemDataBase[0].Mesh = new Mesh();
        // set the hotbar to the last 9 slots of the inventory
        hotbar = inventory.TakeLast(9).ToArray();
        Hotbar();
        Inventory();
    }
    void Hotbar()
    {
        for (int i = 0; i < 9; i += 1)
        {
            // create a new childed object of the slot
            GameObject slot = Instantiate(HBSlot, new Vector3(100 + i * 132, 100, 0), Quaternion.identity);
            slot.GetComponent<SlotSelect>().Init(BaseColor, SelectedColor, i + 1, nullt, hotbar[i].Amount);
            slot.transform.SetParent(transform);
            slot.name = "HotBarSlot " + (i + 1);
        }
    }
    void Inventory()
    {
        for (int l = 0; l < 4; l += 1)
            for (int i = 0; i < 9; i += 1)
            {
                // create a new childed object of the slot
                GameObject slot = Instantiate(MISlot, new Vector3(400 + i * 132, 300 + (3 - l) * 132, 0), Quaternion.identity);
                slot.GetComponent<InventorySlot>().Init(i + 1 + l*9, nullt, inventory[i + (l * 9)].Amount);
                slot.transform.SetParent(GameObject.Find("MainInv").transform);
                slot.name = "Slot " + (i + 1) + " Row " + (l + 1);
            }
    }
    // void Craftableitems()
    // {
    //     GameObject cb = GameObject.Find("CraftingBoard")
    //     foreach(IT i in itemDataBase){
    //         if(IT.Craftable == true){
    //             Instantiate(CRSlot);
    //         }
    //     }
    // }
    void Update()
    {
        hotbar = inventory.TakeLast(9).ToArray();
        // check inputs 1 through 9 and set selected slot to that
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Selectedslot = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Selectedslot = 2;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Selectedslot = 3;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Selectedslot = 4;
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            Selectedslot = 5;
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            Selectedslot = 6;
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            Selectedslot = 7;
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            Selectedslot = 8;
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            Selectedslot = 9;
        }
        foreach (Transform child in transform)
        {
            child.GetComponent<SlotSelect>().Init(BaseColor, SelectedColor, child.GetComponent<SlotSelect>().SlotID, hotbar[child.GetComponent<SlotSelect>().SlotID - 1].Icon, hotbar[child.GetComponent<SlotSelect>().SlotID - 1].Amount);
        }
        foreach(Transform child in GameObject.Find("MainInv").transform)
        {
            child.GetComponent<InventorySlot>().Init(child.GetComponent<InventorySlot>().SlotID, inventory[child.GetComponent<InventorySlot>().SlotID - 1].Icon, inventory[child.GetComponent<InventorySlot>().SlotID - 1].Amount);
        }
        foreach(Slot item in inventory){
            Slot i = inventory[System.Array.IndexOf(inventory, item)];
            if(i.Amount == 0){
                i.Name = "";
                i.Icon = nullt;
            }
        }
    }
    public void SwapItems(int slot1, int slot2)
    {
        Slot temp = inventory[slot1 - 1];
        inventory[slot1 - 1] = inventory[slot2 - 1];
        inventory[slot2 - 1] = temp;
    }
    public void AddItem(string name, int amount)
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i].Name == name && inventory[i].Amount < maxItems)
            {
                inventory[i].Amount += amount;
                // check if the amount is greater than 64
                if (inventory[i].Amount > maxItems)
                {
                    // calculate the leftover amount
                    int leftoverAmount = inventory[i].Amount - maxItems;
                    // set the amount to the max per slot
                    inventory[i].Amount = maxItems;

                    // find the first open slot
                    for (int j = 0; j < inventory.Length; j++)
                    {
                        if (inventory[j].Name == "")
                        {
                            // add the leftover amount to the open slot
                            inventory[j].Name = name;
                            for (int k = 0; k < itemDataBase.Length; k++)
                            {
                                if (itemDataBase[k].Name == name)
                                {
                                    inventory[j].Icon = itemDataBase[k].Icon;
                                }
                            }
                            inventory[j].Amount = leftoverAmount;
                            return;
                        }
                    }
                }
                return;
            }
        }
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i].Name == "")
            {
                inventory[i].Name = name;
                for (int k = 0; k < itemDataBase.Length; k++)
                {
                    if (itemDataBase[k].Name == name)
                    {
                        inventory[i].Icon = itemDataBase[k].Icon;
                    }
                }
                inventory[i].Amount = amount;
                return;
            }
        }
    }
    public void RemoveItem(int slot, int amount){
        inventory[slot - 1].Amount -= amount;
        if(inventory[slot - 1].Amount < 0){
            inventory[slot - 1].Amount = 0;
        }
        if (inventory[slot - 1].Amount == 0)
        {
            inventory[slot - 1].Name = "";
            inventory[slot - 1].Icon = nullt;
            inventory[slot - 1].Amount = 0;
        }
    }
}
[System.Serializable]
public struct Slot
{
    public Texture2D Icon;
    public string Name;
    public int Amount;
}
[System.Serializable]
public struct Recipe
{
    public string Name;
    public int Amount;
}
[System.Serializable]
public struct IT
{
    public string Name;
    public Texture2D Icon;
    public Mesh Mesh;
    public Material[] Material;
    public bool Craftable;
    public Recipe[] Recipe;
}