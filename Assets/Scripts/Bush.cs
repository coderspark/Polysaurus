using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bush : MonoBehaviour
{
    Transform player;
    public string[] items;
    void Awake()
    {
        player = GameObject.Find("Player").transform;
        GetComponent<MeshRenderer>().material.color = new Color(0f, Random.Range(0.4f, 1f), 0, 255f);
    }
    void Update()
    {
        if (Vector3.Distance(player.position, transform.position) < 2 && Input.GetKeyDown(KeyCode.E))
        {
            InventoryManagment inv = GameObject.Find("Inventory").GetComponent<InventoryManagment>();
            foreach (string item in items)
            {
                inv.AddItem(item, Random.Range(1, 7));
            }
            // inv.AddItem("Berries", Random.Range(1, 5));
            Destroy(gameObject);
        }
    }
}
