using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinoRagdoll : MonoBehaviour
{
    public GameObject Rig;
    GameObject spineBleedSpot;
    public IEnumerator Ragdoll()
    {
        // Remove the dinos current collider
        Destroy(GetComponent<CapsuleCollider>());
        //get all the bones in the rig
        SkinnedMeshRenderer smr = this.GetComponentInChildren<SkinnedMeshRenderer>();
        Transform[] thisBones = smr.bones;
        foreach(Transform bone in thisBones)
        {
            if (bone.name == "BleedPoint"){
                spineBleedSpot = bone.gameObject;
            }
            bone.gameObject.AddComponent<BoxCollider>();
            bone.gameObject.AddComponent<Rigidbody>();
            bone.gameObject.GetComponent<BoxCollider>().size = new Vector3(0.0017f, 0.0017f, 0.0017f);
            bone.gameObject.GetComponent<Rigidbody>().AddForce(Random.Range(-150, 150), Random.Range(200, 400), Random.Range(-150, 150));
        }
        for(int i = 0; i < Random.Range(100, 300); i++)
        {
            if(Random.Range(0, 40) == 1){
                Game.Bleed(spineBleedSpot.transform.position, new Vector3(Random.Range(-45, 45), 0, Random.Range(-45, 45)));
            }
            yield return new WaitForSeconds(0.1f);   
        }
        foreach(Transform bone in thisBones)
        {
            Destroy(bone.GetComponent<Rigidbody>());
            Destroy(bone.GetComponent<BoxCollider>());
        }
        Destroy(gameObject);
    }
}