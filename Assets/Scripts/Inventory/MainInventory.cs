using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainInventory : MonoBehaviour
{   
    public bool InventoryOpen = false;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I) && !PauseMenu.GameIsPaused)
        {
            InventoryOpen = !InventoryOpen;  
        }
        foreach(Transform child in transform)
        {
            child.gameObject.SetActive(InventoryOpen);
        }
        if (InventoryOpen){
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else if (!PauseMenu.GameIsPaused){
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
