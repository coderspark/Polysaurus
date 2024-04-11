using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Game
{
    public static void Bleed(Vector3 position, Vector3 rot){
        GameObject Bld = Resources.Load("Blood") as GameObject;
        // create a new blood object called b at the position
        GameObject b = GameObject.Instantiate(Bld, position, Quaternion.Euler(rot));
        b.GetComponent<ParticleSystem>().Play();
    }
}
