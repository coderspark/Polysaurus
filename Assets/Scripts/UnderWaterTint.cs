using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnderWaterTint : MonoBehaviour
{
    public Color Normal;
    public Color UnderWater;
    // Update is called once per frame
    void Update()
    {
        if(GameObject.Find("Cam").transform.position.y < 2.5){
            GetComponent<Image>().color = UnderWater;
        }
        else{
            GetComponent<Image>().color = Normal;
        }
    }
}
