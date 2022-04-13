using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DrillerManager : MonoBehaviour
{
    GameObject pencilAnimationObject;
    Animator pencilAnimator;

    public GameObject drillerBody;
    public GameObject laserBeam;

    public List<string> animationNames;


    public float speedOfAnimation;

    public float yPos;

    public GameObject scrubber;

    float t = 0f;
    float x = 0f;

    bool drilling = false;
    bool drilled = false;

    public float yDrillOffset;

    public ParticleSystem smoke;
    public ParticleSystem woodP;

    public AudioSource myAudio;

    public GameObject tapAndHold;

    bool gameFinished;



    // Start is called before the first frame update
    void Start()
    {

        myAudio = GetComponent<AudioSource>();
        loadPencilAnimation();
        scrubber = FindObjectOfType<_Manager>().scrubber;
        StartCoroutine("startDriller");
       // StartCoroutine("startDriller2");


    }

    void loadPencilAnimation()
    {

        pencilAnimator = GameObject.FindGameObjectWithTag("PencilAnimator").GetComponent<Animator>();
        pencilAnimationObject = GameObject.FindGameObjectWithTag("Pencil");

    }

    // Update is called once per frame
    void Update()
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



        if (drilling)
        {
            touch_condition = Input.GetMouseButton(0);
        }
        else
        {
            touch_condition = Input.GetMouseButton(0) && (!EventSystem.current.IsPointerOverGameObject());
        }


        if (touch_condition && !gameFinished)
        {
            drilling = true;
            laserBeam.SetActive(true);
            t += Time.deltaTime * speedOfAnimation;
            smoke.Play();
            woodP.Play();

            if(!myAudio.isPlaying)
            {
                myAudio.Play();
                _Manager.Agent.startVibration();

            }

            if (tapAndHold.activeInHierarchy)
            {
                tapAndHold.SetActive(false);
            }


        }
        else
        {

            if (myAudio.isPlaying)
            {
                myAudio.Stop();
                _Manager.Agent.stopVibration();

            }
            drilling = false;
            laserBeam.SetActive(false);
            if (smoke.isPlaying)
            {
                smoke.Stop();
               woodP.Stop();
                _Manager.Agent.stopVibration();

            }

        }

        if (!drilled && x>=1)
        {
            drilled = true;
            FindObjectOfType<NextButton>().currentPhase = 3;
            FindObjectOfType<NextButton>().goToNextPhase();
        }

        woodP.gameObject.transform.position = smoke.gameObject.transform.position;

    }


    IEnumerator startDriller()
    {


        var reached = false;

        while (!reached)
        {
            var t_ = t;
            x = Mathf.Clamp(t_ / 2f, 0f, 1f);
            x = x * x * (3 - 2 * x);

            pencilAnimator.Play(animationNames[_Manager.currentLevel], 0, x);

            var targetPos = pencilAnimationObject.transform.position;
            targetPos.y = scrubber.transform.position.y;
            transform.position = targetPos+new Vector3(0,2f,0f);
            var p = targetPos;
            p.y = -0.53f;
            drillerBody.transform.forward = -(p - drillerBody.transform.position);
            smoke.transform.position = p+new Vector3(0,0.1f,0);

            if (x >= 1f)
            {
                reached = true;
                smoke.gameObject.SetActive(false);
                woodP.gameObject.SetActive(false);
                myAudio.Stop();
                gameFinished = true;
                _Manager.Agent.stopVibration();

            }

            yield return null;

        }


    }

    //IEnumerator startDriller2()
    //{


    //    var reached = false;

    //    while (!reached)
    //    {
           
    //        pencilAnimator.Play(animationNames[_Manager.Agent.currentLevel], 0, x);

    //        var targetPos = pencilAnimationObject.transform.position;
    //        targetPos.y = scrubber.transform.position.y;
    //        scrubber.transform.position = targetPos - new Vector3(0, 0, yDrillOffset);
    //        if (x >= 1f)
    //        {
    //            reached = true;
    //        }

    //        yield return new WaitForSeconds(.2f);

    //    }


    //}


}
