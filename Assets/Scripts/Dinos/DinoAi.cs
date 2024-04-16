using UnityEngine;

public class DinoAi : MonoBehaviour
{
    private Transform player;
    private Animation anim;
    private Rigidbody rb;
    public bool agro;
    public bool Dead = false;
    float MoveSpeed= 0;
    float AgroDist = 0;
    float IdleDist = 0;
    float attackdist = 0;
    string dino;
    DinoManager.AgroType dinotype;
    DinoManager.DinoType DinoLocation;
    private bool instantiated = false;
    bool readytoagro = false;
    public bool runningaway = false;
    public float health;
    public float Maxhealth;
    public float AttackDamage;
    void Awake()
    {
        anim = GetComponent<Animation>();
        rb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player").transform;
        Dead = false;
    }
    public void Set(float MSpeed, float ADist, float IDist, string Dino, DinoManager.AgroType DinoType, float Health, float AttackDist, DinoManager.DinoType dt, float Attackdamage)
    {
        MoveSpeed = MSpeed;
        AgroDist = ADist;
        IdleDist = IDist;
        dinotype = DinoType;
        attackdist = AttackDist;
        DinoLocation = dt;
        dino = Dino;
        instantiated = true;
        health = Random.Range(Health * 0.75f, Health * 1.25f);
        Maxhealth = health;
        agro = false;
        RaycastHit hit;
        if (DinoLocation == DinoManager.DinoType.Land){
            // rerandomize the dinos position until the ground under it has a y value of > 9
            while (true)
            {
                transform.position = new Vector3(UnityEngine.Random.Range(-700, 700), 20, UnityEngine.Random.Range(-700, 700));
                if (Physics.Raycast(transform.position, Vector3.down, out hit))
                {
                    if (hit.point.y > 9)
                    {
                        break;
                    }
                }
            }
        }
        AttackDamage = Attackdamage;
    }
    void Update()
    {
        if (instantiated && !PauseMenu.GameIsPaused && !Dead){
            if (agro)
            { 
                transform.LookAt(player);
                // make sure it doesnt look up
                if (DinoLocation == DinoManager.DinoType.Land){
                    transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
                }
                //walk forward and play the animation "Run"
                if(!anim.IsPlaying(dino + "Attack")){
                    rb.AddRelativeForce(Vector3.forward * MoveSpeed * Time.deltaTime, ForceMode.VelocityChange);
                    anim.Play(dino + "Run");
                }
                if (Vector3.Distance(transform.position, player.position) > IdleDist)
                {
                    agro = false;
                }
                if (Vector3.Distance(transform.position, player.position) < attackdist && !anim.IsPlaying(dino + "Attack"))
                {
                    player.GetComponent<PlayerMovement>().health -= AttackDamage;
                    Game.Bleed(player.position, new Vector3(0, 0, 0), new Vector3(0.5f, 0.5f, 0.5f));
                    anim.Play(dino + "Attack");
                }
            }
            else
            {
                if (!runningaway)
                {
                    int randint = UnityEngine.Random.Range(0, 1000);
                    if (randint == 1)
                    {
                        transform.eulerAngles = new Vector3(0, UnityEngine.Random.Range(0, 360), 0);
                    }
                    anim.Play(dino + "Run");
                    rb.AddRelativeForce(Vector3.forward * MoveSpeed/1.5f * Time.deltaTime, ForceMode.VelocityChange);
                    if (Vector3.Distance(transform.position, player.position) < AgroDist && !readytoagro)
                    {
                        if(dinotype == DinoManager.AgroType.Agressive){
                            readytoagro = true;
                            anim.Play(dino + "Scream");
                        }
                        else if (dinotype == DinoManager.AgroType.Scared){
                            runningaway = true;
                        }
                    }
                    if (readytoagro && !anim.IsPlaying(dino + "Scream"))
                    {
                        agro = true;
                        readytoagro = false;
                    }
                }
                else
                {
                    transform.LookAt(player);
                    // turn away from the player
                    transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);
                    rb.AddRelativeForce(Vector3.forward * MoveSpeed * Time.deltaTime, ForceMode.VelocityChange);
                    anim.Play(dino + "Run");
                    if (Vector3.Distance(transform.position, player.position) > IdleDist)
                    {
                        runningaway = false;
                    }
                }
            }
             ApplyCounterMovement();
            if(Vector3.Distance(transform.position, player.position) < 7 && Input.GetMouseButtonDown(0) && !player.GetComponent<Animation>().IsPlaying("Attack"))
            {
                health -= 10;
                // get the position of the dinos head bone with a skinned mesh renderer
                SkinnedMeshRenderer smr = this.GetComponentInChildren<SkinnedMeshRenderer>();
                Transform[] thisBones = smr.bones;
                foreach(Transform bone in thisBones)
                {
                    if (bone.tag == "DinoBleedPoint"){
                        Game.Bleed(bone.position, new Vector3(0, 0, 0), new Vector3(1f, 1f, 1f));
                    }
                }   
                runningaway = false;
                if(dinotype == DinoManager.AgroType.Neutral){
                        readytoagro = true;
                }
                else if (dinotype == DinoManager.AgroType.RunAfterAttacked){
                    runningaway = true;
                }
            }
            if (health <= 0)
            {
                if(!Dead){
                    GameObject.Find("Inventory").GetComponent<InventoryManagment>().AddItem("RawMeat", (int)(Maxhealth/15)+1);
                    GameObject.Find("Inventory").GetComponent<InventoryManagment>().AddItem("DinoHide", (int)(Maxhealth/30)+1);
                    StartCoroutine(Game.Ragdoll(gameObject));
                }
                Dead = true;
            }
        }
    }
    private void ApplyCounterMovement()
    {
        // Get the current velocity of the rigidbody
        Vector3 currentVelocity = rb.velocity;
        // Calculate the counter movement force
        Vector3 counterForce = -currentVelocity * 0.35f;
        // Apply the counter movement force to the rigidbody
        rb.AddForce(counterForce, ForceMode.Force);
    }
}
