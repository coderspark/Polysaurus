using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public Transform head;
    void Update()
    {   
        transform.position = head.position;
    }

}
