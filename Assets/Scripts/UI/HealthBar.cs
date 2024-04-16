using UnityEngine;
using TMPro;

public class HealthBar : MonoBehaviour
{
    Vector3 pos;
    void Start()
    {
        pos = transform.position;
    }
    void Update()
    {
        int hp = (int) GameObject.Find("Player").GetComponent<PlayerMovement>().health;
        transform.position = new Vector3(pos.x+(hp-100)*6, pos.y, pos.z);
        transform.parent.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Health:" + hp.ToString();
    }
}
