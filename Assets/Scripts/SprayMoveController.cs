using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprayMoveController : MonoBehaviour
{
    public float sensitivity;

    public ParticleSystem myParticles;

    public Vector3 startPos;


    public void Start()
    {
        transform.position = startPos;
    }


    Vector3 pointOnPlane()
    {
        var pos = Vector3.zero;

        RaycastHit raycastHit;
        if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),out raycastHit,70f,1<<LayerMask.NameToLayer("TouchPlane")))
        {
            pos = raycastHit.point;
        }

        return pos;
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetMouseButtonDown(0))
        {

            var tp = pointOnPlane();
            tp.z = startPos.z;
            transform.position = tp;
        }

        if(Input.GetMouseButton(0))
        {
            var x = Input.GetAxis("Mouse X") * sensitivity;
            var y = Input.GetAxis("Mouse Y") * sensitivity;

            var targetpos = pointOnPlane();
            transform.position = Vector3.Lerp(transform.position, targetpos, Time.deltaTime * 20f);

            if(!myParticles.isPlaying)
            {
                myParticles.Play();
            }

        }
        else if(myParticles.isPlaying)
        {
            myParticles.Stop();
            transform.position = Vector3.Lerp(transform.position, startPos, Time.deltaTime * 20f);

        }
    }
}
