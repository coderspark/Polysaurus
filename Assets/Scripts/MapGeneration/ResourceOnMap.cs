using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ItemResource{
    public class ResourceOnMap : MonoBehaviour
    {
        ResourceGenerator.CollectMethod CollectMethod;
        Drop[] Drops;
        bool exists = true;
        public void Init(ResourceGenerator.CollectMethod collectMethod, Drop[] drops){
            Drops = drops;
            CollectMethod = collectMethod;
        }
        void Awake(){
            if(gameObject.name == "Bush"){
                GetComponent<MeshRenderer>().material.color = new Color(0f, Random.Range(0.4f, 1f), 0, 255f);
            }
        }
        void Update(){
            GameObject Player = GameObject.Find("Player");
            if(CollectMethod == ResourceGenerator.CollectMethod.Pickup){
                if (Vector3.Distance(GameObject.Find("Player").transform.position, transform.position) < 2 && Input.GetKeyDown(KeyCode.E))
                {
                    foreach(Drop d in Drops){
                        GameObject.Find("Inventory").GetComponent<InventoryManagment>().AddItem(d.Name, Random.Range(d.MinAmount, d.MaxAmount));
                    }
                    Destroy(gameObject);
                }
            }
            else if(CollectMethod == ResourceGenerator.CollectMethod.Mine){
                if (Vector3.Distance(Player.transform.position, new Vector3(transform.position.x, Player.transform.position.y, transform.position.z)) < 3 && Input.GetMouseButtonDown(0) && Player.GetComponent<Animation>().IsPlaying("Attack") == false)
                {
                    if(exists){
                        GetComponent<ParticleSystem>().Play();
                        foreach (Drop d in Drops){
                            GameObject.Find("Inventory").GetComponent<InventoryManagment>().AddItem(d.Name, Random.Range(d.MinAmount, d.MaxAmount));
                        }
                    }
                    foreach (Transform child in transform)
                    {
                        child.gameObject.SetActive(false);
                        exists = false;
                    }
                    Destroy(gameObject, 1.5f); 
                }
            }
        }
    }
}