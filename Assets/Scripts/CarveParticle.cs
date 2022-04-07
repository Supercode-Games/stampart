using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarveParticle : MonoBehaviour
{
    public Rigidbody myRB;

    public float minForceY;
    public float maxForceY;

    public float minForceFlat;
    public float maxForceFlat;


    bool nearCavume;
    GameObject vacume;

    public float dist;

    // Start is called before the first frame update
    void Start()
    {
        burst();
    }

    void burst()
    {
        transform.rotation = Quaternion.Euler(Random.Range(0f, 360f), Random.Range(0f, 360f), Random.Range(0f, 360f));
        myRB.velocity = new Vector3(Random.Range(minForceFlat,maxForceFlat), Random.Range(minForceY,maxForceY), Random.Range(minForceFlat, maxForceFlat));
    }

    public void setVacumeCenter(GameObject vacume)
    {
        nearCavume = true;
        this.vacume = vacume;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

       if(nearCavume)
        {
            dist = Vector3.Distance(transform.position, vacume.transform.position);
            if (dist < 1.4f)
            {
                gameObject.SetActive(false);
            }

        }
        
    }
}
