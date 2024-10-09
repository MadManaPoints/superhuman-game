using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PM : MonoBehaviour
{
    public ParticleSystem pm;
    void Start()
    {
        pm = GetComponent<ParticleSystem>();
        pm.Stop();
    }

    void Update()
    {
    }
}
