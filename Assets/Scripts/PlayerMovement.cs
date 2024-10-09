using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement player;
    Rigidbody playerRb;
    Animator anim;
    CapsuleCollider cap;
    public LayerMask whatIsGround;
    [SerializeField] GameObject pm;
    Vector3 ogCap;
    Vector3 capCol; 
    float speed = 7.0f;
    float teleTimer = 3.0f;
    float transportTimer = 1.5f;
    float hInput;
    float yInput;
    bool canJump;
    
    public float playerHeight;
    float maxSpeed = 5.0f;
    public bool playerControl = true;
    public bool isHitting;
    public bool startTeleport;
    bool up, left, down, right;
    public bool isDed;
    bool grounded;
    bool onPlat;
    Vector3 centerScreen = new Vector3(0.5f, 0.5f, 0f);
    public Vector3 newPos;

    void OnAwake(){
        player = this;
    }

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        cap = GetComponent<CapsuleCollider>();
        pm.SetActive(false);
        capCol = cap.center;
        ogCap = capCol;
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        //Debug.Log(transform.position + "   " + newPos);
        cap.center = capCol;
        Animations();
        Casting();

        if(!playerControl){
            //Application.targetFrameRate = 5;
            teleTimer -= Time.deltaTime;
            playerRb.isKinematic = true;
            if(teleTimer <= 1.5f && startTeleport){
                transform.position = newPos;
                pm.SetActive(false);
                startTeleport = false;
            } else if(teleTimer <= 0f){
                newPos = Vector3.zero;
                teleTimer = 3.0f;
                playerControl = true;
                playerRb.isKinematic = false;
            }
        }

        Ded();
    }

    void Casting(){
        int layerMask = 1 << 8;
        //layerMask = ~layerMask;

        Ray laser = Camera.main.ViewportPointToRay(centerScreen);
        RaycastHit hit = new RaycastHit();

        if(Physics.Raycast(laser, out hit, 50.0f, layerMask)){
            
            if(hit.collider.tag == "Platform"){
                if(newPos == Vector3.zero){
                    newPos = new Vector3(hit.collider.gameObject.transform.position.x, hit.collider.gameObject.transform.position.y + 0.5f, hit.collider.gameObject.transform.position.z);

                }
                isHitting = true;
            } else {
                isHitting = false;
                if(!startTeleport){
                    newPos = Vector3.zero;
                }
            }
        } else {
            isHitting = false;
            if(!startTeleport){
                newPos = Vector3.zero;
            }
        }
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.1f, whatIsGround);
    }

    void FixedUpdate(){
        if(playerControl){
            Movement();
        }
    }

    void Movement(){
        Vector3 move = Vector3.zero;
        if(Input.GetKey(KeyCode.W)){
            up = true;
            move += transform.forward; 
        } else {
            up = false;
        }
        if(Input.GetKey(KeyCode.A) && !up){
            left = true;
            move -= transform.right;
        } else {
            left = false;
        }
        if(Input.GetKey(KeyCode.S) && !up){
            down = true;
            move -= transform.forward;
        } else {
            down = false;
        }
        if(Input.GetKey(KeyCode.D) && !up){
            right = true;
            move += transform.right;
        } else {
            right = false;
        }

        if(down || left || right){
            speed = 4.0f; 
        } else {
            speed = 7.0f;
        }

        move = move.normalized * speed;
        move.y = playerRb.velocity.y;

        if(Input.GetKeyDown(KeyCode.Space) && grounded){
            move.y = 3.0f;
        }

        playerRb.velocity = move; 
    }

    void Animations(){
        hInput = Mathf.FloorToInt(Input.GetAxisRaw("Horizontal"));
        yInput = Mathf.FloorToInt(Input.GetAxisRaw("Vertical"));

        if(anim.GetBool("Run") && Input.GetKeyDown(KeyCode.Space)){
            anim.SetTrigger("RunJump");
        }

        if(Input.GetMouseButton(1) && Input.GetMouseButtonDown(0) && isHitting && playerControl && (grounded || onPlat)){
            anim.SetTrigger("Teleport");
            pm.SetActive(true);
            startTeleport = true;
            playerControl = false;
        }

        anim.SetInteger("RunBack", (int)yInput);
        anim.SetInteger("LeftStrafe", (int)hInput);
        anim.SetInteger("RightStrafe", (int)hInput);
        if(playerRb.velocity != Vector3.zero && yInput == 1){
            anim.SetBool("Run", true);
        } else {
            anim.SetBool("Run", false);
        } 
    }

    public void Ded(){
        if(transform.position.y < -3.0f){
            isDed = true;
        }
    }

    void OnCollisionEnter(Collision col){
        if(col.gameObject.tag == "Ground" || col.gameObject.tag == "Platform"){
            canJump = true;
            onPlat = true;
        }
    }

    void OnCollisionExit(Collision col){
        if(col.gameObject.tag == "Ground" || col.gameObject.tag == "Platform"){
            canJump = false;
            onPlat = false;
        }
    }
}
