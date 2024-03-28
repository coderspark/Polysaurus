using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainInventory : MonoBehaviour
{   
    public bool InventoryOpen = false;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            InventoryOpen = !InventoryOpen;  
        }
        foreach(Transform child in transform)
        {
            child.gameObject.SetActive(InventoryOpen);
        }
    }
}
