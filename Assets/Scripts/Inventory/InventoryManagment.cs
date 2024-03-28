using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManagment : MonoBehaviour
{
    public int Selectedslot = 1;
    public GameObject HBSlot;
    public GameObject MISlot;
    public Color BaseColor;
    public Color SelectedColor;
    public Slot[] inventory;
    public Slot[] hotbar;
    public IT[] itemDataBase;
    public Texture2D nullt;
    public bool draggingitem = false;
    public int maxItems;
    void Start()
    {
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
        for (int l = 0; l < 3; l += 1)
            for (int i = 0; i < 9; i += 1)
            {
                // create a new childed object of the slot
                GameObject slot = Instantiate(MISlot, new Vector3(400 + i * 132, 100 + (3 - l) * 132, 0), Quaternion.identity);
                slot.GetComponent<InventorySlot>().Init(i + 1 + l * 9, nullt, inventory[i + (l * 9)].Amount);
                slot.transform.SetParent(GameObject.Find("MainInv").transform);
                slot.name = "HotBarSlot " + (i + 1) + " Row " + (l + 1);
            }
    }
    void Update()
    {
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
    }
    public void AddItem(string name, int amount)
    {
        for (int i = 0; i < hotbar.Length; i++)
        {
            if (hotbar[i].Name == name && hotbar[i].Amount < maxItems)
            {
                hotbar[i].Amount += amount;
                // check if the amount is greater than 64
                if (hotbar[i].Amount > maxItems)
                {
                    // calculate the leftover amount
                    int leftoverAmount = hotbar[i].Amount - maxItems;
                    // set the amount to the max per slot
                    hotbar[i].Amount = maxItems;

                    // find the first open slot
                    for (int j = 0; j < hotbar.Length; j++)
                    {
                        if (hotbar[j].Name == "")
                        {
                            // add the leftover amount to the open slot
                            hotbar[j].Name = name;
                            for (int k = 0; k < itemDataBase.Length; k++)
                            {
                                if (itemDataBase[k].Name == name)
                                {
                                    hotbar[j].Icon = itemDataBase[k].Icon;
                                }
                            }
                            hotbar[j].Amount = leftoverAmount;
                            return;
                        }
                    }
                }
                return;
            }
        }
        for (int i = 0; i < hotbar.Length; i++)
        {
            if (hotbar[i].Name == "")
            {
                hotbar[i].Name = name;
                for (int k = 0; k < itemDataBase.Length; k++)
                {
                    if (itemDataBase[k].Name == name)
                    {
                        hotbar[i].Icon = itemDataBase[k].Icon;
                    }
                }
                hotbar[i].Amount = amount;
                return;
            }
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
public struct IT
{
    public string Name;
    public Texture2D Icon;
    public Mesh Mesh;
    public Material[] Material;
}