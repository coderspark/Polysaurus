using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour
{
    private Color Base;
    private Color Selected;
    public int SlotID;
    public Texture2D texture;
    private int Amount;
    public InventoryManagment InventoryManagment;
    public void Init(int ID, Texture2D txture, int amount)
    {
        SlotID = ID;
        texture = txture;
        Amount = amount;
    }
    void Update()
    {
        InventoryManagment = GameObject.Find("Inventory").GetComponent<InventoryManagment>();
        // get the children
        foreach (Transform child in transform)
        {
            if (child.gameObject.name == "Item")
            {
                child.gameObject.GetComponent<RawImage>().texture = texture;
            }
            if(child.gameObject.name == "Amount")
            {
                TextMeshProUGUI tmp = child.gameObject.GetComponent<TextMeshProUGUI>();
                if (Amount <= 1)
                {
                    tmp.text = "";
                }
                else
                {
                    tmp.text = Amount.ToString();
                }
            }
        }
    }
}
