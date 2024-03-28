using UnityEngine;

public class DinoAi : MonoBehaviour
{
    private Transform player;
    private Animation anim;
    private Rigidbody rb;
    public bool agro;
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
    void Awake()
    {
        anim = GetComponent<Animation>();
        rb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player").transform;
        
    }
    public void Set(float MSpeed, float ADist, float IDist, string Dino, DinoManager.AgroType DinoType, float Health, float AttackDist, DinoManager.DinoType dt)
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
    }
    void Update()
    {
        if (instantiated){
            if (agro)
            { 
                transform.LookAt(player);
                // make sure it doesnt look up
                if (DinoLocation == DinoManager.DinoType.Land){
                    transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
                }
                //walk forward and play the animation "Run"
                rb.AddRelativeForce(Vector3.forward * MoveSpeed * Time.deltaTime, ForceMode.VelocityChange);
                if(!anim.IsPlaying(dino + "Attack")){
                    anim.Play(dino + "Run");
                }
                if (Vector3.Distance(transform.position, player.position) > IdleDist)
                {
                    agro = false;
                }
                if (Vector3.Distance(transform.position, player.position) < attackdist && !anim.IsPlaying(dino + "Attack"))
                {
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
                    rb.AddRelativeForce(Vector3.forward * 8 * Time.deltaTime, ForceMode.VelocityChange);
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
                GetComponent<ParticleSystem>().Play();
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
                Destroy(gameObject);
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
