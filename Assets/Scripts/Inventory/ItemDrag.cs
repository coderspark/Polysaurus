using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrag : MonoBehaviour
{
    private bool isDragging = false;
    void Update(){
        if(Input.GetMouseButton(0)){
            // check if the click is on the object
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out hit)){
                if(hit.collider.gameObject == gameObject && !isDragging){
                    isDragging = true;
                }
                else if(isDragging && hit.collider.gameObject.name == "Item"){
                    isDragging = false;
                }
            }
        }
        if(isDragging){
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(mousePos.x, mousePos.y, 0);
        }
    }
}