using UnityEngine;
using TMPro;

public class HungerBar : MonoBehaviour
{
    Vector3 pos;
    void Start()
    {
        pos = transform.position;
    }
    void Update()
    {
        int hg = (int) GameObject.Find("Player").GetComponent<PlayerMovement>().hunger;
        transform.position = new Vector3(pos.x+(hg-100)*6, pos.y, pos.z);
        transform.parent.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Hunger:" + hg.ToString();
    }
}