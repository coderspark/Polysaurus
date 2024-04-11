using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftableItem : MonoBehaviour
{
    private Color Base;
    private Color Selected;
    public int SlotID;
    public Texture2D texture;
    public void Init(int ID, Texture2D txture)
    {
        SlotID = ID;
        texture = txture;
    }
    void Update()
    {
        foreach (Transform child in transform)
        {
            if (child.gameObject.name == "Item")
            {
                child.gameObject.GetComponent<RawImage>().texture = texture;
            }
        }
    }
}