using System.Collections;
using System.Collections.Generic;
using UnityEngine;  
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject PauseMenuUI;

    // Update is called once per frame
    void Update(){
        if (Input.GetKeyDown(KeyCode.Escape)){
            if (GameIsPaused){
                Resume();
            } else {
                Pause();
            }
        }
    }
    public void Resume(){
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Pause(){
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        // unlock the cursor
        Cursor.lockState = CursorLockMode.None;
        GameObject.Find("MainInv").GetComponent<MainInventory>().InventoryOpen = false;
    }
    public void Exit(){
        SceneManager.LoadScene("Main Menu");
    }
}
