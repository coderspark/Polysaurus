using UnityEngine;
using UnityEngine.UI;

public class ItemDrag : MonoBehaviour
{
    public Vector3 mousePosition;
    public bool isDragging = false;
    Vector3 startPos;
    void Update()
    {
        mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        if (isDragging)
        {   
            // go to the front of the canvas	
            transform.parent.SetAsLastSibling();
            transform.position = new Vector3(mousePosition.x, mousePosition.y, 0);
        }
    }

    public void Drag()
    {
        isDragging = !isDragging;
        if(isDragging){
            foreach(Transform child in transform.parent.parent){
                if(child.gameObject != gameObject){
                    Transform c = child.GetChild(3);
                    if(c.gameObject.GetComponent<ItemDrag>().isDragging){
                        // c.gameObject.GetComponent<ItemDrag>().isDragging = false;
                        int id = child.gameObject.GetComponent<InventorySlot>().SlotID;
                        InventoryManagment inv = GameObject.Find("Inventory").GetComponent<InventoryManagment>();
                        Debug.Log(id + " " + transform.parent.gameObject.GetComponent<InventorySlot>().SlotID);
                        inv.SwapItems(id, transform.parent.gameObject.GetComponent<InventorySlot>().SlotID);
                        id = 0;
                    }
                }
            }
            startPos = transform.position;
        }
        else{
            transform.position = startPos;
        }
        
    }
}