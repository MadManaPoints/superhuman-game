using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement player;
    Rigidbody playerRb;
    Animator anim;
    float speed = 3.0f;
    int hInput;
    int yInput;


    void OnAwake(){
        player = this;
    }

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Movement();
        Animations();
    }

    void Movement(){
        hInput = Mathf.FloorToInt(Input.GetAxisRaw("Horizontal"));
        yInput = Mathf.FloorToInt(Input.GetAxisRaw("Vertical"));
        Vector3 vel = transform.forward * yInput;
        if(yInput < 0){
            speed = 1.5f;
        } else {
            speed = 3.0f;
        }
        playerRb.velocity = vel * speed;
    }

    void Animations(){
        anim.SetInteger("RunBack", yInput);
        if(playerRb.velocity != Vector3.zero){
            anim.SetBool("Run", true);
        } else {
            anim.SetBool("Run", false);
        }
        if(anim.GetBool("Run") && Input.GetKeyDown(KeyCode.Space)){
            anim.SetTrigger("RunJump");
        }
    }
}
