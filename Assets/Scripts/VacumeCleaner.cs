using UnityEngine;
using UnityEngine.EventSystems;

public class VacumeCleaner : MonoBehaviour
{

    public float forceMultipler;

    public bool canMove;

    bool isSucking;

    public Animator vaccumeAnimator;

    public GameObject vaccumeBody;

    int totalParticlesSucked;
    bool cleaned;

    public ParticleSystem vaccumeSuckingEffect;

    Vector3 startPos;

    public float initOffset;

    void Start()
    {
        startPos = transform.position;
    }


    public Vector3 pointOnPlane()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit raycastHit;

        if (Physics.Raycast(ray, out raycastHit, 50f, 1 << LayerMask.NameToLayer("TouchPlane")))
        {
            return raycastHit.point;
        }

        return Vector3.zero;
    }


    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            FindObjectOfType<NextButton>().tutorial.SetActive(false);
        }

        if (vaccumeBody.transform.localPosition.y <= 0f)
        {

            Collider[] colliders = Physics.OverlapSphere(transform.position, 2f, 1 << LayerMask.NameToLayer("CarveParticle"));


            foreach (var item in colliders)
            {
                item.GetComponent<CarveParticle>().setVacumeCenter(gameObject);
                var rb = item.GetComponent<Rigidbody>();
                var dist = Vector3.Distance(transform.position, rb.transform.position);

                totalParticlesSucked++;

                if (dist <= 0.2f)
                {
                    dist = .2f;
                }
                if (item.transform.position.y < transform.position.y)
                {
                    rb.velocity = (transform.position - rb.transform.position).normalized * (forceMultipler / dist);
                }
            }

        }

        bool touch_condition = false;

#if !UNITY_EDITOR

        var touch = Input.GetTouch(0);

        var touchid = touch.fingerId;

        touch_condition = Input.touches.Length > 0 && (EventSystem.current.IsPointerOverGameObject(touchid));


        if (touch_condition)
        {
            return;
        }


#endif


        touch_condition = Input.GetMouseButtonDown(0) && (EventSystem.current.IsPointerOverGameObject());

        if (touch_condition)
        {
            return;
        }


        touch_condition = Input.GetMouseButtonDown(0);



        if (touch_condition && canMove)
        {
            var targetPos = pointOnPlane();
            targetPos.z += 3f;
            targetPos.y = transform.position.y;

            var tp = targetPos * 1.4f;
            tp.y = transform.position.y;
            transform.position = Vector3.Lerp(transform.position, tp, Time.deltaTime * 20f);

            if (!isSucking)
            {
                isSucking = true;
                vaccumeAnimator.SetTrigger("Action");
                vaccumeSuckingEffect.Play();
                GetComponent<AudioSource>().Play(0);
                _Manager.Agent.startVibrationSingle();
            }

        }



        if (isSucking)
        {
            touch_condition = Input.GetMouseButton(0);
        }
        else
        {

            touch_condition = Input.GetMouseButton(0) && (!EventSystem.current.IsPointerOverGameObject());
        }




        if(!touch_condition)
        {
            initOffset = Mathf.Lerp(initOffset, 0f, Time.deltaTime * 10f);
            transform.position = Vector3.Lerp(transform.position, startPos + new Vector3(initOffset,0,0), Time.deltaTime * 18f);
        }

        if (canMove && touch_condition)
        {


            var targetPos = pointOnPlane();
            targetPos.z += 3f;
            targetPos.y = transform.position.y;

            transform.position = Vector3.Lerp(transform.position,targetPos,Time.deltaTime*20f);



        }
       else if(isSucking && canMove)
        {
            isSucking = false;

            vaccumeAnimator.SetTrigger("Idle");
            vaccumeSuckingEffect.Stop();
            GetComponent<AudioSource>().Stop();



        }

        if(!cleaned && isCleaned())
        {


            cleaned = true;
            FindObjectOfType<NextButton>().activateButton(1);


        }

    }


    bool isCleaned()
    {
        bool cleaned = false;

        Collider[] colliders = Physics.OverlapBox(new Vector3(0, -.31f, 5.79f), new Vector3(12f, 2f, 12f) * .5f, Quaternion.identity, 1 << LayerMask.NameToLayer("CarveParticle"));


        if(colliders.Length<2)
        {
            cleaned = true;
        }

        return cleaned;
    }


    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
            Gizmos.DrawWireCube(new Vector3(0, -.31f, 5.79f), new Vector3(12f, 2f, 12f));
    }

}
