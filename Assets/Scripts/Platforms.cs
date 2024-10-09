using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platforms : MonoBehaviour
{
    Collider col;
    public Platforms platforms;

    void Start(){
        col = GetComponent<Collider>();
    }
}
