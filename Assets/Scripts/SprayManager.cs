using System.Collections;
using UnityEngine;
using MoreMountains.NiceVibrations;
using UnityEngine.EventSystems;

public class SprayManager : MonoBehaviour
{
    public GameObject sprayBody;

    public float moveSpeed;

    public float f;
    public float r;
    public float u;

    public Animator sprayAnim;
    bool spraying;

    public GameObject tut;

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

    private void Update()
    {


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


        if (spraying)
        {
            touch_condition = Input.GetMouseButton(0);
        }
        else
        {
            touch_condition = Input.GetMouseButton(0) && (!EventSystem.current.IsPointerOverGameObject());
        }


        if (touch_condition)
        {

            var targetPos = pointOnPlane()+new Vector3();
            targetPos += transform.forward * f;
            targetPos += transform.right * r;
            targetPos += transform.up * u;


            transform.position = Vector3.Lerp(transform.position, targetPos*1.4f, Time.deltaTime * moveSpeed);
            sprayAnim.speed = .8f;

            if(!GetComponent<AudioSource>().isPlaying)
            {
                spraying = true;
                GetComponent<AudioSource>().Play(0);


#if UNITY_ANDROID

#endif
            }
            if(tut.activeInHierarchy)
            {
                tut.SetActive(false);
            }

        }
        else
        {
            sprayAnim.speed = 0f;

            if (GetComponent<AudioSource>().isPlaying)
            {
                spraying = false;
                GetComponent<AudioSource>().Stop();
            }

        }

    }


    IEnumerator vibrateCorMM()
    {
        while (true)
        {
            MMVibrationManager.Vibrate();
            yield return new WaitForSeconds(.08f);

        }
    }



}
