using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ItemResource{
    public class ResourceGenerator : MonoBehaviour
    {
        public Resource[] resources;
        public enum CollectMethod {Pickup, Mine}
        void Start()
        {
            foreach (Resource r in resources){
                for (int i = -700; i < 700; i += r.density)
                {
                    for (int j = -700; j < 700; j += r.density)
                    {
                        transform.position = new Vector3(i, 80, j);
                        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit) && hit.point.y < r.MaxHeight && hit.point.y > r.MinHeight && Random.Range(0, 40) < 10)
                        {
                            GameObject ob = Instantiate(r.Object, new Vector3(transform.position.x+Random.Range(-4f, 4f), hit.point.y, transform.position.z+Random.Range(-4f, 4f)), Quaternion.Euler(0, Random.Range(0, 360), 0));
                            ResourceOnMap rom = ob.GetComponent<ResourceOnMap>();
                            rom.Init(r.collectMethod, r.Drops);
                            ob.name = r.Name;
                        }
                    }    
                }
            }
        }
    }

    [System.Serializable]
    public struct Resource
    {
        public string Name;
        public ResourceGenerator.CollectMethod collectMethod;
        public GameObject Object;
        public int density;
        public float MinHeight;
        public float MaxHeight;
        public Drop[] Drops;
    }

    [System.Serializable]
    public struct Drop{
        public string Name;
        public int MinAmount;
        public int MaxAmount;
    }
}