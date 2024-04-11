using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Main movement variables
    public float moveSpeed = 9f;
    public float counterMovement = 0.35f;
    public float jumpPower = 50f;
    public bool grounded;
    public bool Underwater;
    public float rotationSpeed = 3000f; // Speed of rotation
    public float horizontalInput;
    public float verticalInput;

    // GameObjects, Transforms, and more of that stuff
    private Rigidbody rb;
    private Vector3 level;
    private GameObject cam;
    public Animation anim;
    public LayerMask WhatIsGround;
    // Other things to keep code clean
    public string movestate = "Idle";
    private int randint;
    public float distance;
    
    void Awake()
    {
        rotationSpeed = OptionsMenu.sensetivity;
        rb = GetComponent<Rigidbody>();
        cam = GameObject.Find("Camera");  
        RaycastHit hit;
        while (true)
        {
            transform.position = new Vector3(UnityEngine.Random.Range(-700, 700), 10, UnityEngine.Random.Range(-700, 700));
            if (Physics.Raycast(transform.position, Vector3.down, out hit))
            {
                if (hit.point.y > 9)
                {
                    break;
                }
            }
        }
        if(Physics.Raycast(transform.position, Vector3.down, out hit, WhatIsGround))
        {
            transform.position = hit.point;
        }
        else if (Physics.Raycast(transform.position, Vector3.up, out hit, WhatIsGround))
        {
            transform.position = hit.point;
        }
        transform.position += new Vector3(0, 1, 0);
    }

    // movement
    void Movement()
    {
        movestate = "Idle";
        if(Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = 24f;
        }
        else
        {
            moveSpeed = 14f;
        }
        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce((transform.forward * moveSpeed));
            movestate = "Run";
        }
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce((transform.right * -moveSpeed));
            movestate = "Run";
        }
        if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce((transform.forward * -moveSpeed));
            movestate = "Run";
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce((transform.right * moveSpeed));
            movestate = "Run";
        }
        if (Input.GetKey(KeyCode.Space) && grounded)
        {
            rb.AddForce(Vector3.up * jumpPower);
        }
        ApplyCounterMovement();
        randint = Random.Range(-1, 2);
        if(Input.GetMouseButtonDown(0))
        {
            HoldedItem inv;
            inv = GameObject.Find("Holded Item").GetComponent<HoldedItem>();
            if(inv.CurrentItem != "None")
            { 
                if (randint == 1)
                {
                    movestate = "AttackLeft";
                }
                else if (randint == 0)
                {
                    movestate = "AttackRight";
                }
            }
            else
            {
                movestate = "AttackRight";
            }
            
        }  
        // if the players x velocity is less than 0.1 and more than -0.1, set it to 0
        if (rb.velocity.x < 0.1 && rb.velocity.x > -0.1)
        {
            rb.velocity = new Vector3(0, rb.velocity.y, rb.velocity.z);
        }
        // do the same for the z velocity
        if (rb.velocity.z < 0.1 && rb.velocity.z > -0.1)
        {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, 0);
        }
        if (transform.position.y < 2.5)
        {
            if(!Underwater)
            {
                GetComponent<ParticleSystem>().Play();
            }
            Underwater = true;
        }
        else
        {
            Underwater = false;
        }
    }
    void Animate()
    {
        if(!grounded)
        {
            movestate = "Jump"; 
        }
        anim.Play(movestate);
    }
    private void ApplyCounterMovement()
    {
        // Get the current velocity of the rigidbody
        Vector3 currentVelocity = rb.velocity;
        if(!grounded)
        {
            currentVelocity.y = 0;
        }
        // Calculate the counter movement force
        Vector3 counterForce = -currentVelocity * counterMovement;
        // Apply the counter movement force to the rigidbody
        rb.AddForce(counterForce, ForceMode.Force);
    }
    void RotateCamera()
    {
        // add turn on x and y
        horizontalInput += Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
        verticalInput -= Input.GetAxis("Mouse Y") * (rotationSpeed / 2f) * Time.deltaTime;
        
        // reset horizontal input if it goes over 360
        if (horizontalInput > 360 || horizontalInput < -360)
        {
            horizontalInput = 0;
        }
        verticalInput = Mathf.Clamp(verticalInput, -40f, 60f);

        // this f***ing quaternion bulls*** took 2 HOURS D:
        transform.rotation = Quaternion.Euler(0f, horizontalInput, 0f);
        cam.transform.rotation = Quaternion.Euler(verticalInput, horizontalInput, 0f);
        
    }
    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, Vector3.down, out hit, WhatIsGround))
        {
            level = hit.point;
        }
        // set ground to true/false
        if (level != null){
            distance = Mathf.Abs(level.y - transform.position.y);
        }
        else
        {
            distance = 9999f;
        }
        if (distance < 0.8f)
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }
        if(!anim.IsPlaying("AttackLeft") && !anim.IsPlaying("AttackRight") && !PauseMenu.GameIsPaused){Movement();}
        RotateCamera();
        Animate();
    }  
}
