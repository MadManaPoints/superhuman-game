using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Unity.VisualScripting;
using System;

public class CamFollow : MonoBehaviour
{
    PlayerMovement player;
    [SerializeField] Transform playerPos;
    [SerializeField] Transform orientation;
    private float xRotation;
    private float yRotation;
    [SerializeField] float sens;
    [SerializeField] float a, b;
    CinemachineVirtualCamera vcam;
    float zoomIn = 0.0f;
    float zoomOut = 1.0f;
    
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerMovement>();
        vcam = GetComponent<CinemachineVirtualCamera>();
    }

    void Update()
    {
        CamControls();
    }

    void CamControls(){
        float mouseX = Input.GetAxisRaw("Mouse X");
        float mouseY = Input.GetAxisRaw("Mouse Y");

        yRotation += mouseX * Time.deltaTime * sens;
        xRotation -= mouseY * Time.deltaTime * sens / 4;

        xRotation = Mathf.Clamp(xRotation, a, b);

        if(player.playerControl){
            orientation.rotation = Quaternion.Euler(xRotation, yRotation, 0f);
            playerPos.rotation = Quaternion.Euler(0f, yRotation, 0f);
        }

        Zoom();
        

        
    }

    void Zoom(){
        if(!player.playerControl){
            vcam.m_Lens.FieldOfView = Mathf.Lerp(vcam.m_Lens.FieldOfView, 60.0f, zoomOut);
            zoomIn = 0.0f;
            if(zoomOut < 1.0f){
                zoomOut += Time.deltaTime;
            } else {
                zoomOut = 1.0f;
            }
        } else if(Input.GetMouseButton(1)){
            vcam.m_Lens.FieldOfView = Mathf.Lerp(vcam.m_Lens.FieldOfView, 40.0f, zoomIn);
            zoomOut = 0.0f;
            if(zoomIn < 1.0f){
                zoomIn += Time.deltaTime;
            } else {
                zoomIn = 1.0f;
            }
        } else {
            vcam.m_Lens.FieldOfView = Mathf.Lerp(vcam.m_Lens.FieldOfView, 60.0f, zoomOut);
            zoomIn = 0.0f;
            if(zoomOut < 1.0f){
                zoomOut += Time.deltaTime;
            } else {
                zoomOut = 1.0f;
            }
        }
    }
}
