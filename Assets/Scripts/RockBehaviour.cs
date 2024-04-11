using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockBehaviour : MonoBehaviour
{
    Transform player;
    public string Item;
    public int Min;
    public int Max;
    void Awake()
    {
        player = GameObject.Find("Player").transform;
    }
    void Update()
    {
        if (Vector3.Distance(player.position, transform.position) < 2 && Input.GetKeyDown(KeyCode.E))
        {
            GameObject.Find("Inventory").GetComponent<InventoryManagment>().AddItem(Item, Random.Range(Min, Max));
            Destroy(gameObject);
        }
    }
}
