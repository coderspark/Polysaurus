using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Game
{
    public static void Bleed(Vector3 position, Vector3 rot, Vector3 scale){
        GameObject Bld = Resources.Load("Blood") as GameObject;
        // create a new blood object called b at the position
        GameObject b = GameObject.Instantiate(Bld, position, Quaternion.Euler(rot));
        b.transform.localScale = scale;
        b.GetComponent<ParticleSystem>().Play();
        GameObject.Destroy(b, 2f);
    }
    public static IEnumerator Ragdoll(GameObject ob)
    {
        GameObject spineBleedSpot = null;
        // Remove the dinos current collider
        Object.Destroy(ob.GetComponent<CapsuleCollider>());
        //get all the bones in the rig
        SkinnedMeshRenderer smr = ob.GetComponentInChildren<SkinnedMeshRenderer>();
        Transform[] thisBones = smr.bones;
        foreach(Transform bone in thisBones)
        {
            if (bone.tag == "DinoBleedPoint"){
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
                Game.Bleed(spineBleedSpot.transform.position, new Vector3(Random.Range(-45, 45), 0, Random.Range(-45, 45)), new Vector3(1f, 1f, 1f));
            }
            yield return new WaitForSeconds(0.1f);   
        }
        foreach(Transform bone in thisBones)
        {
            Object.Destroy(bone.GetComponent<Rigidbody>());
            Object.Destroy(bone.GetComponent<BoxCollider>());
        }
        GameObject.Destroy(ob);
    }
}
