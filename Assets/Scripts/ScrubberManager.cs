using System.Collections;
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

    public bool lerped = false;

    void Start()
    {

        startPos = transform.position;
        transform.position += new Vector3(4f, 0, 0);
        Invoke("getIn", 2f);

    }

    void getIn()
    {
        StartCoroutine("getInCor");
    }

    IEnumerator getInCor()
    {
        bool reached = false;
        Vector3 currentPos = transform.position;
        

        float t = 0f;

        while (!reached)
        {
            t += Time.deltaTime;
            var x = Mathf.Clamp(t / .4f, 0f, 1f);
            transform.position = Vector3.Lerp(currentPos, startPos, x);

            if(x>=1f)
            {
                lerped = true;
                reached = true;
            }

            yield return null;
        }

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

    void Update()
    {


        bool touch_condition = false;

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

            if (scrubberDropped)
            {
                scrubberDropped = false;
                terrainClipper.segmentCount = 0;

            }

        }
        else if(lerped)
        {
            transform.position = Vector3.Lerp(transform.position, startPos, Time.deltaTime * 18f);

        }

    }
}
