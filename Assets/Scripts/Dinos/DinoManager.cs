using UnityEngine;
using System;
public class DinoManager : MonoBehaviour
{
    public Dino[] Dinos;
    public Material[] materials;
    private DinoAi dinoscript;
    public bool SpawnDinosOnStart;
    public enum AgroType {Agressive, Scared, Neutral, RunAfterAttacked};
    public enum DinoType {Flyer, Land, Water}
    void Start()
    {
        if(SpawnDinosOnStart){SpawnDinos();}
    }
    public void SpawnDinos()
    {
        foreach(Dino dino in Dinos){
            for (int i = 1; i <= dino.spawn_rate; i++){
                // create a new dino
                GameObject dinoPrefab = Instantiate(dino.DinoPrefab, new Vector3(UnityEngine.Random.Range(-700, 700), transform.position.y, UnityEngine.Random.Range(-700, 700)), Quaternion.identity);
                dinoPrefab.name = dino.DinoName + " " + i;
                // set the dino's material
                dinoPrefab.GetComponentInChildren<SkinnedMeshRenderer>().material = materials[UnityEngine.Random.Range(0, materials.Length)];
                dinoscript = dinoPrefab.GetComponent<DinoAi>();
                dinoscript.Set(dino.DinoSpeed, dino.DinoAgroDistance, dino.DinoIdleDistance, dino.DinoName, dino.DinoAgroType, dino.Health, dino.DinoAttackDistance, dino.DinoType);
            }
        }
    }
}
[System.Serializable]
public struct Dino{
    public string DinoName;
    public DinoManager.AgroType DinoAgroType;
    public DinoManager.DinoType DinoType;
    public float DinoSpeed;
    [Range(1, 100)]
    public int spawn_rate;
    [Range(1, 1000)]
    public float Health;
    public float DinoAttackDistance;
    public float DinoAgroDistance;
    public float DinoIdleDistance;
    public GameObject DinoPrefab;
}