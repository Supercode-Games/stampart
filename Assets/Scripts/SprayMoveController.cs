using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SprayMoveController : MonoBehaviour
{
    public float sensitivity;

    public ParticleSystem myParticles;

    public Vector3 startPos;

   public Vector3 initOffset;

    AudioSource audioSource;
    public Button nextButt;
    bool up = false;
    bool goBackToPlace;

    public void Start()
    {
        audioSource = GetComponent<AudioSource>();
        transform.position = startPos;
    }


    private void OnEnable()
    {
        initOffset = new Vector3(10f, 0f, 0f);

    }

    Vector3 pointOnPlane()
    {
        var pos = Vector3.zero;

        RaycastHit raycastHit;
        if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),out raycastHit,70f,1<<LayerMask.NameToLayer("TouchPlane")))
        {
            pos = raycastHit.point;
        }

        return pos + new Vector3(0,1,0);
    }

    // Update is called once per frame
    void Update()
    {
        bool touch_condition = false;

#if !UNITY_EDITOR

        if (Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);

            var touchid = touch.fingerId;

            touch_condition =(!EventSystem.current.IsPointerOverGameObject(touchid));
        }

#endif



#if UNITY_EDITOR



      touch_condition = (!EventSystem.current.IsPointerOverGameObject());
        

#endif

        if (Input.GetMouseButtonDown(0) && touch_condition)
        {
            up = false;
            goBackToPlace = false;
            CancelInvoke("goBackToPlaceActivate");
            var tp = pointOnPlane();
            tp.z = startPos.z;
            transform.position = tp;
        }

        if (Input.GetMouseButtonDown(0) &&  touch_condition)
        {
            nextButt.interactable = true;
        }

        if (Input.GetMouseButton(0) && touch_condition)
        {
            var x = Input.GetAxis("Mouse X") * sensitivity;
            var y = Input.GetAxis("Mouse Y") * sensitivity;

            var targetpos = pointOnPlane();
            transform.position = Vector3.Lerp(transform.position, targetpos, Time.deltaTime * 20f);

            if(!myParticles.isPlaying)
            {
                myParticles.Play();
                audioSource.Play();
            }

        }
        else
        {
            if (!up)
            {
                up = true;
                if (myParticles.isPlaying)
                {
                    audioSource.Stop();
                }
                myParticles.Stop();
                CancelInvoke("goBackToPlaceActivate");
                Invoke("goBackToPlaceActivate",.4f);
                
            }

            if(goBackToPlace)
            {
                transform.position = Vector3.Lerp(transform.position, startPos + initOffset, Time.deltaTime * 10f);
                initOffset = Vector3.Lerp(initOffset, Vector3.zero, Time.deltaTime * 10f);
            }
            
        }
         
    }

    void goBackToPlaceActivate()
    {
        goBackToPlace = true;
    }
}
