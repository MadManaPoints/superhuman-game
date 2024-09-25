using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] Transform orientation;
    private float xRotation;
    private float yRotation;
    [SerializeField] float sens;
    [SerializeField] float a, b;
    Vector3 offset = new Vector3(1f,0f, 0f);

    void Start()
    {
        
    }

    void Update()
    {
        CamControls();
    }

    void CamControls(){
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sens;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sens / 4;

        yRotation += mouseX;
        xRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation, a, b);

        //orientation.position = orientation.position + offset;
        orientation.rotation = Quaternion.Euler(xRotation, yRotation, 0f);
        player.rotation = Quaternion.Euler(0f, yRotation, 0f);
    }
}
