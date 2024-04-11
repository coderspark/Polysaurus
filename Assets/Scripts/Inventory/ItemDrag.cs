using UnityEngine;
public class ItemDrag : MonoBehaviour
{
    public bool isDragging = false;
    public Vector3 startPosition;
    void Start()
    {
        startPosition = transform.position;
    }
    public void Drag(bool calledelsewhere = false)
    {
        if(isDragging & calledelsewhere)
        {
            isDragging = false;
            transform.position = startPosition;
        }
        else
        {
            bool NoneDragging = true;
            foreach(Transform child in transform.parent.parent){
                ItemDrag c = child.GetChild(3).GetComponent<ItemDrag>();
                if(c.isDragging){
                    c.Drag(true);
                    GameObject.Find("Inventory").GetComponent<InventoryManagment>().SwapItems(child.GetComponent<InventorySlot>().SlotID, transform.parent.GetComponent<InventorySlot>().SlotID);
                    NoneDragging = false;
                }
            }
            if(NoneDragging)
            {
                isDragging = true;
            }
        }
    }

    void Update()
    {
        if (isDragging)
        {
            Vector2 mousePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            transform.position = new Vector3(mousePos.x-60, mousePos.y-60, 0);
            transform.parent.SetAsLastSibling();
        }
    }
}
