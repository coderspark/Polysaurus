using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinoRagdoll : MonoBehaviour
{
    public GameObject Rig;
    public IEnumerator Ragdoll()
    {
        //get all the bones in the rig
        List<GameObject> Bones = new List<GameObject>();
        foreach (GameObject bone in Bones)
        {
            Rigidbody rb = bone.AddComponent<Rigidbody>();
            bone.AddComponent<BoxCollider>();
        }
        yield return new WaitForSeconds(5);
        foreach (GameObject bone in Bones)
        {
            Destroy(bone.GetComponent<Rigidbody>());
            Destroy(bone.GetComponent<BoxCollider>());
        }
        Destroy(gameObject);
    }
}