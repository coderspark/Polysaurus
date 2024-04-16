using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingBoard : MonoBehaviour
{
    public Vector3[] SlotPositions;

    void Start(){
        for(int i = 0; i < 5; i++){
            for(int j = -1; j < 1; j++){
                SlotPositions[i + j * 5] = new Vector3((transform.position.x-330)+i*132, transform.position.y + j*132, 0);
            }
        }
    }
}
