using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Craftable : MonoBehaviour
{
    Texture2D Item;
    Recipe[] recipe;
    public bool[] recipeCheck;
    public void Init(Texture2D item, Recipe[] rec){
        Item = item;
        recipe = rec;
    }
    void Update(){
        recipeCheck = new bool[recipe.Length];
        foreach(Recipe r in recipe){
            foreach (Slot item in GameObject.Find("Inventory").GetComponent<InventoryManagment>().inventory){
                if(item.Name == r.Name && item.Amount >= r.Amount){
                    recipeCheck[System.Array.IndexOf(recipe, r)] = true;
                }
            }
        }
    }
}
