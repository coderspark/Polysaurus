using System;
using UnityEngine;
using TMPro;

public class ConsoleCommand : MonoBehaviour
{
    public string Command;
    public string Value;
    public int Amount;
    public void Parse(){
        string cmd = GetComponent<TMP_InputField>().text;
        string[] c = cmd.Split(' ');
        if(c.Length < 2){
            return;
        }
        Command = c[0];
        Value = c[1];
        if(c.Length == 3) Amount = Int32.Parse(c[2]);
    }
    public void Run(){
        if(Command == "GFI"){
            GameObject.Find("Inventory").GetComponent<InventoryManagment>().AddItem(Value, Amount);
        }
    }
}
