using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ref : MonoBehaviour
{
    public bool hit;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(!hit && collision.gameObject.tag=="ref")
        {
            hit = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
