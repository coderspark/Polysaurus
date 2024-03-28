using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeBehaviour : MonoBehaviour
{
    private GameObject Player;
    bool exists = true;
    // Update is called once per frame
    void Update()
    {
        if (Player == null)
        {
            Player = GameObject.Find("Player");
        }
        else
        {
            if (Vector3.Distance(Player.transform.position, new Vector3(transform.position.x, Player.transform.position.y, transform.position.z)) < 3 && Input.GetMouseButtonDown(0) && Player.GetComponent<Animation>().IsPlaying("Attack") == false)
            {
                if (exists){
                    GetComponent<ParticleSystem>().Play();
                }
                GameObject.Find("Inventory").GetComponent<InventoryManagment>().AddItem("Wood", Random.Range(1, 3));
                // Set the SetActive of the trees children to false
                foreach (Transform child in transform)
                {
                    child.gameObject.SetActive(false);
                    exists = false;
                }
                Destroy(gameObject, 1.5f); 
            }
        }
    }
}
