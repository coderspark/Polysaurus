using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldedItem : MonoBehaviour
{
    public InventoryManagment inv;
    
    public string CurrentItem;
    public MeshFilter meshFilter;
    public MeshRenderer meshRenderer;
    void Update()
    {
        CurrentItem = inv.hotbar[inv.Selectedslot - 1].Name;
        for (int i = 0; i < inv.itemDataBase.Length; i++)
        {
            if (CurrentItem == inv.itemDataBase[i].Name)
            {
                meshRenderer.materials = inv.itemDataBase[i].Material;
                meshFilter.mesh = inv.itemDataBase[i].Mesh;
            }
        }
    }
}
