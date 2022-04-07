using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class ScrubberManager : MonoBehaviour
{
    public GameObject scrubberBody;
 
    public float sensitivity;
    public float moveSpeed;


    bool isInAction;

    public Animator myAnim;


    public bool scrubberDropped;

    public RuntimeCircleClipper terrainClipper;

    Vector3 startPos;

    public GameObject tut;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }


    public Vector3 pointOnPlane()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit raycastHit;

        if(Physics.Raycast(ray,out raycastHit,50f,1 << LayerMask.NameToLayer("TouchPlane")))
        {
            return raycastHit.point;
        }

        return Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {


        bool touch_condition = false;

//#if !UNITY_EDITOR

//        var touch = Input.GetTouch(0);

//        var touchid = touch.fingerId;

//        touch_condition = Input.touches.Length > 0 && (EventSystem.current.IsPointerOverGameObject(touchid));


//        if (touch_condition)
//        {
//            return;
//        }


//#endif

        touch_condition = Input.GetMouseButtonDown(0) && (!EventSystem.current.IsPointerOverGameObject());

        if(touch_condition)
        {
            return;
        }



        if (isInAction)
        {
            touch_condition = Input.GetMouseButton(0);
        }
        else
        {
            touch_condition = Input.GetMouseButton(0) && (!EventSystem.current.IsPointerOverGameObject());

        }



        if (touch_condition)
        {
            tut.SetActive(false);
            var targetPos = pointOnPlane();
            targetPos.z += 3f;

            transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * moveSpeed);


            if (!isInAction)
            {
                isInAction = true;
                myAnim.SetTrigger("InAction");
                //_Manager.Agent.startVibration();
                //GetComponent<AudioSource>().Play(0);
            }

            if (!scrubberDropped && scrubberBody.transform.position.y <= 1.05f)
            {
                scrubberDropped = true;
                terrainClipper.segmentCount = 20;
            }

        }
        else if(isInAction)
        {
            isInAction = false;
            myAnim.SetTrigger("Idl");
            //_Manager.Agent.stopVibration();
            //GetComponent<AudioSource>().Stop();




            if (scrubberDropped)
            {
                scrubberDropped = false;
                terrainClipper.segmentCount = 0;

            }

        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, startPos, Time.deltaTime * 18f);

        }

    }
}
